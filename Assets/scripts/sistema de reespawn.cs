using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SistemaRespawn : MonoBehaviour
{
    public float limiteCaida = -10f;

    private Transform puntoRespawn;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        GameObject respawn = GameObject.FindGameObjectWithTag("Respawn");

        if (respawn != null)
        {
            puntoRespawn = respawn.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con tag Respawn");
        }
    }

    void Update()
    {
        if (puntoRespawn == null)
            return;

        if (transform.position.y < limiteCaida)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        controller.enabled = false;
        transform.position = puntoRespawn.position;
        controller.enabled = true;
    }
}