using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{ 
public enum EWeponType
{
    main,
    plasm,
    rocket
    
}
    public interface IWeapon 
    {
        EWeponType GetWeaponType(EWeponType eWeponTipe);
    }
}

