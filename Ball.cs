using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    EventManager eventManager;

    public bool isMoving = false;

    private Vector3 position1;
    private Vector3 position2;

    void Start()
    {
        eventManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EventManager>();
    }

    private void Update()
    {
        if (gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude > 0.01)
        {
            isMoving = true;
            //StartCoroutine(move());
        }
    }

    /*IEnumerator move()
    {
        position1 = transform.position;

        yield return new WaitForSeconds(0.2f);

        position2 = transform.position;

        if (position1 != position2)
        {
            eventManager.ballMoving = true;
            StartCoroutine(move());
        }

        else if (position1 == position2)
        {
            eventManager.ballMoving = false;
            isMoving = false;
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            //isMoving = true;
            //StartCoroutine(move());
        }

        else if (collision.gameObject.layer == 8)
        {
            if (eventManager.breakShot == true && eventManager.collisionCount < 4)
            {
                eventManager.collisionCount++;
            }

            else if (eventManager.breakShot == true && eventManager.collisionCount == 4)
            {
                eventManager.breakShot = false;
            }
            
            else if (eventManager.breakShot == false && (eventManager.isSolid == true || eventManager.isStripe == true))
            {
                eventManager.railContact = true;
            }
        }
    }
}
