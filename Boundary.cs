using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    EventManager eventManager;

    public GameObject control;

    private BoxCollider _collider;

    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.FindGameObjectWithTag("GameController");

        eventManager = control.GetComponent<EventManager>();

        _collider = GetComponent<BoxCollider>();

        _collider.center = new Vector3(0, -30, 0);
        _collider.size = new Vector3(1000, 1, 1000);
        _collider.isTrigger = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7)
        {
            if (collision.gameObject.tag == "Player")
            {
                Destroy(collision.gameObject);
                eventManager.isFoul = true;
                eventManager.whiteBallMoving = false;
            }

            else
            {
                Destroy(collision.gameObject);
                eventManager.isFoul = true;
                eventManager._ball.Remove(collision.gameObject);
            }
        }
    }
}
