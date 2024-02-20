using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Playable : MonoBehaviour
{
    EventManager eventManager;
    followBall followBall;

    [SerializeField] private Canvas playerControl;
    [SerializeField] private Camera playerCam;
    [SerializeField] private Vector3 screenPosition;
    [SerializeField] public Vector3 worldPosition;
    [SerializeField] private LayerMask groundMask;
	[SerializeField] private LayerMask lineMask;

    public float speed = 100.0f;
    private Rigidbody rb;
    private LineRenderer line;

    public bool isReady = false;
    public bool firstContact = true;
    public bool isMoving = false;

    Vector3 position1;
    Vector3 position2;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();

        eventManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EventManager>();
        //followBall = playerCam.GetComponent<followBall>();

        if (eventManager.breakShot == false)
        {
            firstContact = false;
            eventManager.railContact = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (eventManager.pauseGame == false)
        {
            if (eventManager.turnStart == true && isMoving == false)
            {
                if (Input.GetKeyDown(KeyCode.Space) == true)
                {
                    if (isReady == false)
                    {
                        //followBall.camSet = true;
                        isReady = true;
                        playerControl.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (eventManager.pauseGame == false)
        {
            if (isReady == false)
            {
                mousePosition();
            }
        }
    }

    void mousePosition()
    {
        if (eventManager.turnStart == true && eventManager.whiteBallMoving == false && isMoving == false)
        {
            screenPosition = Input.mousePosition;

            screenPosition.z = Camera.main.nearClipPlane + 1;

            Ray camRay = Camera.main.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(camRay, out RaycastHit hitData, 100, groundMask))
            {
                worldPosition = hitData.point - transform.position;
            }

            Debug.DrawRay(camRay.origin, camRay.direction * 100, Color.black);

            worldPosition.y = transform.position.y;

            line.enabled = true;
            line.SetPosition(0, transform.position);

            if (Physics.Raycast(transform.position, worldPosition, out RaycastHit hitInfo, 100, lineMask))
            {
                line.SetPosition(1, hitInfo.point);
            }
        }
    }

    public void AddForce()
    {
        line.enabled = false;
        eventManager.turnStart = false;
        eventManager.whiteBallMoving = true;
        isReady = false;
        playerControl.gameObject.SetActive(false);
        StartCoroutine(Movement());
        rb.AddForce(worldPosition * (speed * 2));
    }

    public void PowerSlider()
    {
        Slider slider = playerControl.GetComponentInChildren<Slider>();
        TMP_Text powerValue = slider.GetComponentInChildren<TMP_Text>();
        powerValue.text = slider.value.ToString();

        speed = slider.value;

    }

    public void Return()
    {
        isReady = false;
        playerControl.gameObject.SetActive(false);
    }

    IEnumerator Movement()
    {
        position1 = transform.position;

        yield return new WaitForSeconds(0.1f);

        position2 = transform.position;

        if (position1 != position2)
        {
            eventManager.whiteBallMoving = true;
            StartCoroutine(Movement());
        }

        else if (position2 == position1)
        {
            eventManager.whiteBallMoving = false;
            isMoving = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (eventManager.breakShot == false)
        {
            if (firstContact == false)
            {
                if (eventManager.isSolid == true)
                {
                    if (collision.gameObject.tag == "solidBall")
                    {
                        firstContact = true;
                    }

                    else if (collision.gameObject.tag == "stripeBall" || collision.gameObject.tag == "blackBall")
                    {
                        eventManager.isFoul = true;
                    }
                }

                else if (eventManager.isStripe == true)
                {
                    if (collision.gameObject.tag == "stripeBall")
                    {
                        firstContact = true;
                    }

                    else if (collision.gameObject.tag == "solidBall" || collision.gameObject.tag == "blackBall")
                    {
                        eventManager.isFoul = true;
                    }
                }
            }
        }
    }
}
