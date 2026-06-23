using UnityEngine;

public class BasicAttack : Attack
{
    [SerializeField] float damage;
    private Collider2D myCollider;
    private float hitDuration;
    private float hitActivationTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        myCollider.enabled = false;
        hitDuration = 0.5f;
        hitActivationTime = 0;
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
            }
        }
    }

    public void Trigger(float damageModifier)
    {
        myCollider.enabled = true;
    }
}
