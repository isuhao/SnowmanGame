using UnityEngine;
using System.Collections;

public class SunScript : MonoBehaviour {

    float dayLength;

	// Use this for initialization
	void Start () {
        dayLength = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().dayLength;
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.right, (360 / dayLength) * Time.deltaTime);
	}
}
