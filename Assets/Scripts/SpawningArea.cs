using UnityEngine;
using System.Collections;

public class SpawningArea : MonoBehaviour {

    public GameObject ObjectToSpawn;
    public float RateOfSpawn = 1;

    private float nextSpawn = 0;

    public float range = 100.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + RateOfSpawn;

            // Random position within this transform
            Vector3 rndPosWithin;
            rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(range * -1, range));
            rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
            GameObject snowman = (GameObject)Instantiate(ObjectToSpawn, rndPosWithin, transform.rotation);
            snowman.GetComponent<Snowman>().Scale();
        }
	}
}
