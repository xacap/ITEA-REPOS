using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCamera : MonoBehaviour
{
    [SerializeField] private float moveCameraSpeed = 0.2f;

    private void Update()
    {
        Vector3 forwardRun = this.transform.position + this.transform.up * moveCameraSpeed * Time.fixedDeltaTime;
        this.transform.position = forwardRun;
    }
}
