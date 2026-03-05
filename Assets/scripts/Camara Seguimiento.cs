using UnityEngine;

public class CamaraSeguimiento : MonoBehaviour
{
    [Header("Objetivo a seguir")]
    public Transform jugadorObjetivo;

    [Header("Configuración")]
    public float suavizado = 5f;
    public bool noRetroceder = true;

    private float ultimaPosicionX;

    void Start()
    {
        if (jugadorObjetivo != null)
        {
            ultimaPosicionX = jugadorObjetivo.position.x;
        }
    }

    void LateUpdate()
    {
        if (jugadorObjetivo == null)
            return;

        if (!jugadorObjetivo)
            return;

        float objetivoX = jugadorObjetivo.position.x;

        if (noRetroceder)
        {
            objetivoX = Mathf.Max(ultimaPosicionX, objetivoX);
            ultimaPosicionX = objetivoX;
        }

        Vector3 nuevaPosicion = new Vector3(
            objetivoX,
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            nuevaPosicion,
            suavizado * Time.deltaTime
        );
    }
}