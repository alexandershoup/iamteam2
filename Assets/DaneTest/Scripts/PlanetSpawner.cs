using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] BodySourceManager manager;
    [SerializeField] GameObject[] planets;

    IDictionary<ulong, Assets.DaneTest.Scripts.Plan9> uniquePlanets = new Dictionary<ulong, Assets.DaneTest.Scripts.Plan9>();

    //List<ulong> trackingIDs = new List<ulong>();

    //float[] xPos = new float[6];
    //float[] yPos = new float[6];
    //float[] zPos = new float[6];

    //bool canSpawn;

    void Update()
    {
        // TODO: wherever it is, make it so that weird Earth doesn't appear behind everything

        if (manager.GetData() != null)
        {
            for (int i = 0; i < manager.GetData().Length; i++)
            {
                Body body = manager.GetData()[i];

                if (body.IsTracked)
                {
                    //canSpawn = false;
                    if (!uniquePlanets.ContainsKey(body.TrackingId))
                    {
                        StartCoroutine(SpawnPlanet(body.TrackingId));
                        //canSpawn = true;
                    }
                    else MovePlanet(body);

                    //if (canSpawn) StartCoroutine(SpawnPlanet(body.TrackingId));

                }
            }
        }
        //Here we will check each planet to see if it's corresponding body is still being tracked
        foreach (var item in uniquePlanets)
        {
            bool idExists = false;
            for (int i = 0; i < manager.GetData().Length; i++)
            {
                if (item.Key == manager.GetData()[i].TrackingId)
                {
                    idExists = true;
                }
            }
            if (!idExists)
            {
                Destroy(uniquePlanets[item.Key].planet);
                uniquePlanets.Remove(item.Key);
            }
        }
    }

    IEnumerator SpawnPlanet(ulong tID)
    {
        yield return new WaitForEndOfFrame();
        GameObject newPlanet = Instantiate(planets[Random.Range(0, planets.Length)]);
        Assets.DaneTest.Scripts.Plan9 uniquePlanet = new Assets.DaneTest.Scripts.Plan9();
        uniquePlanet.planet = newPlanet;
        // TODO: if planets does not already have an instance of this planet
        uniquePlanets.Add(tID, uniquePlanet);

        // TODO: below lines are for making a planet fade in, because I want it to be fancy
        //float timer = 3;
        //float elapsedTime = 0;
        //while (elapsedTime <= timer)
        //{
        //    // fade in planet
        //    elapsedTime += Time.deltaTime;
        //}

        //canSpawn = false;
    }

    void DestroyPlanet()
    {
        // TODO: when it's offscreen, destroy the planet
    }

    void MovePlanet(Body body)
    {
        uniquePlanets[body.TrackingId].xPos = body.Joints[JointType.SpineMid].Position.X;
        uniquePlanets[body.TrackingId].yPos = body.Joints[JointType.SpineMid].Position.Y;
        uniquePlanets[body.TrackingId].zPos = body.Joints[JointType.SpineMid].Position.Z;

        uniquePlanets[body.TrackingId].planet.transform.position = new Vector3(uniquePlanets[body.TrackingId].xPos * -10, 0, uniquePlanets[body.TrackingId].zPos * -3);
    }
}
