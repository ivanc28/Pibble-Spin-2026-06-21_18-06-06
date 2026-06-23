using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Attack
{
    private Collider2D myCollider;
    private float hitDuration;
    private float hitActivationTime;

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
        hitActivationTime = 0;
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
            hitActivationTime += 1 * Time.fixedDeltaTime;
            if (hitActivationTime >= hitDuration)
            {
                hitActivationTime = 0;
                myCollider.enabled = false;
                Debug.Log("deactivate collider");
            }
        }
    }

    public override void Trigger(float damageModifier)
    {
        myCollider.enabled = true;
        Debug.Log("activate collider");
    }
}
