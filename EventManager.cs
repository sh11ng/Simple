using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EventManager : MonoBehaviour
{
    Playable player;
    Goal goal;

    public List<GameObject> _ball;

    public Canvas groupSelect;
    [SerializeField] TMP_Text foulLabel;
    [SerializeField] TMP_Text solidNumber;
    [SerializeField] TMP_Text stripeNumber;

    public GameObject whiteBall;
    public GameObject _goal;

    [SerializeField] private int number = 15;
    public int collisionCount = 0;
    public int solidCount = 7;
    public int stripeCount = 7;

    public bool turnStart = true;
    public bool isFoul = false;
    public bool ballMoving = false;
    public bool whiteBallMoving = false;
    public bool railContact = true;
    public bool breakShot = true;
    public bool isSolid = false;
    public bool isStripe = false;
    public bool finalShot = false;
    public bool pauseGame = false;

    void Start()
    {
        _goal = GameObject.FindGameObjectWithTag("Finish");

        goal = _goal.GetComponent<Goal>();
    }

    private void Update()
    {
        foreach (GameObject ball in _ball)
        {
            if (ball.GetComponent<Rigidbody>().velocity.sqrMagnitude > 0.0001)
            {
                ballMoving = true;
                number--;
            }

            else if (number == 15)
            {
                ballMoving = false;
            }
        }

        number = 15;

        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<Instancing>().ballInstance == true)
        {
            whiteBall = GameObject.FindGameObjectWithTag("Player");
            player = whiteBall.GetComponent<Playable>();
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Instancing>().ballInstance = false;
        }

        if (breakShot == false && turnStart == true && ballMoving == false && whiteBallMoving == false && isSolid == false && isStripe == false && isFoul == false)
        {
            groupSelect.gameObject.SetActive(true);
        }

        if (isFoul == true)
        {
            foulLabel.gameObject.SetActive(true);
        }
        else if (isFoul == false)
        {
            foulLabel.gameObject.SetActive(false);
        }

        solidNumber.GetComponent<TMP_Text>().text = solidCount.ToString();
        stripeNumber.GetComponent<TMP_Text>().text = stripeCount.ToString();
    }

    private void LateUpdate()
    {
        if (ballMoving == false  && whiteBallMoving == false && turnStart == false)
        {
            StartCoroutine(TurnReset());
        }
    }

    IEnumerator TurnReset()
    {
        if ((railContact == false || player.firstContact == false) && breakShot == false && player.isMoving == false)
        {
            isFoul = true;
        }

        yield return new WaitForSeconds(2);

        turnStart = true;
        railContact = false;
        player.firstContact = false;
        goal.solidScore = false;
        goal.stripeScore = false;
    }
}