using UnityEngine;
using UnityEngine.InputSystem;

public class ArcherShootAbility : PlayerAbility
{
    public GameObject arrowPrefab;
    public Transform shootPoint;

    public float shootCooldown = 0.5f;
    private float lastShootTime;

    public int allowedPlayerIndex = 1; // jugador que puede usar la habilidad

    public override void Activate()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();

        if (playerInput == null || playerInput.playerIndex != allowedPlayerIndex)
            return;

        if (Time.time < lastShootTime + shootCooldown)
            return;

        lastShootTime = Time.time;

        Instantiate(
            arrowPrefab,
            shootPoint.position,
            shootPoint.rotation
        );
    }
}