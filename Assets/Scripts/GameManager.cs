using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    Vector3 originalCameraPosition;
    float shakeAmt = 0.02f;
    public Camera mainCamera;

    public GameObject daysCounter;
    public GameObject scoreCounter;
    public GameObject presentsCounter;
    public GameObject sun;

    public int numDays = 25;
    public int numPresents = 10;
    public int score = 0;

    float lastDayTime;
    public float dayLength = 30;
    public bool dayTime = true;

	// Use this for initialization
	void Start () {
        lastDayTime = Time.time;
        originalCameraPosition = mainCamera.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        daysCounter.GetComponent<Text>().text = "" + numDays;
        scoreCounter.GetComponent<Text>().text = "" + score;
        presentsCounter.GetComponent<Text>().text = "" + numPresents;

        if(Time.time - lastDayTime >= dayLength)
        {
            lastDayTime = Time.time;
            numDays--;
        }

        if (Time.time - lastDayTime >= dayLength / 4 && Time.time - lastDayTime < (dayLength / 4) * 3 && dayTime)
            dayTime = false;

        if (Time.time - lastDayTime >= (dayLength / 4) * 3 && !dayTime)
            dayTime = true;

        if(dayTime)
        {
            GameObject[] snowmen = GameObject.FindGameObjectsWithTag("Snowman");
            foreach(GameObject snowman in snowmen)
            {
                snowman.GetComponent<Snowman>().Die(false);
            }
        }
	}

    public void ShakeCamera()
    {
        if(!IsInvoking())
        { 
            InvokeRepeating("CameraShake", 0, 0.01f);  
            Invoke("StopShaking", 0.3f);
        }
    }

    void CameraShake()
    {
        if (shakeAmt > 0)
        {
            float quakeAmtX = Random.value * shakeAmt * 2 - shakeAmt;
            float quakeAmtY = Random.value * shakeAmt * 2 - shakeAmt;
            float quakeAmtZ = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.x += quakeAmtX;
            pp.y += quakeAmtY;
            pp.z += quakeAmtZ;
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.position = originalCameraPosition;
    }
}
