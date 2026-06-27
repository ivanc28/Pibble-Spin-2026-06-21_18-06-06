using UnityEngine;

public class Tornado : Projectile
{    
    private bool stationary;
    [SerializeField] float secondsPerTick;
    private float tickTimer;
    private float lifeTimer;
    [SerializeField] float lifetime;
    private Collider2D collider;

    public override void Init(float modifiedDamage)
    {
        base.Init(modifiedDamage);
        piercing = true;
        stationary = false;
        tickTimer = 0;
        collider = GetComponent<Collider2D>();
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
                collider.enabled = false;
                collider.enabled = true;
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
        }
    }

    public override void EndOfLifespanBehavior()
    {
        stationary = true;
        rb.linearVelocity = new Vector2(0,0);
    }
}
