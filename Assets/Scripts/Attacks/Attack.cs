using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public enum Weapons { Basic, Shuriken, Buzzsaw, Tornado, Revolving_Blade };
    public Weapons weapon;
    public float damage;
    public int spinsPerTrigger;
    public int spinCounter;
    public float attackRange;
    private float modifiedDamage;

    protected virtual void Start()
    {
        modifiedDamage = damage;
        spinCounter = 0;
    }
    public virtual void Trigger()
    {
        spinCounter += 1;
    }
    public virtual void ApplyDamageModifier(float damageModifier)
    {
        modifiedDamage = damage * damageModifier;
    }
    public float GetDamage()
    {
        return modifiedDamage;
    }
}
