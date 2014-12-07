using UnityEngine;
using System.Collections;

public class BloodScript : MonoBehaviour {
    
    public float speed = 50.0f;
    public float time = 5.0f;

	// Use this for initialization
	void Start () 
    {
        GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * speed);
        Destroy(gameObject, time);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
