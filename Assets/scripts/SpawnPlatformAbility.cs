using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class SpawnPlatformAbility : PlayerAbility
{
    public GameObject platformPrefab;
    public Transform spawnPoint;

    private GameObject currentPlatform;

    public float platformDuration = 3f;
    public float riseHeight = 1f;
    public float riseSpeed = 4f;

    [Header("Restricciones")]
    public int allowedPlayerIndex = 0; // qué jugador puede usarlo

    private CharacterController controller;
    private PlayerInput playerInput;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    public override void Activate()
    {
        // SOLO cierto jugador puede usar la habilidad
        if (playerInput.playerIndex != allowedPlayerIndex)
            return;

        // SOLO si está en el suelo
        if (!controller.isGrounded)
            return;

        // SOLO si no existe otra plataforma
        if (currentPlatform != null)
            return;

        Vector3 spawnPos = spawnPoint.position - new Vector3(0, riseHeight, 0);

        currentPlatform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);

        StartCoroutine(RisePlatform(currentPlatform));
        StartCoroutine(DestroyPlatform());
    }

    IEnumerator RisePlatform(GameObject platform)
    {
        Vector3 start = platform.transform.position;
        Vector3 target = spawnPoint.position;

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * riseSpeed;
            platform.transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }
    }

    IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(platformDuration);

        if (currentPlatform != null)
        {
            Destroy(currentPlatform);
            currentPlatform = null;
        }
    }
}