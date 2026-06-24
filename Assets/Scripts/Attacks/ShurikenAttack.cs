using UnityEngine;

public class ShurikenAttack : Attack
{
    private CircleCollider2D myCollider;
    private float modifiedDamage;
    private bool shooting;

    // public static ShurikenAttack Instance { get; set; }

    // private void Awake()
    // {
    //     if(Instance != null && Instance != this)
    //     {
    //         Destroy(gameObject);
    //         return;
    //     }
    //     Instance = this;
    //     myCollider = GetComponent<CircleCollider2D>();
    //     myCollider.enabled = false;
    //     modifiedDamage = damage;

    //     myCollider.radius = attackRange;
    // }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.enabled = false;
        modifiedDamage = damage;

        myCollider.radius = attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Transform FindNearestTarget()
    {
        // 1. Scan for all colliders inside the detection radius
        Collider2D[] targetsInRadius = Physics2D.OverlapCircleAll(transform.position, attackRange, 3);
        
        Transform nearestTarget = null;
        float closestDistance = Mathf.Infinity;

        // 2. Loop through targets to identify the absolute closest one
        foreach (Collider2D targetCollider in targetsInRadius)
        {
            // Debug.Log("collider");
            float distanceToTarget = Vector2.Distance(transform.position, targetCollider.transform.position);
            // Debug.Log(distanceToTarget);
            
            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                nearestTarget = targetCollider.transform;
            }
        }

        // 3. Lock onto the target found
        return nearestTarget;
    }
    private void ShootAt(Vector2 target)
    {

    }

    public override void Trigger()
    {
        base.Trigger();
        if (spinCounter >= spinsPerTrigger)
        {
            myCollider.enabled = true;
            Transform target = FindNearestTarget();
            // Debug.Log("target");
            // Debug.Log(target.position);
            spinCounter = 0;
        }
    }
}
