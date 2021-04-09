using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Libraries;


namespace Model
{
    public class CBaseBaheviorController : MonoBehaviour, IBehaivorController
    {
        CAirClass targetObj;

        public void setAirTarget(CAirClass airClass)
        {
            targetObj = airClass;
        }
    }
}
