using UnityEngine;

public class CamaraMenu : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [Tooltip("Grados por segundo a los que girará la cámara.")]
    public float velocidadRotacion = 4f;

    void Update()
    {
        // Rotamos sobre el eje Y (vertical) para obtener el giro horizontal de 360°
        transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime, Space.Self);
    }
}