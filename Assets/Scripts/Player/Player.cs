using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

using TMPro;
using UnityEngine.Serialization;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Components")]
    public PlayerData data;
    public Rigidbody2D rb;
    public SpriteRenderer playerRenderer;
    public Animator anim;
    public PlayerInventory inventory;
    public PlayerStats stats;
    private Vector2 moveInput;
    private float maxHealth;
    private float health;
    private float healthRegen;
    private float regenTimer;
    private float secondsPerRegen;
    private bool canMove;
    private float pickupRange;
    private int killCount;
    private int killCountThreshhold;
    private int killCountLvlUps;
    public static Player Instance { get; private set; }

    private bool isSpinning;
    private float spinCharge;
    private float maxSpinCharge;
    private float spinChargeDecreaseRate;
    private float spinChargeIncreaseRate;
    private float spinsPerSecond;
    private float attackTriggerTimer;
    private bool isIFraming;
    
    [SerializeField] List<Attack> allAttacks = new List<Attack>();
    private List<Attack> availableAttacks = new List<Attack>();
    private int maxAttacks;
    // [SerializeField] GameObject[] availableAttacks;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        inventory = new();
        stats = new();

        maxHealth = data.baseHealth;
        healthRegen = data.baseHealthRegen;
        secondsPerRegen = 5;
        regenTimer = 0;
        ResetHP();
        canMove = true;
        isSpinning = false;
        maxSpinCharge = data.maxSpinCharge;
        spinCharge = maxSpinCharge;
        spinChargeDecreaseRate = data.spinChargeDecreaseRate;
        spinChargeIncreaseRate = data.spinChargeIncreaseRate;
        spinsPerSecond = data.baseSpinsPerSecond;
        attackTriggerTimer = 0;
        maxAttacks = data.maxAttacks;
        killCountThreshhold = data.baseKillCountThreshhold;
        pickupRange = data.basePickupRange;
        allAttacks.Sort((x, y) => x.weapon.CompareTo(y.weapon));
        availableAttacks.Add(allAttacks[0]);

        // availableAttacks.Add(allAttacks[1]);
        availableAttacks.Add(allAttacks[3]);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // ---- CALLED BY INPUT SYSTEM -------
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }
    public void Spin()
    {
        if (!isSpinning && spinCharge < 0.01f) 
        { 
            return;
        }
        ToggleSpin();
    }
    // -----------------------------------

    // Update is called once per frame
    void Update()
    {
        if (ShouldDisableMovement())
        {
            return;
        }
        #region Animations
        anim.SetFloat("moveSpeed", rb.linearVelocity.magnitude);
        if(rb.linearVelocityX > 0)
        {
            playerRenderer.flipX = false;
        }
        else if (rb.linearVelocityX < 0)
        {
            playerRenderer.flipX = true;
        }
        #endregion
        #region Death
        if (health <= 0)
        {
            Die();
        }
        #endregion
        #region Kills Level Up
        if (killCount >= killCountThreshhold)
        {
            killCountLvlUps++;
            killCountThreshhold = CalculateKillCountThreshhold(data.baseKillCountThreshhold, killCountLvlUps, data.killCountCoefficient, data.killCountPower);
            IncreaseSpinSpeed(0.05f);
            SpawnFadingText();
            killCount = 0;
        }
        #endregion
        //if (Keyboard.current.spaceKey.wasPressedThisFrame)
        //{
        //    Debug.Log("Space Key Pressed!");
        //    ToggleSpin();
        //}

    }
    private void FixedUpdate()
    {
        if (ShouldDisableMovement())
        {
            return;
        }
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
                attackTriggerTimer = 1 / spinsPerSecond;
            }
            else
            {
                attackTriggerTimer -= 1 * Time.fixedDeltaTime;
            }
            if (spinCharge < 0)
            {
                spinCharge = 0;
                if (isSpinning)
                {
                    ToggleSpin();
                }
            }
        }
        else if (spinCharge <= maxSpinCharge + stats.spinLifetimeIncrease)
        {
            ((BasicAttack)availableAttacks[0]).Activate(false);
            spinCharge += (spinChargeIncreaseRate + stats.spinRechargeRateIncrease) * Time.fixedDeltaTime;
            if (spinCharge > maxSpinCharge + stats.spinLifetimeIncrease)
            {
                spinCharge = maxSpinCharge + stats.spinLifetimeIncrease;
            }
        }

        if (health < maxHealth)
        {
            if (regenTimer >= secondsPerRegen)
            {
                health += healthRegen * stats.regenRateMult;
                if (health > maxHealth)
                {
                    health = maxHealth;
                }
                regenTimer = 0;
            }
            else
            {
                regenTimer += Time.fixedDeltaTime;
            }
        }
    }
    private void Move()
    {
        // Calculate the direction we want to move in and our desired velocity
        Vector2 normalizedMoveInput = moveInput.normalized;
        Vector2 targetSpeed = normalizedMoveInput * data.moveSpeed * stats.moveSpeedMult * (isSpinning ? data.spinningMoveSpeedFactor : 1f);

        #region Calculate AccelRate
        float accelRate;
        if (isSpinning)
        {
            accelRate = (Mathf.Abs(targetSpeed.magnitude) > 0.01f) ? data.spinningAccelAmount : data.spinningDecelAmount;
        }
        else
        {
            accelRate = (Mathf.Abs(targetSpeed.magnitude) > 0.01f) ? data.moveAccelAmount : data.moveDecelAmount;
        }
        #endregion

        //Calculate difference between current velocity and desired velocity
        Vector2 velocityDif = targetSpeed - rb.linearVelocity;
        Vector2 movement = velocityDif * accelRate;

        rb.AddForce(movement, ForceMode2D.Force);
    }
    private void ToggleSpin()
    {
        isSpinning = !isSpinning;
        ((BasicAttack)availableAttacks[0]).Activate(isSpinning);
        Debug.Log(isSpinning);
    }

    public bool ItemInRange(Transform item)
    {
        return Vector2.Distance(item.position, transform.position) <= pickupRange * stats.pickupRangeMult;
    }
    public bool StructureInRange(Transform structure, float distance)
    {
        return Vector2.Distance(structure.position, transform.position) <= distance;
    }
    /// <summary>
    /// Increases max HP by the proportion, heals the player by how much we increased HP by.
    /// </summary>
    /// <param name="proportion"></param>
    public void IncreaseMaxHPAndHPStat(float proportion)
    {
        float oldMaxHealth = maxHealth;
        stats.maxHPMult += proportion;
        maxHealth = data.baseHealth * stats.maxHPMult;
        // Calculate how much max HP was increased by and add that amt to our current health
        float diff = maxHealth - oldMaxHealth;
        health += diff;
    }
    private void ResetHP()
    {
        health = maxHealth;
    }
    public void DamagePlayer(float dmg)
    {
        if (isIFraming)
        {
            return;
        }
        health -= dmg;
        isIFraming = true;
        StartCoroutine(StartIFrames());
    }
    private IEnumerator StartIFrames()
    {
        float timer = 0;
        float iFrameLengthCalc = data.iFrameTime;
        while (timer < iFrameLengthCalc)
        {
            Shader defaultShader = playerRenderer.material.shader;
            // Flash white
            playerRenderer.material.shader = Shader.Find("GUI/Text Shader");
            yield return new WaitForSeconds(data.iFrameFlashRate / 2);
            // Flash normal
            playerRenderer.material.shader = defaultShader;
            yield return new WaitForSeconds(data.iFrameFlashRate / 2);
            timer += data.iFrameFlashRate;
        }
        isIFraming = false;
    }
    public void Die()
    {
        Debug.Log("you ded");
        canMove = false;
        GameManager.Instance.SetGamePaused(true);
        rb.linearVelocity = Vector2.zero;
        UIManager.Instance.ShowDeathScreen();
    }
    private bool ShouldDisableMovement()
    {
        return GameManager.Instance.IsPaused || !canMove;
    }
    public List<Attack> GetAvailableAttacks()
    {
        return availableAttacks;
    }
    public float GetHealth() { return health; }
    public float GetMaxHealth() { return maxHealth; }
    public float GetSpinCharge() {  return spinCharge; }
    public float GetMaxSpinCharge() { return maxSpinCharge + stats.spinLifetimeIncrease;  }
    public void ClaimWeapon(Attack.Weapons weapon)
    {
        availableAttacks.Add(allAttacks[(int)weapon]);
    }
    public void IncreaseSpinSpeed(float increase)
    {
        spinsPerSecond += increase;
    }
    public void IncrementKillCount()
    {
        killCount++;
    }

    private int CalculateKillCountThreshhold(int baseThreshhold, int level, float coefficient, float power)
    {
        return (int)(baseThreshhold + coefficient * Mathf.Pow(level, power));
    }
    public float GetSpinsPerSecond()
    {
        return spinsPerSecond;
    }

    private void SpawnFadingText()
    {
        Instantiate(data.lvlUpText, transform.position, Quaternion.identity);
    }
}
