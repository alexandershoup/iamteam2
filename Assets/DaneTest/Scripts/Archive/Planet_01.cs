using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class Planet_01 : MonoBehaviour
{
    [SerializeField]
    BodySourceManager manager;

    float[] xPos = new float[6];
    float[] yPos = new float[6];
    float[] zPos = new float[6];

    void Update()
    {
        if (manager.GetData() != null)
        {
            for (var i = 0; i < manager.GetData().Length; i++)
            {
                Body body = manager.GetData()[i];
                if (body.IsTracked)
                {
                    xPos[i] = body.Joints[JointType.SpineMid].Position.X;
                    yPos[i] = body.Joints[JointType.SpineMid].Position.Y;
                    zPos[i] = body.Joints[JointType.SpineMid].Position.Z;
                    transform.position = new Vector3(xPos[i] * -10, 0, zPos[i] * -3);
                    //print("xPos: " + xPos[i]);
                }
            }
        }


    }
}
