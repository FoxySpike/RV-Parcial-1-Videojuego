using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVisuals : MonoBehaviour
{
    [Header("Modelos Hijos")]
    public GameObject[] modelos; // Arrastra aquí tus 4 modelos (Mage, etc.)

    void Start()
    {
        // Obtenemos el índice del jugador (0, 1, 2 o 3)
        int id = GetComponent<PlayerInput>().playerIndex;

        for (int i = 0; i < modelos.Length; i++)
        {
            if (modelos[i] != null)
                modelos[i].SetActive(i == id);
        }
    }
}