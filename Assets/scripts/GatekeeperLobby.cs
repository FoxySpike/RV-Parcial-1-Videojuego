using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Linq;

public class GatekeeperLobby : MonoBehaviour
{
    [Header("Configuración")]
    public string gameSceneName = "Nivel1";

    // Lista para guardar qué dispositivos ya "se unieron"
    private List<InputDevice> joinedDevices = new List<InputDevice>();

    [Header("Debug - Teclado")]
    public bool debugMode = true;

    void Update()
    {
        // 1. Lógica para los Controles (Gamepads)
        // Buscamos en todos los mandos conectados
        foreach (var gamepad in Gamepad.all)
        {
            // Si presionan el botón "A" (Sur) y no están en la lista
            if (gamepad.buttonSouth.wasPressedThisFrame && !joinedDevices.Contains(gamepad))
            {
                RegisterDevice(gamepad);
            }
        }

        // 2. Lógica de Debug (Teclado Particionado)
        // Esto simula que cada tecla es un "jugador" diferente
        if (debugMode && joinedDevices.Count < 4)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame) RegisterDebugDevice("P1-Keyboard");
            if (Keyboard.current.mKey.wasPressedThisFrame) RegisterDebugDevice("P2-Keyboard");
            if (Keyboard.current.rightCtrlKey.wasPressedThisFrame) RegisterDebugDevice("P3-Keyboard");
            if (Keyboard.current.enterKey.wasPressedThisFrame) RegisterDebugDevice("P4-Keyboard");
        }
    }

    void RegisterDevice(InputDevice device)
    {
        if (joinedDevices.Count < 4)
        {
            joinedDevices.Add(device);
            Debug.Log($"Dispositivo registrado: {device.displayName}. Total: {joinedDevices.Count}/4");
            CheckFullLobby();
        }
    }

    // Método especial para el debug de teclado (ya que el teclado es un solo dispositivo)
    void RegisterDebugDevice(string pLabel)
    {
        // En debug, simplemente sumamos al contador para probar la transición
        // Usamos un objeto dummy para que la lista crezca
        if (joinedDevices.Count < 4)
        {
            joinedDevices.Add(Keyboard.current);
            Debug.Log($"{pLabel} listo. Total: {joinedDevices.Count}/4");
            CheckFullLobby();
        }
    }

    void CheckFullLobby()
    {
        if (joinedDevices.Count == 4)
        {
            Debug.Log("¡Los 4 jugadores están listos! Cargando escena...");
            SceneManager.LoadScene(gameSceneName);
        }
    }
}