using UnityEngine;
using System.Collections;

public class SunScript : MonoBehaviour {

    public float sunSpeed = 1;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(sunSpeed * Time.deltaTime, 0, 0);
	}
}
