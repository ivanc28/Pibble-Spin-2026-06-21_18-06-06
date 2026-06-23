using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] float damage;
    public abstract void Trigger(float damageModifier);
}
