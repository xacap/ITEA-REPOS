using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBehavior : MonoBehaviour
{
    public static CameraBehavior Instance    
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
    public Image FadeInOutImg;

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
        StartCoroutine(FadeInOut());
        cameraPosition.x = Player.transform.position.x;
    }

    IEnumerator FadeInOut()
    {
        float a = 1;
        FadeInOutImg.color = new Vector4(1, 1, 1, a);
        yield return new WaitForSeconds(0.3f);

        while (a >= 0)
        {
            FadeInOutImg.color = new Vector4(1, 1, 1, a);
            a -= 0.02f;
            yield return null;
        }
    }
}
