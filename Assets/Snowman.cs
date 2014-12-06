using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour {

    GameObject target;
    public float moveSpeed = 3; 
    float rotationSpeed = 3; 
    float range = 1000f;
    float stop = 2.0f;
    Transform myTransform;

    bool hasPresent = false;

    public int health = 100;

	// Use this for initialization
	void Start () {

        myTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {

        target = null;
        if (hasPresent)
        {
            target = GameObject.FindGameObjectWithTag("Finish");
        }
        else
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Present");

            float shortestDistance = 1000.0f;
            foreach (GameObject present in objects)
            {
                float distance = Vector3.Distance(gameObject.transform.position, present.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    target = present;
                }
            }

            if(target == null)
            {
                target = GameObject.FindGameObjectWithTag("Player");
            }
        }

        if (health <= 0)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++ )
            {
                Transform child = gameObject.transform.GetChild(i);
                child.gameObject.AddComponent<Rigidbody>();
                Destroy(child.gameObject, 5.0f);
            }

            gameObject.transform.DetachChildren();
            Destroy(gameObject);
        }
        else
        {
            if (target != null)
            {
                //rotate to look at the player
                float distance = Vector3.Distance(myTransform.position, target.transform.position);
                if (distance <= range && distance > stop)
                {

                    //move towards the player
                    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.transform.position - myTransform.position), rotationSpeed * Time.deltaTime);
                    myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
                }
                else if (distance <= stop)
                {
                    if (hasPresent)
                    {
                    }
                    else
                    {
                        if (target.tag == "Present")
                        {
                            target.tag = "Enemy";
                            target.transform.parent = gameObject.transform;
                            hasPresent = true;
                        }
                    }
                }
            }
        }
	}

    public void Hit()
    {
        health -= 25;
        moveSpeed *= 1.2f;
        rotationSpeed *= 1.2f;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            //child.
        }
    }
}
