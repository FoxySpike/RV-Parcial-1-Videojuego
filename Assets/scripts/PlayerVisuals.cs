using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVisuals : MonoBehaviour
{
    [Header("Modelos Hijos")]
    public GameObject[] modelos;

    void Start()
    {
        int id = GetComponent<PlayerInput>().playerIndex;

        Animator selectedAnimator = null;

        for (int i = 0; i < modelos.Length; i++)
        {
            if (modelos[i] != null)
            {
                bool active = (i == id);
                modelos[i].SetActive(active);

                if (active)
                    selectedAnimator = modelos[i].GetComponent<Animator>();
            }
        }

        // Informar al sistema de movimiento cuál animator usar
        PlayerMovement movement = GetComponent<PlayerMovement>();

        if (movement != null && selectedAnimator != null)
        {
            movement.SetAnimator(selectedAnimator);
        }
    }
}