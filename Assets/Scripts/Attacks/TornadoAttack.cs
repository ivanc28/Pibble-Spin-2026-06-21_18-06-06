using UnityEngine;

public class TornadoAttack : Attack
{
    private CircleCollider2D myCollider;
    private bool shooting;
    private Rigidbody2D rb;
    [SerializeField] GameObject projectile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.radius = attackRange;
        myCollider.enabled = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
    private Transform FindNearestTarget()
    {
        // 1. Scan for all colliders inside the detection radius
        Collider2D[] targetsInRadius = Physics2D.OverlapCircleAll(Player.Instance.transform.position, attackRange, LayerMask.GetMask("Enemy"));
        Debug.Log(targetsInRadius);
        
        Transform nearestTarget = null;
        float closestDistance = Mathf.Infinity;

        // 2. Loop through targets to identify the absolute closest one
        foreach (Collider2D targetCollider in targetsInRadius)
        {
            Debug.Log("collider");
            Debug.Log(targetCollider.transform.position);
            float distanceToTarget = Vector2.Distance(transform.position, targetCollider.transform.position);
            // if (targetCollider.gameObject.CompareTag("Enemy"))
            // {
            //     Debug.Log("enemy found");
            // }
            
            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                nearestTarget = targetCollider.transform;
            }
        }

        // 3. Lock onto the target found
        return nearestTarget;
    }

    public override void Trigger()
    {
        base.Trigger();
        if (spinCounter >= spinsPerTrigger)
        {
            myCollider.enabled = true;
            Transform target = FindNearestTarget();
            if (target != null)
            {
                GameObject tornado = Instantiate(projectile, transform.position, transform.rotation, null);
                tornado.SetActive(true);
                tornado.GetComponent<Tornado>().Init(base.GetDamage());
                tornado.GetComponent<Tornado>().ShootAt(target.position);

                spinCounter = 0;
            }
        }
    }
}
