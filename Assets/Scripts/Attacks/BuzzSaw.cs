using UnityEngine;

public class BuzzSaw : Projectile
{    
    public override void Init(float modifiedDamage)
    {
        base.Init(modifiedDamage);
        piercing = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
}
