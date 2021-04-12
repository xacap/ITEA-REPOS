using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class CLeaveTrigger : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            CLevelGenerator.instance.AddPiece();
            CLevelGenerator.instance.RemoveOldestPiece();
        }
    }
}

