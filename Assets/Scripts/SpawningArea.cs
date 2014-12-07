using UnityEngine;
using System.Collections;

public class SpawningArea : MonoBehaviour {

    public GameObject ObjectToSpawn;
    float RateOfSpawn;
    public int spawnMultiplier = 10;

    private float nextSpawn = 0;

    public float range = 100.0f;
    GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        int numberToSpawn = (27 - gameManager.numDays) * spawnMultiplier;
        RateOfSpawn = gameManager.dayLength / numberToSpawn;
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameManager.dayTime && Time.time > nextSpawn)
        {
            nextSpawn = Time.time + RateOfSpawn;

            Vector3 rndPosWithin;
            rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(range * -1, range));
            rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
            GameObject snowman = (GameObject)Instantiate(ObjectToSpawn, rndPosWithin, transform.rotation);
            snowman.GetComponent<Snowman>().Scale();
        }
	}
}
