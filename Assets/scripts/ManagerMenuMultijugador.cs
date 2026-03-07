using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ManagerMenuMultijugador : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject panelMenu;
    public GameObject[] chulitosJugadores; // Arrastra los 4 iconos de chulitos aquí
    public GameObject botonContinuar;

    [Header("Referencias Lógica")]
    public AutoSpawner spawner; // Arrastra tu objeto con el script AutoSpawner

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoCheck;

    private bool[] jugadorListo = new bool[4];
    private bool menuActivo = true;

    void Start()
    {
        // Pausamos el juego al iniciar
        Time.timeScale = 0f;
        panelMenu.SetActive(true);
        botonContinuar.SetActive(false);

        foreach (var chulo in chulitosJugadores) chulo.SetActive(false);
    }

    void Update()
    {
        if (!menuActivo) return;

        ActualizarEstadoDispositivos();
    }

    private void ActualizarEstadoDispositivos()
    {
        int conteoListos = 0;

        // Lógica basada en tu AutoSpawner
        if (spawner.modoPruebaTeclado)
        {
            // En modo prueba, los 4 están listos instantáneamente
            for (int i = 0; i < 4; i++) MarcarJugador(i);
            conteoListos = 4;
        }
        else
        {
            // Jugador 1 siempre es teclado en tu script real
            MarcarJugador(0);
            conteoListos = 1;

            // Detectar mandos para los otros 3 espacios (P2, P3, P4)
            int mandosDetectados = Gamepad.all.Count;
            for (int i = 1; i < 4; i++)
            {
                if (i <= mandosDetectados)
                {
                    MarcarJugador(i);
                    conteoListos++;
                }
                else
                {
                    DesmarcarJugador(i);
                }
            }
        }

        // Mostrar botón de continuar solo si están los 4
        botonContinuar.SetActive(conteoListos == 4);
    }

    private void MarcarJugador(int index)
    {
        if (!jugadorListo[index])
        {
            jugadorListo[index] = true;
            chulitosJugadores[index].SetActive(true);
            audioSource.PlayOneShot(sonidoCheck);
        }
    }

    private void DesmarcarJugador(int index)
    {
        if (jugadorListo[index])
        {
            jugadorListo[index] = false;
            chulitosJugadores[index].SetActive(false);
        }
    }

    public void OnBotonContinuarClick()
    {
        menuActivo = false;
        panelMenu.SetActive(false);
        Time.timeScale = 1f; // Despausamos el juego
        Debug.Log("Juego Iniciado: Together Up!");
    }
}
