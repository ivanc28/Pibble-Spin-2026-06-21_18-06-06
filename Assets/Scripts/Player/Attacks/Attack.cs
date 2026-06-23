using UnityEngine;

public abstract class Attack : ScriptableObject
{
    [SerializeField] float damage;
    public void Trigger(float damageModifier){}
}
