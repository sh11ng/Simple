using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followBall : MonoBehaviour
{
	EventManager eventManager;
    Playable player;

	private GameObject control;
    private GameObject whiteBall;

	private Vector3 position;
	private Quaternion rotation;
    public bool camSet;

    private float radius = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
		control = GameObject.FindGameObjectWithTag("GameController");

		eventManager = control.GetComponent<EventManager>();

		position = transform.localPosition;
		rotation = transform.localRotation;

		camSet = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.localPosition = new Vector3(FindX(FindAngle(player.worldPosition)), 0, FindZ(FindAngle(player.worldPosition)));
            transform.LookAt(whiteBall.transform);

			camSet = true;
        }
        
		if (eventManager.turnStart == true)
        {
			transform.localPosition = position;
			transform.localRotation = rotation;
        }
    }

    private void FixedUpdate()
    {
		if (whiteBall == null)
		{
			whiteBall = GameObject.FindGameObjectWithTag("Player");
			player = whiteBall.GetComponent<Playable>();
		}
	}

    float FindAngle(Vector3 point)
	{
		if (point.x < 0)
		{
			if (point.z > 0)
			{
				if (-point.x > point.z)
				{
					return RadianToDegrees(Mathf.Atan2(point.z ,-point.x));
				}
				
				else if (-point.x < point.z)
				{
					return (90 - RadianToDegrees(Mathf.Atan2(-point.x ,point.z)));
				}
				
				else if (-point.x == point.z)
				{
					return 45f;
				}
			}
			
			else if (point.z < 0)
			{
				if (-point.x > -point.z)
				{
					return (360 - RadianToDegrees(Mathf.Atan2(-point.z ,-point.x)));
				}
				
				else if (-point.x < -point.z)
				{
					return (270 + RadianToDegrees(Mathf.Atan2(-point.x ,-point.z)));
				}
				
				else if (-point.x == -point.z)
				{
					return 315f;
				}
			}
			
			else if (point.z == 0)
			{
				return 0f;
			}
		}
		
		else if (point.x > 0)
		{
			if (point.z > 0)
			{
				if (point.x > point.z)
				{
					return (180 - RadianToDegrees(Mathf.Atan2(point.z ,point.x)));
				}
				
				else if (point.x < point.z)
				{
					return (90 + RadianToDegrees(Mathf.Atan2(point.x ,point.z)));
				}
				
				else if (point.x == point.z)
				{
					return 135f;
				}
			}
			
			else if (point.z < 0)
			{
				if (point.x > -point.z)
				{
					return (180 + RadianToDegrees(Mathf.Atan2(-point.z ,point.x)));
				}
				
				else if (point.x < -point.z)
				{
					return (270 - RadianToDegrees(Mathf.Atan2(point.x ,-point.z)));
				}
				
				else if (point.x == -point.z)
				{
					return 225f;
				}
			}
			
			else if (point.z == 0)
			{
				return 180f;
			}
		}

		return 0;
	}

    float FindX(float degree)
    {
        return (transform.localPosition.x + ((Mathf.Cos(-degree * Mathf.PI / 180) * radius) - transform.localPosition.x));
    }

    float FindZ(float degree)
    {
        return (transform.localPosition.z + ((Mathf.Sin(-degree * Mathf.PI / 180) * radius) - transform.localPosition.z));
    }
	
	float DegreesToRadian(float degree)
	{
		return (degree * (Mathf.PI / 180));
	}
	
	float RadianToDegrees(float radian)
	{
		return (radian / (Mathf.PI / 180));
	}
}
