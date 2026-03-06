using UnityEngine;

public class BotonPresion : MonoBehaviour
{
    public bool EstaPresionado { get; private set; }

    private int objetosEncima = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")||other.CompareTag("ObjetoMovil"))
        {
            objetosEncima++;
            EstaPresionado = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")||other.CompareTag("ObjetoMovil"))
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