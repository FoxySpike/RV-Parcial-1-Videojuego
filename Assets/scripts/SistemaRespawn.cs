using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SistemaRespawn : MonoBehaviour
{
    public float limiteGuardarCamara = -10f;
    public float limiteRespawn = -30f;

    private Transform[] spawnPoints;
    private CharacterController controller;
    private Camera camara;

    private Vector3 posicionCamaraGuardada;
    private bool camaraGuardada = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Obtener cámara
        GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");

        if (camObj != null)
            camara = camObj.GetComponent<Camera>();
        else
            Debug.LogError("No se encontró la cámara con tag MainCamera");


        // Obtener contenedor de spawns
        GameObject spawnContainer = GameObject.FindGameObjectWithTag("spawn");

        if (spawnContainer == null)
        {
            Debug.LogError("No se encontró objeto con tag 'spawn'");
            return;
        }

        // Obtener hijos (spawnpoints)
        spawnPoints = new Transform[spawnContainer.transform.childCount];

        for (int i = 0; i < spawnContainer.transform.childCount; i++)
        {
            spawnPoints[i] = spawnContainer.transform.GetChild(i);
        }
    }

    void Update()
    {
        if (camara == null || spawnPoints == null || spawnPoints.Length == 0)
            return;

        // Guardar posición de cámara
        if (!camaraGuardada && transform.position.y < limiteGuardarCamara)
        {
            posicionCamaraGuardada = camara.transform.position;
            camaraGuardada = true;
        }

        // Respawn
        if (transform.position.y < limiteRespawn)
        {
            Respawn();
            camaraGuardada = false;
        }
    }

    void Respawn()
    {
        Transform spawnElegido = ObtenerSpawn();

        controller.enabled = false;
        transform.position = spawnElegido.position;
        controller.enabled = true;
    }

    Transform ObtenerSpawn()
    {
        float x = posicionCamaraGuardada.x;

        if (x >= -60 && x < -10)
            return spawnPoints[0]; // Spawn 1

        if (x >= -10 && x < 25)
            return spawnPoints[1]; // Spawn 2

        if (x >= 25 && x < 50)
            return spawnPoints[2]; // Spawn 3

        if (x >= 50 && x <= 200)
            return spawnPoints[3]; // Spawn 4

        // Si la cámara queda fuera de los rangos
        return spawnPoints[0];
    }
}