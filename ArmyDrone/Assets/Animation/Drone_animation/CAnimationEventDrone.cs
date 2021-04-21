using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAnimationEventDrone : MonoBehaviour
{
   private void AddExplosionEffect(GameObject _go)
    {
       Instantiate(_go,  this.transform);
    }

    private void AddExplosionElectro(GameObject _go)
    {
        Instantiate(_go, this.transform);
    }
}
