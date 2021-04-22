using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAnimationEventDrone : MonoBehaviour
{
    private GameObject mParrentGo;

    private void Awake()
    {
        mParrentGo = transform.parent.gameObject;
    }

    private void AddExplosionEffect(GameObject _go)
    {
       Instantiate(_go,  this.transform);
        Destroy(mParrentGo, 1f);
    }

    private void AddExplosionElectro(GameObject _go)
    {
        Instantiate(_go, this.transform);
    }

    private void DestroyParrent()
    {
        Destroy(mParrentGo);

    }
}
