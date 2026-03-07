using UnityEngine;

public class BoxFall : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public void DropBox()
    {
        rb.useGravity = true;
    }
}