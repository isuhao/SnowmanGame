using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public GameObject blood;
    public int numParticles = 5;

    public float speed = 50.0f;
    public float time = 5.0f;
	// Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * speed);
        Destroy(gameObject, time);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            Snowman snowman = other.GetComponentInParent<Snowman>();
            if(snowman != null)
            {
                snowman.Hit();
            }
            for (int i = 0; i < numParticles; i++)
            {
                Vector3 location = gameObject.transform.position + (gameObject.transform.forward * 2);
                GameObject bloodObject = (GameObject)Instantiate(blood, location, gameObject.transform.rotation);
            }
        }

        Destroy(gameObject);
    }
}
