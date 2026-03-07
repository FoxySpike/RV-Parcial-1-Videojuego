using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CamaraSeguimiento : MonoBehaviour
{
    [Header("Jugadores")]
    public List<Transform> jugadores = new List<Transform>();

    [Header("Movimiento")]
    public float suavizado = 5f;

    private float alturaFija;
    private float zFijo;

    void Start()
    {
        alturaFija = transform.position.y;
        zFijo = transform.position.z;

        BuscarJugadores();
    }

    void LateUpdate()
    {
        if (jugadores.Count == 0)
        {
            BuscarJugadores();
            return;
        }

        float centroX = CalcularCentroX();

        Vector3 posicionObjetivo = new Vector3(
            centroX,
            alturaFija,
            zFijo
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

    float CalcularCentroX()
    {
        float suma = 0f;

        foreach (var j in jugadores)
        {
            suma += j.position.x;
        }

        return suma / jugadores.Count;
    }
}