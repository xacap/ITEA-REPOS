using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Room;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public static PlayerMovement Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PlayerMovement>();
                    if (instance == null)
                    {
                        var instanceContainer = new GameObject("PlayerMovement");
                        instance = instanceContainer.AddComponent<PlayerMovement>();
                    }
                }
                return instance;
            }
        }
        private static PlayerMovement instance;


        private Rigidbody _rb;
        public float moveSpeed = 5f;
        public Animator Anim;

        public void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            Anim = GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            if (JoystickMove.Instance.JoyVec.x != 0 || JoystickMove.Instance.JoyVec.y != 0)
            {
                _rb.velocity = new Vector3(JoystickMove.Instance.JoyVec.x, 0, JoystickMove.Instance.JoyVec.y) * moveSpeed;
                _rb.rotation = Quaternion.LookRotation(new Vector3(JoystickMove.Instance.JoyVec.x, 0, JoystickMove.Instance.JoyVec.y));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("NextRoom"))
            {
                Debug.Log(" Get Next Room ");
                StageMgr.Instance.NextStage();
            }
        }
    }
}


