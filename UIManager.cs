using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    EventManager eventManager;

    [SerializeField] private Camera camera;
    [SerializeField] private Camera TopCam;
    [SerializeField] private Camera NorthCam;
    [SerializeField] private Camera SouthCam;
    [SerializeField] private Camera LeftCam;
    [SerializeField] private Camera RightCam;

    //private GameObject[] ball;

    private void Start()
    {
        if (camera == null)
        {
            TopCam.enabled = true;
            NorthCam.enabled = false;
            SouthCam.enabled = false;
            LeftCam.enabled = false;
            RightCam.enabled = false;

            eventManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EventManager>();
        }

        else if (camera != null)
        {
            camera.enabled = true;
        }
        //eventManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EventManager>();
        //ball = GameObject.FindGameObjectsWithTag("Ball");
    }

    /*private void Update()
    {
        if ()
        {
            ball = GameObject.FindGameObjectsWithTag("Ball");
        }
    }*/

    public void StartGame(int SceneNumber)
    {
        StartCoroutine(Loading());
        SceneManager.LoadScene(SceneNumber);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        eventManager.pauseGame = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        eventManager.pauseGame = false;
    }

    public void SelectSolid()
    {
        eventManager.isSolid = true;
        eventManager.isStripe = false;
    }

    public void SelectStripe()
    {
        eventManager.isSolid = false;
        eventManager.isStripe = true;
    }

    public void CamSelect(int index)
    {
        switch (index)
        {
            case 0:
                TopCam.enabled = true;
                NorthCam.enabled = false;
                SouthCam.enabled = false;
                LeftCam.enabled = false;
                RightCam.enabled = false;
                break;
            case 1:
                TopCam.enabled = false;
                NorthCam.enabled = true;
                SouthCam.enabled = false;
                LeftCam.enabled = false;
                RightCam.enabled = false;
                break;
            case 2:
                TopCam.enabled = false;
                NorthCam.enabled = false;
                SouthCam.enabled = true;
                LeftCam.enabled = false;
                RightCam.enabled = false;
                break;
            case 3:
                TopCam.enabled = false;
                NorthCam.enabled = false;
                SouthCam.enabled = false;
                LeftCam.enabled = true;
                RightCam.enabled = false;
                break;
            case 4:
                TopCam.enabled = false;
                NorthCam.enabled = false;
                SouthCam.enabled = false;
                LeftCam.enabled = false;
                RightCam.enabled = true;
                break;
        }
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(5f);
    }
}
