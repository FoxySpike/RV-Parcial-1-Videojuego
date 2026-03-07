using UnityEngine;

public class TargetHit : MonoBehaviour
{
    public RopeUp ropeTarget;      // cuerda que sube
    public BoxFall box;            // caja que cae
    public RopeFade ropeBox;       // cuerda que se desvanece

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Arrow"))
            return;

        Debug.Log("Diana impactada");

        if (ropeTarget != null)
            ropeTarget.StartRise();

        if (box != null)
            box.DropBox();

        if (ropeBox != null)
            ropeBox.StartFade();

        Destroy(gameObject);
    }
}