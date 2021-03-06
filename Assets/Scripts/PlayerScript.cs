﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayerScript : MonoBehaviour {

    public float speed = 50.0f;
    public Transform projectileSpawn;
    public GameObject projectile;
    public Camera camera;

	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectile, projectileSpawn.position, gameObject.transform.rotation);
            GetComponent<AudioSource>().Play();
        }
    }
	
	// Update is called once per frame
    void FixedUpdate()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput < 0.3f && horizontalInput > -0.3f)
            horizontalInput = 0f;

        if (verticalInput < 0.3f && verticalInput > -0.3f)
            verticalInput = 0f;

        Vector3 horizontalDirection = camera.transform.right * horizontalInput;
        Vector3 verticalDirection = camera.transform.forward * verticalInput;

        Vector3 direction = horizontalDirection + verticalDirection;
        direction.y = 0;
        direction.Normalize();

        GetComponent<Rigidbody>().AddForce(direction * speed);

        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
	}
}
