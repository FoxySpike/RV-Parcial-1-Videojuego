using UnityEngine;

public class KnightPushAbility : PlayerAbility
{
    [Header("Configuración")]
    public float checkRadius = 2f;
    public LayerMask pushLayer;

    private CharacterController controller;

    public Animator animator;           // referencia al animator
    public string shootTrigger = "Shoot"; // nombre del trigger en el animator

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public override void Activate()
    {
        // ✅ Solo funciona en el suelo
        if (!controller.isGrounded)
            return;

        PushObjects();
    }

    void PushObjects()
    {
        // reproducir animación
        if (animator != null)
        {
            animator.SetTrigger(shootTrigger);
        }
        Collider[] hits = Physics.OverlapSphere(
            transform.position + transform.forward * checkRadius,
            checkRadius,
            pushLayer
        );

        foreach (var hit in hits)
        {
            WoodenObject wood = hit.GetComponent<WoodenObject>();
            if (wood == null)
                continue;

            // 🔥 Ahora enviamos la posición del golpe
            wood.BreakObject(hit.transform.position);
        }
    }
}