using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {

    GameObject target;
    float range = 1000f;
    float stop = 2.0f;
    float rotationSpeed = 5;
    public float fireSpeed = 1;
    public float timeOffset = 0.0f;

    float lastShot;
    public Transform projectileSpawn;
    public GameObject projectile;

	// Use this for initialization
	void Start () {
        lastShot = Time.time + timeOffset;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (target == null || (target.GetComponent<Snowman>() != null && target.GetComponent<Snowman>().dead))
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Snowman");

            float shortestDistance = range;
            foreach (GameObject enemy in objects)
            {
                float distance = Vector3.Distance(gameObject.transform.position, enemy.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    target = enemy;
                }
            }
        }

        if (target != null && target.GetComponent<Snowman>() != null && !target.GetComponent<Snowman>().dead)
        {
            Vector3 lookPosition = target.transform.position + (target.transform.forward * 5);
            lookPosition.y = gameObject.transform.position.y;
            Vector3 lookDirection = lookPosition - gameObject.transform.position;
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(lookDirection), rotationSpeed * Time.deltaTime);

            Vector3 forward = transform.forward * 100;
            Debug.DrawRay(transform.position, forward, Color.green);

            if (Time.time - lastShot > (1 / fireSpeed))
            {
                lastShot = Time.time;
                Instantiate(projectile, projectileSpawn.position, gameObject.transform.rotation);
                GetComponent<AudioSource>().Play();
            }
        }
	}
}
