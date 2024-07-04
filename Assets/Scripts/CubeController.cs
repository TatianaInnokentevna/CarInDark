using UnityEngine;
using UnityEngine.AI;

public class CubeController : MonoBehaviour
{
    private BoxCollider _collider;
    private MeshRenderer _mesh;
    public float speed = 5f, rotationSpeed = 100f;

    public CarLights carLights;
    private NavMeshAgent agent;
    private Camera mainCamera;
    private AudioSource startAudioSource;
    private AudioSource stopAudioSource;
    private bool isMoving = false;
    private bool wasMoving = false;

    void Awake()
    {
        _collider = gameObject.GetComponent<BoxCollider>();
        _mesh = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        startAudioSource = audioSources[0];
        stopAudioSource = audioSources[1];
    }

    private void Update()
    {
        float hormove = Input.GetAxis("Horizontal");
        float vermove = Input.GetAxis("Vertical");

        // Rotate the car
        transform.Rotate(0, hormove * rotationSpeed * Time.deltaTime, 0);

        // Move the car forward and backward
        transform.position += transform.forward * vermove * speed * Time.deltaTime;

        // Determine if the car is moving
        bool isCurrentlyMoving = vermove != 0 || hormove != 0;

        // Play start sound if the car starts moving
        if (isCurrentlyMoving && !wasMoving)
        {
            startAudioSource.Play();
            isMoving = true;
        }

        // Play stop sound if the car stops moving
        if (!isCurrentlyMoving && wasMoving)
        {
            stopAudioSource.Play();
            isMoving = false;
        }

        // Update the wasMoving state
        wasMoving = isCurrentlyMoving;

        if (vermove > 0)
        {
            carLights.SetLights(true, false); // Включаем передние фары, выключаем задние
        }
        else if (vermove < 0)
        {
            carLights.SetLights(false, true); // Выключаем передние фары, включаем задние
        }
        else
        {
            carLights.SetLights(false, false); // Выключаем все фары
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                if (!isMoving)
                {
                    startAudioSource.Play();
                    isMoving = true;
                }
            }
        }

        if (isMoving && agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            stopAudioSource.Play();
            isMoving = false;
        }
    }
}
