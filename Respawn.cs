using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    EventManager eventManager;

    [SerializeField] public GameObject ball;
    [SerializeField] public GameObject ballInstance;
    private GameObject control;

    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.FindGameObjectWithTag("GameController");
        eventManager = control.GetComponent<EventManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ballInstance != null && eventManager.turnStart == true)
        {
            transform.position = ballInstance.transform.position;
            transform.rotation = ballInstance.transform.rotation;
        }

        else if (ballInstance == null && eventManager.isFoul == false && eventManager.turnStart == true && eventManager.whiteBallMoving == false && eventManager.ballMoving == false)
        {
            Destroy(gameObject);
        }

        else if (ballInstance == null && eventManager.isFoul == true && eventManager.turnStart == true && eventManager.whiteBallMoving == false && eventManager.ballMoving == false)
        {
            ballInstance = Instantiate(ball, transform.position, transform.rotation);
            eventManager._ball.Add(ballInstance);
        }
    }
}
