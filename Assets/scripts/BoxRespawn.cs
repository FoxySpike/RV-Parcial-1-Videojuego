using UnityEngine;

public class BoxRespawn : MonoBehaviour
{
    public Transform respawnPoint;   // punto donde reaparece
    public float minY = -10f;        // límite inferior

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < minY)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // detener movimiento
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // mover a la posición de respawn
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;
    }
}