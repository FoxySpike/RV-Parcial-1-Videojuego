using UnityEngine;

public class PlataformaCooperativa : MonoBehaviour
{
    [Header("Botones")]
    public BotonPresion[] botones;
    public int botonesRequeridos = 2;

    [Header("Movimiento")]
    public Transform posicionActivada;
    public float velocidad = 3f;

    [Header("Audio")]
    public AudioSource audioMovimiento;

    private Vector3 posicionInicial;
    private bool moviendose = false;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        int botonesActivos = ContarBotonesPresionados();

        Vector3 destino = botonesActivos >= botonesRequeridos
            ? posicionActivada.position
            : posicionInicial;

        bool seEstaMoviendo = Vector3.Distance(transform.position, destino) > 0.01f;

        MoverHacia(destino);

        ControlarAudio(seEstaMoviendo);
    }

    int ContarBotonesPresionados()
    {
        int contador = 0;

        foreach (BotonPresion boton in botones)
        {
            if (boton != null && boton.EstaPresionado)
            {
                contador++;
            }
        }

        return contador;
    }

    void MoverHacia(Vector3 destino)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            destino,
            velocidad * Time.deltaTime
        );
    }

    void ControlarAudio(bool seEstaMoviendo)
    {
        if (seEstaMoviendo && !moviendose)
        {
            moviendose = true;

            if (audioMovimiento != null)
            {
                audioMovimiento.time = 2f; // empezar desde el segundo 2
                audioMovimiento.Play();
            }
        }
        else if (!seEstaMoviendo && moviendose)
        {
            moviendose = false;

            if (audioMovimiento != null)
            {
                audioMovimiento.Stop();
            }
        }
    }
}