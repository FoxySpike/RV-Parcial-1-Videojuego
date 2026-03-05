using UnityEngine;
using UnityEngine.InputSystem;

public class AutoSpawner : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject playerPrefab;
    public Transform[] spawnPoints; // Nueva lista de posiciones

    private string[] schemes = { "P1", "P2", "P3", "P4" };

    void Start()
    {
        // Verificamos que tengamos suficientes puntos de spawn
        if (spawnPoints.Length < 4)
        {
            Debug.LogError("¡Faltan puntos de spawn en el Inspector!");
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            var p = PlayerInput.Instantiate(
                playerPrefab,
                playerIndex: i,
                controlScheme: schemes[i],
                pairWithDevices: Keyboard.current
            );

            if (p != null)
            {
                // Usamos la posición exacta del objeto que pongas en Unity
                p.transform.position = spawnPoints[i].position;
                p.transform.rotation = spawnPoints[i].rotation; // También respetamos su rotación

                p.currentActionMap = p.actions.FindActionMap("Player");
            }
        }
    }
}