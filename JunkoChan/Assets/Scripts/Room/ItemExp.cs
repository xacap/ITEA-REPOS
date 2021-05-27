using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Room
{
    public class ItemExp : MonoBehaviour
    {
        GameObject Player;

        void Start()
        {
            Player = PlayerData.Instance.Player;

            StartCoroutine(WaitClearRoom());
        }

        IEnumerator WaitClearRoom()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                while (transform.parent.gameObject.GetComponent<RoomCondition>().isClearRoom)
                {
                    transform.position = Vector3.Lerp(transform.position, Player.transform.position, 0.2f);
                    yield return null;
                }

            }
        }
    }
}

