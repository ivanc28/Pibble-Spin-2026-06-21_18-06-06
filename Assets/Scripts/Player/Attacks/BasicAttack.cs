using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Attack
{
    private Collider2D myCollider;
    private float hitDuration;
    private float hitActivationTimer;
    private float modifiedDamage;

    public static BasicAttack Instance { get; set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        myCollider = GetComponent<Collider2D>();
        myCollider.enabled = false;
        hitDuration = 0.1f;
        hitActivationTimer = 0;
        modifiedDamage = damage;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (myCollider.enabled)
        {
            hitActivationTimer += 1 * Time.fixedDeltaTime;
            if (hitActivationTimer >= hitDuration)
            {
                hitActivationTimer = 0;
                myCollider.enabled = false;
                Debug.Log("deactivate collider");
            }
        }
    }

    public override void Trigger()
    {
        myCollider.enabled = true;
        Debug.Log("activate collider");
    }
    public void ApplyDamageModifier(float damageModifier)
    {
        modifiedDamage = damage * damageModifier;
    }
}
