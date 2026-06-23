using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public float damage;
    public int spinsPerTrigger;
    public int spinCounter;
    public float attackRange;
    public virtual void Trigger()
    {
        spinCounter += 1;
    }
}
