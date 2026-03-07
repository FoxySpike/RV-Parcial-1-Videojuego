using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelFinishUI : MonoBehaviour
{
    public Image fadeImage;
    public GameObject panelFinal;

    public float velocidadFade = 1.5f;

    void Start()
    {
        Color c = fadeImage.color;
        c.a = 0;
        fadeImage.color = c;

        panelFinal.SetActive(false);
    }

    public void CompletarNivel()
    {
        StartCoroutine(FadeBlanco());
    }

    IEnumerator FadeBlanco()
    {
        Color c = fadeImage.color;

        while (c.a < 0.6f)
        {
            c.a += Time.deltaTime * velocidadFade;
            fadeImage.color = c;
            yield return null;
        }

        panelFinal.SetActive(true);
    }

    public void SiguienteNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
