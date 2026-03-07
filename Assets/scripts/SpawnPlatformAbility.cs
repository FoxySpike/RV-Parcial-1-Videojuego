using UnityEngine;
using System.Collections;

public class SpawnPlatformAbility : PlayerAbility
{
    public GameObject platformPrefab;
    public Transform spawnPoint;

    private GameObject currentPlatform;

    public float platformDuration = 3f;
    public float riseHeight = 1f;
    public float riseSpeed = 4f;

    private CharacterController controller;

    public Animator animator;
    public string shootTrigger = "Shoot";

    public AudioSource audioSpawn; // audio de la habilidad

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public override void Activate()
    {
        if (!controller.isGrounded)
            return;

        if (currentPlatform != null)
            return;

        Vector3 spawnPos = spawnPoint.position - new Vector3(0, riseHeight, 0);

        currentPlatform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);

        // reproducir sonido desde el segundo 2
        if (audioSpawn != null)
        {
            audioSpawn.time = 0.7f;
            audioSpawn.Play();
        }

        StartCoroutine(RisePlatform(currentPlatform));
        StartCoroutine(DestroyPlatform());
    }

    IEnumerator RisePlatform(GameObject platform)
    {
        if (animator != null)
        {
            animator.SetTrigger(shootTrigger);
        }

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