using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerAttack : Attack
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
        hitDuration = 4f;
        hitActivationTimer = 0;
        foreach (Transform child in transform)
        {
            child.GetComponent<Spinner>().damage = damage;
            child.GetComponent<Spinner>().ApplyDamageModifier(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (spriteRenderer.enabled)
        {
            // transform.position = Player.Instance.transform.position;
            hitActivationTimer += Time.fixedDeltaTime;
            if (hitActivationTimer >= hitDuration)
            {
                hitActivationTimer = 0;
                Activate(false);
                // Debug.Log("deactivate collider");
            }
        }
    }

    public override void Trigger()
    {
        if (!spriteRenderer.enabled)
        {
            base.Trigger();
            if (spinCounter >= spinsPerTrigger)
            {
                Activate(true);
                // Debug.Log("activate collider");
                spinCounter = 0;
            }
        }
    }
    
    private void Activate(bool activate)
    {
        if (activate)
        {
            // transform.position = Player.Instance.transform.position;
            rb.AddTorque(-1 * Mathf.PI * Player.Instance.GetSpinsPerSecond(), ForceMode2D.Impulse);
            spriteRenderer.enabled = true;
            
            foreach (Transform child in transform)
            {
                child.GetComponent<Collider2D>().enabled = true;
                child.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else
        {
            spriteRenderer.enabled = false;
            rb.angularVelocity = 0;
            foreach (Transform child in transform)
            {
                child.GetComponent<Collider2D>().enabled = false;
                child.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    public override void ApplyDamageModifier(float damageModifier)
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spinner>().ApplyDamageModifier(damageModifier);
        }
    }
}
