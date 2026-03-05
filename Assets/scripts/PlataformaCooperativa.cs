using UnityEngine;

public class PlataformaCooperativa : MonoBehaviour
{
    [Header("Botones")]
    public BotonPresion[] botones;
    public int botonesRequeridos = 2;

    [Header("Movimiento")]
    public Transform posicionActivada;
    public float velocidad = 3f;

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        int botonesActivos = ContarBotonesPresionados();

        if (botonesActivos >= botonesRequeridos)
        {
            MoverHacia(posicionActivada.position);
        }
        else
        {
            MoverHacia(posicionInicial);
        }
    }

    int ContarBotonesPresionados()
    {
        int contador = 0;

        foreach (BotonPresion boton in botones)
        {
            if (boton != null && boton.EstaPresionado)
            {
                contador++;
            }
        }

        return contador;
    }

    void MoverHacia(Vector3 destino)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            destino,
            velocidad * Time.deltaTime
        );
    }
}