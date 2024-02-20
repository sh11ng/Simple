using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    Score score;
    EventManager eventManager;

    public GameObject control;

    public bool solidScore = false;
    public bool stripeScore = false;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.FindGameObjectWithTag("GameController").GetComponent<Score>();
        eventManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EventManager>();
    }

    private void FixedUpdate()
    {
        if (eventManager.isSolid == true && solidScore == false && stripeScore == true)
        {
            eventManager.isFoul = true;
        }

        else if (eventManager.isStripe == true && solidScore == true && stripeScore == false)
        {
            eventManager.isFoul = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (eventManager.breakShot == true)
            {
                if (collision.gameObject.tag == "solidBall")
                {
                    Destroy(collision.gameObject);
                    eventManager.breakShot = false;
                    eventManager.solidCount--;
                    score.solidScore++;
                    eventManager._ball.Remove(collision.gameObject);
                }

                else if (collision.gameObject.tag == "stripeBall")
                {
                    Destroy(collision.gameObject);
                    eventManager.breakShot = false;
                    eventManager.stripeCount--;
                    score.stripeScore++;
                    eventManager._ball.Remove(collision.gameObject);
                }

                else if (collision.gameObject.tag == "blackBall")
                {
                    Destroy(collision.gameObject);
                    score.blackGoal = true;
                    eventManager._ball.Remove(collision.gameObject);
                }

                else if (collision.gameObject.tag == "Player")
                {
                    Destroy(collision.gameObject);
                    eventManager.isFoul = true;
                }
            }
            else if (eventManager.breakShot == false)
            {
                if (eventManager.isSolid == true || eventManager.isStripe == true)
                {
                    if (collision.gameObject.tag == "solidBall")
                    {
                        Destroy(collision.gameObject);
                        solidScore = true;
                        eventManager.railContact = true;
                        eventManager.solidCount--;
                        score.solidScore++;
                        eventManager._ball.Remove(collision.gameObject);
                    }

                    else if (collision.gameObject.tag == "stripeBall")
                    {
                        Destroy(collision.gameObject);
                        stripeScore = true;
                        eventManager.railContact = true;
                        eventManager.stripeCount--;
                        score.stripeScore++;
                        eventManager._ball.Remove(collision.gameObject);
                    }

                    else if (collision.gameObject.tag == "blackBall")
                    {
                        Destroy(collision.gameObject);
                        score.blackGoal = true;
                        eventManager._ball.Remove(collision.gameObject);
                    }

                    else if (collision.gameObject.tag == "Player")
                    {
                        Destroy(collision.gameObject);
                        eventManager.isFoul = true;
                    }
                }
                else if (eventManager.isSolid == false && eventManager.isStripe == false)
                {
                    if (collision.gameObject.tag == "solidBall")
                    {
                        Destroy(collision.gameObject);
                        eventManager.solidCount--;
                        score.solidScore++;
                        eventManager._ball.Remove(collision.gameObject);
                    }

                    else if (collision.gameObject.tag == "stripeBall")
                    {
                        Destroy(collision.gameObject);
                        eventManager.stripeCount--;
                        score.stripeScore++;
                        eventManager._ball.Remove(collision.gameObject);
                    }

                    else if (collision.gameObject.tag == "blackBall")
                    {
                        Destroy(collision.gameObject);
                        score.blackGoal = true;
                        eventManager._ball.Remove(collision.gameObject);
                    }

                    else if (collision.gameObject.tag == "Player")
                    {
                        Destroy(collision.gameObject);
                        eventManager.isFoul = true;
                    }
                }
            }
        }
    }
}
