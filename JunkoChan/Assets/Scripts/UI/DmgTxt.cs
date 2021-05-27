using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgTxt : MonoBehaviour
{
    public TextMesh DmgText;
    
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 2f);
    }

    public void DisplayDamage(float potatoDmg, bool isCritical)
    {
        if (isCritical)
        {
            DmgText.text = "<color=#ff0000>" + "-" + potatoDmg + "</color>";
        }
        else 
        {
            DmgText.text = "<color=#ffffff>" + "-" + potatoDmg + "</color>";
        }
    }
}
