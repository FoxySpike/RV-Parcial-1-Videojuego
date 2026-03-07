using UnityEngine;

public class ArcherShootAbility : PlayerAbility
{
    public GameObject arrowPrefab;
    public Transform shootPoint;

    public float shootCooldown = 0.5f;
    private float lastShootTime;

    public Animator animator;
    public string shootTrigger = "Shoot";

    public AudioSource shootSound; // sonido del disparo

    public override void Activate()
    {
        // cooldown
        if (Time.time < lastShootTime + shootCooldown)
            return;

        if (arrowPrefab == null || shootPoint == null)
        {
            Debug.LogError("Falta arrowPrefab o shootPoint");
            return;
        }

        lastShootTime = Time.time;

        if (animator != null)
        {
            animator.SetTrigger(shootTrigger);
        }

        // reproducir sonido
        if (shootSound != null)
        {
            shootSound.Play();
        }

        Instantiate(
            arrowPrefab,
            shootPoint.position,
            shootPoint.rotation
        );

        Debug.Log("Flecha disparada");
    }
}