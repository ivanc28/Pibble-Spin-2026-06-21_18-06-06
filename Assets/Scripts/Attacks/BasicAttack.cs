using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Attack
{
    private CircleCollider2D myCollider;
    private float hitDuration;
    private float hitActivationTimer;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.enabled = false;
        spriteRenderer.enabled = false;
        hitDuration = 0.1f;
        hitActivationTimer = 0;
        rb = GetComponent<Rigidbody2D>();

        myCollider.radius = attackRange;
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
                // Debug.Log("deactivate collider");
            }
        }
    }

    public override void Trigger()
    {
        base.Trigger();
        if (spinCounter >= spinsPerTrigger)
        {
            myCollider.enabled = true;
            // Debug.Log("activate collider");
            spinCounter = 0;
        }
    }
    
    public void Activate(bool activate)
    {
        if (activate)
        {
            rb.AddTorque(Player.Instance.GetSpinsPerSecond() * 1.5f, ForceMode2D.Impulse);
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
            rb.angularVelocity = 0;
        }
    }
}
