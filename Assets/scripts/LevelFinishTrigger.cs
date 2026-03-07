using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishTrigger : MonoBehaviour
{
    public int jugadoresNecesarios = 4;

    private HashSet<GameObject> jugadoresDentro = new HashSet<GameObject>();
    private bool nivelTerminado = false;

    private LevelFinishUI ui;

    public AudioSource sonidoFinal; // sonido de nivel completado

    void Start()
    {
        ui = FindObjectOfType<LevelFinishUI>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadoresDentro.Add(other.gameObject);

            if (!nivelTerminado && jugadoresDentro.Count >= jugadoresNecesarios)
            {
                nivelTerminado = true;

                // reproducir sonido
                if (sonidoFinal != null)
                {
                    sonidoFinal.Play();
                }

                ui.CompletarNivel();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadoresDentro.Remove(other.gameObject);
        }
    }
}