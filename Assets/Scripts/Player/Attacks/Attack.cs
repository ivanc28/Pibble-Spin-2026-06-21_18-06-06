using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    float damage;
    public abstract void Trigger(float damageModifier);
}
