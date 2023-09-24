using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeinManager : MonoBehaviour
{
    public GameObject littleVein;
    public GameObject bigVein;

    public float littleVeinFreq = 10;
    public float littleVeinSpeed = 10;
    public float littleVeinSpawn = 20;
    public float bigVeinFreq = 10;
    public float bigVeinSpeed = 10;
    public float bigVeinSpawn = 20;

    float littleVeinTimer = 0;
    float bigVeinTimer = 0;

    public List<GameObject> littleVeins = new List<GameObject>();
    public List<GameObject> bigVeins = new List<GameObject>();

    void Start()
    {
        littleVeinTimer = littleVeinFreq / littleVeinSpeed;
        bigVeinTimer = bigVeinFreq / bigVeinSpeed;

		littleVeins.Add(Instantiate(littleVein, transform));
		bigVeins.Add(Instantiate(bigVein, transform));
	}

    void Update()
    {
        littleVeinTimer += Time.deltaTime;
        bigVeinTimer += Time.deltaTime;
        
        // Little Vein
        if ( littleVeinTimer > littleVeinFreq / littleVeinSpeed)
        {
            littleVeinTimer = 0;

            var vein = Instantiate(littleVein, transform);
            vein.transform.position += new Vector3(littleVeinSpawn, 0,0);
			littleVeins.Add(vein);
		}

        for (int i = 0; i < littleVeins.Count; i++)
        {
            var vein = littleVeins[i];
            vein.transform.position += new Vector3(-littleVeinSpeed * Time.deltaTime, 0, 0);

            if (vein.transform.position.x < -littleVeinSpawn)
            {
                i--;
                littleVeins.Remove(vein);
                Destroy(vein);
            }
        }

		// Big Vein
		if (bigVeinTimer > bigVeinFreq / bigVeinSpeed)
		{
			bigVeinTimer = 0;

			var vein = Instantiate(bigVein, transform);
			vein.transform.position += new Vector3(bigVeinSpawn, 0, 0);
			bigVeins.Add(vein);
		}

		for (int i = 0; i < bigVeins.Count; i++)
		{
			var vein = bigVeins[i];
			vein.transform.position += new Vector3(-bigVeinSpeed * Time.deltaTime, 0, 0);

			if (vein.transform.position.x < -bigVeinSpawn)
			{
				i--;
				bigVeins.Remove(vein);
				Destroy(vein);
			}
		}
	}
}
