using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class CLeaveTrigger : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null && collision.gameObject.tag == "player")
            {
                CLevelGenerator.instance.AddPiece();
                CLevelGenerator.instance.RemoveOldestPiece();
            }
        }
        
    }
}

