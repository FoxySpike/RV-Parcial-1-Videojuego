using UnityEngine;
using UnityEngine.InputSystem;

public class AutoSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints;

    void Start()
    {
        var gamepads = Gamepad.all;

        // Player 1 → teclado
        SpawnPlayer(0, Keyboard.current, "P1");

        // Players 2-4 → gamepads
        for (int i = 0; i < gamepads.Count && i < 3; i++)
        {
            SpawnPlayer(i + 1, gamepads[i], "Gamepad");
        }
        Debug.Log("Gamepads conectados: " + Gamepad.all.Count);
        foreach (var g in Gamepad.all)
        {
            Debug.Log("Gamepad detectado: " + g.displayName);
        }
    }

    void SpawnPlayer(int index, InputDevice device, string scheme)
    {
        var p = PlayerInput.Instantiate(
            playerPrefab,
            playerIndex: index,
            controlScheme: scheme,
            pairWithDevice: device
        );

        p.transform.position = spawnPoints[index].position;
        p.transform.rotation = spawnPoints[index].rotation;
    }
}