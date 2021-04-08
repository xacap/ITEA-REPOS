using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class CLeaveTrigger : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            CLevelGenerator.instance.AddPiece();
            CLevelGenerator.instance.RemoveOldestPiece();
        }
    }
}

