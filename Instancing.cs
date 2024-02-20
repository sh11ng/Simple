using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instancing : MonoBehaviour
{
    EventManager eventManager;

    public List<Transform> spawnPoint;

	[SerializeField] private LayerMask groundLayer;

    [SerializeField] private GameObject[] _ball;
	[SerializeField] private GameObject whiteBall;
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private GameObject ghost;

    private GameObject[] _spawn;

    [SerializeField] private Vector3 worldPosition;
    [SerializeField] private Vector3 screenPosition;

    [SerializeField] private Material ghostMaterial;

    [SerializeField] private bool startInstantiate;
    public bool ballInstance = false;
	
	private Ray ray;

    void Start()
    {
        _spawn = GameObject.FindGameObjectsWithTag("Spawn");

        eventManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EventManager>();

        foreach (GameObject i in _spawn)
        {
            spawnPoint.Add(i.transform);
        }

        ballInstantiate();

        startInstantiate = false;
    }

    void Update()
    {
        if (eventManager.pauseGame == false)
        {
            if (startInstantiate == false)
            {
                screenPosition = Input.mousePosition;
                screenPosition.z = Camera.main.nearClipPlane + 1;
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                Physics.Raycast(ray, out RaycastHit hitData, 1000, groundLayer);

                worldPosition = hitData.point;

                if (hitData.collider != null)
                {
                    if (ghost == null)
                    {
                        ghost = Instantiate(ghostPrefab, new Vector3(10f, 0.00407f, 0f), Quaternion.identity);
                        ghost.GetComponent<Renderer>().material = ghostMaterial;
                    }

                    else
                    {
                        ghost.transform.position = new Vector3(10f, 0.00407f, worldPosition.z);
                    }

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Instantiate(whiteBall, new Vector3(10f, 0.00407f, worldPosition.z), Quaternion.identity);
                        Destroy(ghost);
                        startInstantiate = true;
                        ballInstance = true;
                    }
                }
            }

            else if (eventManager.breakShot == true && eventManager.isFoul == true && startInstantiate == true && eventManager.turnStart == true)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));

                screenPosition = Input.mousePosition;
                screenPosition.z = Camera.main.nearClipPlane + 1;
                ray = Camera.main.ScreenPointToRay(screenPosition);

                Physics.Raycast(ray, out RaycastHit hitData, 100, groundLayer);

                worldPosition = hitData.point;

                if (hitData.collider != null)
                {
                    if (ghost == null)
                    {
                        ghost = Instantiate(ghostPrefab, new Vector3(10f, 0.00407f, 0f), Quaternion.identity);
                        ghost.GetComponent<Renderer>().material = ghostMaterial;
                    }

                    else
                    {
                        ghost.transform.position = new Vector3(10f, 0.00407f, worldPosition.z);
                    }

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Instantiate(whiteBall, new Vector3(10f, 0.00407f, worldPosition.z), Quaternion.identity);
                        Destroy(ghost);
                        eventManager.isFoul = false;
                        ballInstance = true;
                    }
                }
            }

            else if (eventManager.isFoul == true && eventManager.turnStart == true)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                screenPosition = Input.mousePosition;
                screenPosition.z = Camera.main.nearClipPlane + 1;
                ray = Camera.main.ScreenPointToRay(screenPosition);

                Physics.Raycast(ray, out RaycastHit hitData, 100, groundLayer);

                worldPosition = hitData.point;

                if (ghost == null)
                {
                    ghost = Instantiate(ghostPrefab, new Vector3(0f, 0.00407f, 0f), Quaternion.identity);
                    ghost.GetComponent<Renderer>().material = ghostMaterial;
                }

                else
                {
                    ghost.transform.position = new Vector3(worldPosition.x, 0.00407f, worldPosition.z);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Instantiate(whiteBall, new Vector3(worldPosition.x, 0.00407f, worldPosition.z), Quaternion.identity);
                    Destroy(ghost);
                    eventManager.isFoul = false;
                    ballInstance = true;
                }
            }
        }
    }

    void ballInstantiate()
    {
        for (int i = 0; i < 15; i++)
        {
            int r = Random.Range(0, spawnPoint.Count - 1);

            _spawn[i].GetComponent<Respawn>().ballInstance = Instantiate(_ball[i], spawnPoint[r].position, Random.rotation);
            _spawn[i].GetComponent<Respawn>().ball = _ball[i];
            eventManager._ball.Add(_spawn[i].GetComponent<Respawn>().ballInstance);
            spawnPoint.RemoveAt(r);
        }
    }
}
