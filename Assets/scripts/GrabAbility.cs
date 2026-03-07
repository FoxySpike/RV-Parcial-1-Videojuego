using UnityEngine;

public class GrabAbility : PlayerAbility
{
    [Header("Detección")]
    public float grabRadius = 2f;
    public LayerMask grabbableLayer;

    [Header("Posición de agarre")]
    public Transform grabPoint;

    private GameObject objetoCercano;
    private Rigidbody objetoAgarrado;

    public Animator animator;           // referencia al animator
    public string shootTrigger = "Shoot";

    void Update()
    {
        DetectarObjeto();
    }

    void DetectarObjeto()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            grabRadius,
            grabbableLayer
        );

        objetoCercano = null;

        float distanciaMin = Mathf.Infinity;

        foreach (var hit in hits)
        {
            float d = Vector3.Distance(
                transform.position,
                hit.transform.position
            );

            if (d < distanciaMin)
            {
                distanciaMin = d;
                objetoCercano = hit.gameObject;
            }
        }
    }

    public override void Activate()
    {

        if (animator != null)
        {
            animator.SetTrigger(shootTrigger);
        }
        
        if (objetoAgarrado == null)
        {
            IntentarAgarrar();
        }
        else
        {
            Soltar();
        }
    }

    void IntentarAgarrar()
    {
        if (objetoCercano == null) return;

        objetoAgarrado = objetoCercano.GetComponent<Rigidbody>();

        if (objetoAgarrado == null) return;

        objetoAgarrado.isKinematic = true;
        objetoAgarrado.transform.position = grabPoint.position;
        objetoAgarrado.transform.SetParent(grabPoint);
    }

    void Soltar()
    {
        objetoAgarrado.transform.SetParent(null);
        objetoAgarrado.isKinematic = false;

        objetoAgarrado = null;
    }
}