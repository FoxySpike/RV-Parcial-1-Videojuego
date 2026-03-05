using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovimientoJugador3D : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadMovimiento = 8f;
    public float fuerzaSalto = 7f;

    [Header("Suelo")]
    public Transform puntoSuelo;
    public float radioSuelo = 0.2f;
    public LayerMask capaSuelo;

    private Rigidbody rb;
    private float inputX;
    private float inputZ;
    private bool estaEnSuelo;
    private bool saltoSolicitado;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Recomendado para jugador
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Captura de input (SIEMPRE en Update)
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && estaEnSuelo)
        {
            saltoSolicitado = true;
        }
    }

    void FixedUpdate()
    {
        VerificarSuelo();
        Mover();
        Saltar();
    }

    void Mover()
    {
        Vector3 direccion = new Vector3(inputX, 0f, inputZ).normalized;

        if (direccion.magnitude > 0.1f)
        {
            RotarJugador(direccion);
        }

        Vector3 velocidadObjetivo = direccion * velocidadMovimiento;

        Vector3 cambioVelocidad = velocidadObjetivo - 
            new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(cambioVelocidad, ForceMode.VelocityChange);
    }

    void RotarJugador(Vector3 direccion)
    {
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            rotacionObjetivo,
            10f * Time.fixedDeltaTime
        );
    }

    void Saltar()
    {
        if (saltoSolicitado)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            saltoSolicitado = false;
        }
    }

    void VerificarSuelo()
    {
        estaEnSuelo = Physics.CheckSphere(puntoSuelo.position, radioSuelo, capaSuelo);
    }

    void OnDrawGizmosSelected()
    {
        if (puntoSuelo == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(puntoSuelo.position, radioSuelo);
    }
}