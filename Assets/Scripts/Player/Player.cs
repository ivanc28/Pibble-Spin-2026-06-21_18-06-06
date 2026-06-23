using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

using TMPro;

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

    private bool isSpinning;
    private float spinCharge;
    private float maxSpinCharge;
    private float spinChargeDecreaseRate;
    private float spinChargeIncreaseRate;
    private float spinsPerSecond;
    private float attackTriggerTimer;

    [SerializeField] List<Attack> availableAttacks = new List<Attack>();
    private int maxAttacks;
    // [SerializeField] GameObject[] availableAttacks;

    public TextMeshProUGUI textComponent; // on screen counter for spin charge

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
        isSpinning = false;
        maxSpinCharge = data.maxSpinCharge;
        spinCharge = maxSpinCharge;
        spinChargeDecreaseRate = data.spinChargeDecreaseRate;
        spinChargeIncreaseRate = data.spinChargeIncreaseRate;
        spinsPerSecond = data.baseSpinsPerSecond;
        attackTriggerTimer = 0;
        maxAttacks = data.maxAttacks;
        pickupRange = data.basePickupRange;
        availableAttacks.Add(BasicAttack.Instance);
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
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Space Key Pressed!");
            ToggleSpin();
        }
        textComponent.text = "charge: " + spinCharge.ToString();
    }
    private void FixedUpdate()
    {
        Move();
        if (isSpinning && spinCharge > 0)
        {
            spinCharge -= spinChargeDecreaseRate * Time.fixedDeltaTime;
            if (attackTriggerTimer <= 0)
            {
                foreach (Attack a in availableAttacks)
                {
                    a.Trigger();
                }
                attackTriggerTimer = 1/spinsPerSecond;
            }
            else
            {
                attackTriggerTimer -= 1 * Time.fixedDeltaTime;
            }
            if (spinCharge < 0)
            {
                spinCharge = 0;
                ToggleSpin();
            }
        }
        else if (spinCharge <= maxSpinCharge)
        {
            spinCharge += spinChargeIncreaseRate * Time.fixedDeltaTime;
            if (spinCharge > maxSpinCharge)
            {
                spinCharge = maxSpinCharge;
            }
        }
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
    private void ToggleSpin()
    {
        isSpinning = !isSpinning;
        Debug.Log(isSpinning);
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
