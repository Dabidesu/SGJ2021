using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public interface IDamagable<T>
{
    public void Damage(T damageAmount);
}

public interface IWeapon
{
    int Rank {get; set;}
    float BaseDamage {get;}
    float Damage {get;}
    public void Fire(Player user, Vector3 userPosition, Vector3 targetPosition);
    public void DropItem(Player user, Vector3 userPosition, Vector3 targetPosition);
}

public interface IRangedWeapon : IWeapon
{
    public float ProjectileSpeed {get;}
}

public interface IMeleeWeapon : IWeapon
{
        
}

public interface IMagicWeapon : IWeapon
{
    float ManaCost {get;}
}

public interface IPhysical : IWeapon
{
    float StaminaCost {get;}
}


