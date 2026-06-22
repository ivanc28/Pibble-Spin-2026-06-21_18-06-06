using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Components")]
    public PlayerData data;
    public Rigidbody2D rb;
    public PlayerInventory inventory;
    private Vector2 moveInput;
    private float health;
    private bool canMove;
    private float pickupRange;
    public static Player Instance { get; set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        inventory = new();
        health = data.baseHealth;
        canMove = true;
        pickupRange = data.basePickupRange;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }
    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }
        if(health <= 0)
        {
            Die();
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        // Calculate the direction we want to move in and our desired velocity
        Vector2 normalizedMoveInput = moveInput.normalized;
        Vector2 targetSpeed = normalizedMoveInput * data.moveSpeed;

        #region Calculate AccelRate
        float accelRate;

        accelRate = (Mathf.Abs(targetSpeed.magnitude) > 0.01f) ? data.moveAccelAmount : data.moveDecelAmount;
        #endregion

        //Calculate difference between current velocity and desired velocity
        Vector2 velocityDif = targetSpeed - rb.linearVelocity;
        Vector2 movement = velocityDif * accelRate;

        rb.AddForce(movement, ForceMode2D.Force);
    }
    public bool ItemInRange(Transform item)
    {
        return Vector2.Distance(item.position, transform.position) <= pickupRange;
    }
    public void DamagePlayer(float dmg)
    {
        health -= dmg;
    }
    public void Die()
    {
        Debug.Log("you ded");
        canMove = false;
    }
}
