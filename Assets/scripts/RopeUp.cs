using UnityEngine;

public class RopeUp : MonoBehaviour
{
    public float speed = 5f;
    public float maxHeight = 20f;

    private bool rising = false;

    public void StartRise()
    {
        rising = true;
    }

    void Update()
    {
        if (!rising) return;

        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y > maxHeight)
            Destroy(gameObject);
    }
}