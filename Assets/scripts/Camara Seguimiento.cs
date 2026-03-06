using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CamaraSeguimiento : MonoBehaviour
{
    [Header("Jugadores")]
    public List<Transform> jugadores = new List<Transform>();

    [Header("Movimiento")]
    public float suavizado = 5f;

    [Header("Distancia cámara")]
    public float distanciaMin = 12f;
    public float distanciaMax = 22f;
    public float separacionMax = 20f;

    private float alturaFija;
    private float distanciaActual;

    void Start()
    {
        alturaFija = transform.position.y;
        distanciaActual = transform.position.z;

        BuscarJugadores();
    }

    void LateUpdate()
    {
        if (jugadores.Count == 0)
        {
            BuscarJugadores();
            return;
        }

        Vector3 centro = CalcularCentro();
        float separacion = CalcularSeparacion();

        float t = Mathf.Clamp01(separacion / separacionMax);
        float distanciaObjetivo = Mathf.Lerp(distanciaMin, distanciaMax, t);

        distanciaActual = Mathf.Lerp(
            distanciaActual,
            distanciaObjetivo,
            suavizado * Time.deltaTime
        );

        Vector3 posicionObjetivo = new Vector3(
            centro.x,
            alturaFija,
            centro.z - distanciaActual
        );

        transform.position = Vector3.Lerp(
            transform.position,
            posicionObjetivo,
            suavizado * Time.deltaTime
        );
    }

    void BuscarJugadores()
    {
        jugadores.Clear();

        PlayerInput[] players = FindObjectsOfType<PlayerInput>();

        foreach (var p in players)
        {
            jugadores.Add(p.transform);
        }
    }

    Vector3 CalcularCentro()
    {
        Vector3 suma = Vector3.zero;

        foreach (var j in jugadores)
        {
            suma += j.position;
        }

        return suma / jugadores.Count;
    }

    float CalcularSeparacion()
    {
        float maxDist = 0f;

        for (int i = 0; i < jugadores.Count; i++)
        {
            for (int j = i + 1; j < jugadores.Count; j++)
            {
                float dist = Vector3.Distance(
                    jugadores[i].position,
                    jugadores[j].position
                );

                if (dist > maxDist)
                    maxDist = dist;
            }
        }

        return maxDist;
    }
}