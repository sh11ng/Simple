using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Canvas solidWin;
    public Canvas stripeWin;
    public Canvas lostScreen;

    public int solidScore = 0;
    public int stripeScore = 0;
    public bool blackGoal = false;

    private void Update()
    {
        if (solidScore == 7 || stripeScore == 7)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<EventManager>().finalShot = true;
        }
    }

    void FixedUpdate()
    {
        if (blackGoal == true)
        {
            if (solidScore == 7)
            {
                solidWin.gameObject.SetActive(true);
                Time.timeScale = 0f;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<EventManager>().pauseGame = true;
            }

            else if (stripeScore == 7)
            {
                stripeWin.gameObject.SetActive(true);
                Time.timeScale = 0f;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<EventManager>().pauseGame = true;
            }

            else if (solidScore < 7 || stripeScore < 7)
            {
                lostScreen.gameObject.SetActive(true);
                Time.timeScale = 0f;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<EventManager>().pauseGame = true;
            }
        }
    }
}
