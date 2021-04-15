using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{ 
public enum EWeponType
{
    main,
    rocket,
    lazer
}
    public interface IWeapon 
    {
        EWeponType GetWeaponType(EWeponType eWeponTipe);
    }
}

