using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour {

    GameObject target;
    public float moveSpeed = 3; 
    float rotationSpeed = 3; 
    float range = 1000f;
    float stop = 2.0f;
    Transform myTransform;

    public AudioSource explosionSource;
    public AudioSource hitSource;

    public bool dead = false;
    float randomScale;
    bool hasPresent = false;

    public float health = 100;

	// Use this for initialization
	void Start () {

        myTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {

        if (health <= 0)
        {
            Die(true);
        }
        else
        {
            target = null;
            if (hasPresent)
            {
                target = GameObject.FindGameObjectWithTag("Finish");
            }
            else
            {
                GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");

                float shortestDistance = range;
                foreach (GameObject turret in turrets)
                {
                    float distance = Vector3.Distance(gameObject.transform.position, turret.transform.position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        target = turret;
                    }
                }

                if (target == null)
                {
                    GameObject[] presents = GameObject.FindGameObjectsWithTag("Present");

                    shortestDistance = range;
                    foreach (GameObject present in presents)
                    {
                        float distance = Vector3.Distance(gameObject.transform.position, present.transform.position);
                        if (distance < shortestDistance)
                        {
                            shortestDistance = distance;
                            target = present;
                        }
                    }
                }

                if(target == null)
                {
                    target = GameObject.FindGameObjectWithTag("Player");
                }
            }

            if (target != null)
            {
                //rotate to look at the target
                float distance = Vector3.Distance(myTransform.position, target.transform.position);
                if (distance <= range && distance > stop)
                {
                    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.transform.position - myTransform.position), rotationSpeed * Time.deltaTime);
                    myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
                }
                else if (distance <= stop)
                {
                    if (hasPresent)
                    {
                        Die(false);
                    }
                    else
                    {
                        if (target.tag == "Turret")
                        {
                            Destroy(target.GetComponent<TurretScript>());
                            target.tag = "TurretTaken";
                            target.transform.parent = gameObject.transform;
                            hasPresent = true;
                        }
                        else if (target.tag == "Present")
                        {
                            target.tag = "PresentTaken";
                            target.transform.parent = gameObject.transform;
                            hasPresent = true;
                            GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                            gameManager.numPresents--;
                        }
                    }
                }
            }
        }
	}

    public void Die(bool withScore)
    {
        if (!dead)
        {
            explosionSource.Play();
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().ShakeCamera();

            if (withScore)
            {
                GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                gameManager.score += (int)(100 * randomScale);
            }
            dead = true;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform child = gameObject.transform.GetChild(i);
                if (child.gameObject.tag != "PresentTaken")
                {
                    child.gameObject.AddComponent<Rigidbody>();
                    Destroy(child.gameObject, 5.0f);
                }
                else
                {
                    child.gameObject.tag = "Present";
                }
            }

            gameObject.transform.DetachChildren();
            Destroy(gameObject, 5.0f);
        }
    }

    public void Hit()
    {
        hitSource.Play();
        health -= 25;
        moveSpeed *= 1.2f;
        rotationSpeed *= 1.2f;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            //child.
        }
    }

    public void Scale()
    {
        float randomNumber = NormalNext(0.1f, 0.0f, 2.0f);

        if (randomNumber < 0.5f) randomNumber = 0.5f;

        int rare = Random.Range(1, 50);
        if (rare == 9) randomNumber = 3.0f;

        randomScale = randomNumber;
        gameObject.transform.localScale *= randomNumber;
        health *= randomNumber;
        moveSpeed /= randomNumber;
    }

    public static float NormalNext(float Steps, float minValue, float MaxValue)
    {
        int count = 0;
        float val = minValue;

        if (Steps < 0) return 0;

        while (count * Steps <= MaxValue)
        {
            val += Random.Range(0.0f, Steps);
            count++;
        }

        return val;
    }
}
