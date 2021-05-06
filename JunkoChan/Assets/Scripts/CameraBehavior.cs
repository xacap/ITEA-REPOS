using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public static CameraBehavior Instance // singlton     
    {
        get
        {
            if (Instance == null )
            {
                instance = FindObjectOfType<CameraBehavior>();
                if (instance == null )
                {
                    var instanceContainer = new GameObject("CameraBehavior");
                    instance = instanceContainer.AddComponent<CameraBehavior>( );
                }
            }
            return instance;
        }
    }
    private static CameraBehavior instance;

    public GameObject Player;

    public float offsetY = 45f;
    public float offsetZ = -40f;

    Vector3 cameraPosition;

    void LateUpdate()
    {
        cameraPosition.y = Player.transform.position.y + offsetY;
        cameraPosition.z= Player.transform.position.z + offsetZ;

        transform.position = cameraPosition;
    }

    public void CarmeraNextRoom()
    {
        //Fade in/out
        cameraPosition.x = Player.transform.position.x;
    }
}
