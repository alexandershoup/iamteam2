using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class Planet : MonoBehaviour
{
    // TODO: add follow behavior
    public float rotationSpeed = 6;


    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed));
    }
}
