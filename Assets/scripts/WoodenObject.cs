using UnityEngine;
using System.Collections;

public class WoodenObject : MonoBehaviour
{
    public float explosionForce = 5f;
    public float explosionRadius = 3f;

    public AudioSource sonidoRomper;

    private bool broken = false;
    private Collider objectCollider;

    void Awake()
    {
        objectCollider = GetComponent<Collider>();

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;
    }

    public void BreakObject(Vector3 hitPosition)
    {
        if (broken)
            return;

        broken = true;

        Debug.Log("Estructura desarmada");

        if (sonidoRomper != null)
{
        sonidoRomper.time = 1f; // empieza desde el segundo 1
        sonidoRomper.Play();
}

        // desactivar collider del padre
        objectCollider.enabled = false;

        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();

            if (rb == null)
                continue;

            rb.isKinematic = false;

            rb.AddExplosionForce(
                explosionForce,
                hitPosition,
                explosionRadius,
                1f,
                ForceMode.Impulse
            );

            StartCoroutine(FadeAndDestroy(child.gameObject, 2f));
        }

        Destroy(gameObject, 5f);
    }

    IEnumerator FadeAndDestroy(GameObject obj, float duration)
    {
        Renderer rend = obj.GetComponent<Renderer>();

        if (rend != null)
        {
            Material mat = rend.material;

            SetMaterialFade(mat);

            Color color = mat.color;
            float time = 0;

            while (time < duration)
            {
                float alpha = Mathf.Lerp(1f, 0f, time / duration);
                mat.color = new Color(color.r, color.g, color.b, alpha);

                time += Time.deltaTime;
                yield return null;
            }
        }

        Destroy(obj);
    }

    void SetMaterialFade(Material mat)
    {
        mat.SetFloat("_Mode", 2);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);

        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");

        mat.renderQueue = 3000;
    }
}