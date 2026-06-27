using UnityEngine;

public class Tornado : Projectile
{    
    private bool stationary;
    [SerializeField] float secondsPerTick;
    private float tickTimer;
    private float lifeTimer;
    [SerializeField] float lifetime;
    private Collider2D collider;
    private float hitDuration;
    private float hitActivationTimer;

    public override void Init(float modifiedDamage)
    {
        base.Init(modifiedDamage);
        piercing = true;
        stationary = false;
        tickTimer = 0;
        collider = GetComponent<Collider2D>();
        collider.enabled = true;
        hitDuration = 0.1f;
        hitActivationTimer = 0;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void FixedUpdate()
    {
        if (stationary)
        {
            if (tickTimer >= secondsPerTick)
            {
                collider.enabled = true;
                tickTimer = 0;
            }
            else
            {
                tickTimer += Time.fixedDeltaTime;
            }
            lifeTimer += Time.fixedDeltaTime;
            if(lifeTimer > lifetime)
            {
                Destroy(gameObject);
            }
            if (collider.enabled)
            {
                hitActivationTimer += Time.fixedDeltaTime;
                if (hitActivationTimer >= hitDuration)
                {
                    hitActivationTimer = 0;
                    collider.enabled = false;
                }
            }
        }
    }

    public override void EndOfLifespanBehavior()
    {
        stationary = true;
        collider.enabled = false;
        rb.linearVelocity = new Vector2(0,0);
    }
}
