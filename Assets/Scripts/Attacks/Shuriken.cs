using UnityEngine;

public class Shuriken : Projectile
{    
    public override void Init(float modifiedDamage)
    {
        base.Init(modifiedDamage);
        piercing = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
}
