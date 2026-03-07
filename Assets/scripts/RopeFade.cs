using UnityEngine;
using System.Collections;

public class RopeFade : MonoBehaviour
{
    public float fadeDuration = 2f;

    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void StartFade()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float t = 0;
        Color color = rend.material.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, t / fadeDuration);
            rend.material.color = color;
            yield return null;
        }

        Destroy(gameObject);
    }
}