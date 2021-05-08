using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Player;

namespace UI
{
    public class JoystickMove : MonoBehaviour
    {
        public static JoystickMove Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<JoystickMove>();
                    if (instance == null)
                    {
                        var instanceContainer = new GameObject("JoystickMove");
                        instance = instanceContainer.AddComponent<JoystickMove>();
                    }
                }
                return instance;
            }
        }
        public static JoystickMove instance;

        public GameObject Joystick;
        public GameObject Background;
        Vector3 StickFirstPosition;
        public Vector3 JoyVec;
        Vector3 JoystickFirstPosition;
        float StickRadius;

        public bool isPlayerMoving = false;


        private void Awake()
        {
            StickRadius = Background.gameObject.GetComponent<RectTransform>().sizeDelta.y / 3;
            JoystickFirstPosition = Background.transform.position;
        }
        

        public void PointDown() 
        {
            Background.transform.position = Input.mousePosition;
            Joystick.transform.position = Input.mousePosition;
            StickFirstPosition = Input.mousePosition;

            if (!PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                PlayerMovement.Instance.Anim.SetBool("Attack", false);
                PlayerMovement.Instance.Anim.SetBool("Idle", false);
                PlayerMovement.Instance.Anim.SetBool("Run", true);
            }

            isPlayerMoving = true;
        }

        public void Drag(BaseEventData baseEventData)
        {
            PointerEventData pointerEventData = baseEventData as PointerEventData;
            Vector3 DragPosition = pointerEventData.position;
            JoyVec = (DragPosition - StickFirstPosition).normalized;

            float stickDistance = Vector3.Distance(DragPosition, StickFirstPosition);
            if (stickDistance < StickRadius)
            {
                Joystick.transform.position = StickFirstPosition + JoyVec * stickDistance;
            }
            else
            {
                Joystick.transform.position = StickFirstPosition + JoyVec * StickRadius;
            }
        }

        public void Drop()
        {
            JoyVec = Vector3.zero;
            Background.transform.position = JoystickFirstPosition;
            Joystick.transform.position = JoystickFirstPosition;

            if (!PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                PlayerMovement.Instance.Anim.SetBool("Attack", false);
                PlayerMovement.Instance.Anim.SetBool("Run", false);
                PlayerMovement.Instance.Anim.SetBool("Idle", true);
            }

            isPlayerMoving = false;
        }
       
    }
}


