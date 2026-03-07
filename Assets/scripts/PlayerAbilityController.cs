using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityController : MonoBehaviour
{
    public PlayerAbility ability;
    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput.playerIndex == 0)
            ability = GetComponent<KnightPushAbility>();

        if (playerInput.playerIndex == 1)
            ability = GetComponent<SpawnPlatformAbility>();

        if (playerInput.playerIndex == 3)
            ability = GetComponent<ArcherShootAbility>();

        if (playerInput.playerIndex == 2)
            ability = GetComponent<GrabAbility>();
    }

    public void OnAbility(InputValue value)
    {
        if (!value.isPressed) return;

        ability?.Activate();
    }
}