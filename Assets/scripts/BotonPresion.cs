using UnityEngine;

public class BotonPresion : MonoBehaviour
{
    public bool EstaPresionado { get; private set; }

    private int objetosEncima = 0;

    public AudioSource sonidoBoton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("ObjetoMovil"))
        {
            objetosEncima++;

            if (!EstaPresionado)
            {
                EstaPresionado = true;
                sonidoBoton.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("ObjetoMovil"))
        {
            objetosEncima--;

            if (objetosEncima <= 0)
            {
                objetosEncima = 0;
                EstaPresionado = false;
            }
        }
    }
}