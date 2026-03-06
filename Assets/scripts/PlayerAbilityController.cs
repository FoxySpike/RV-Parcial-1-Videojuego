using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityController : MonoBehaviour
{
    private PlayerAbility ability;

    void Awake()
    {
        ability = GetComponent<PlayerAbility>();
    }

    public void OnAbility(InputValue value)
    {
        if (!value.isPressed) return;

        if (ability != null)
        {
            ability.Activate();
        }
    }
}