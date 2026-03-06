using UnityEngine;
using UnityEngine.InputSystem;

public class AutoSpawner : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject playerPrefab;
    public Transform[] spawnPoints;

    [Header("Debug / Pruebas")]
    [Tooltip("Si se activa, spawnea 4 jugadores usando solo el teclado (WASD, IJKL, Arrows, Numpad)")]
    public bool modoPruebaTeclado = false;

    void Start()
    {
        Invoke("EjecutarSpawn", 0.2f);
    }

    void EjecutarSpawn()
    {
        if (modoPruebaTeclado) SpawnParaPruebasLocal();
        else InicializarMultijugadorReal();
    }
    private void InicializarMultijugadorReal()
    {
        // Jugador 1: Siempre Teclado
        SpawnPlayer(0, Keyboard.current, "P1");

        // Jugadores 2-4: Solo si hay mandos conectados
        var gamepads = Gamepad.all;
        for (int i = 0; i < gamepads.Count && i < 3; i++)
        {
            string nombreEsquema = "P" + (i + 2); // Resultado: P2, P3, P4
            SpawnPlayer(i + 1, gamepads[i], nombreEsquema);
        }

        LogDispositivos(gamepads.Count);
    }

    private void SpawnParaPruebasLocal()
    {
        Debug.Log("--- MODO PRUEBA ACTIVO: Spawneando 4 personajes en teclado ---");

        // Cada uno con su esquema de teclas (WASD, IJKL, Arrows, Numpad)
        SpawnPlayer(0, Keyboard.current, "P1");
        SpawnPlayer(1, Keyboard.current, "P2");
        SpawnPlayer(2, Keyboard.current, "P3");
        SpawnPlayer(3, Keyboard.current, "P4");
    }

    private void SpawnPlayer(int index, InputDevice device, string scheme)
    {
        if (device == null || index >= spawnPoints.Length) return;

        var p = PlayerInput.Instantiate(
            playerPrefab,
            playerIndex: index,
            controlScheme: scheme,
            pairWithDevice: device
        );

        p.transform.position = spawnPoints[index].position;
        p.transform.rotation = spawnPoints[index].rotation;
    }

    private void LogDispositivos(int count)
    {
        Debug.Log($"Gamepads detectados: {count}");
        foreach (var g in Gamepad.all)
        {
            Debug.Log($"Conectado: {g.displayName}");
        }
    }
}