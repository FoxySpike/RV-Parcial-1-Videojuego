using UnityEngine;

public class sistemadereespawn : MonoBehaviour
{
    public Transform posicionReespawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            other.transform.position = posicionReespawn.position;
        }
    }
}
