using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public Rigidbody2D rb;
    public Animator anim;
    private float moveSpeed;
    private int currencyDropped;
    private float spinSpeedIncrease;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // anim = gameObject.GetComponent<Animator>();
        moveSpeed = Random.Range(data.minMoveSpeed, data.maxMoveSpeed);
        currencyDropped = Random.Range(data.minCurrencyDropped, data.maxCurrencyDropped + 1);
        transform.parent.GetComponent<SpawnManager>().IncrementEnemyCount(1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDir = Player.Instance.transform.position - transform.position;
        rb.linearVelocity = moveDir.normalized * moveSpeed;
        if(Mathf.Abs(rb.angularVelocity) >= data.killSpinSpeed)
        {
            Player.Instance.IncrementKillCount();
            Die();
        }
        // TESTING ONLY:
        //if (Mouse.current.leftButton.wasPressedThisFrame && !GameManager.Instance.IsPaused)
        //{
        //    Die();
        //}
    }
    private void Die()
    {
        StartCoroutine(DieLogic());
    }
    private IEnumerator DieLogic()
    {
        transform.parent.GetComponent<SpawnManager>().IncrementEnemyCount(-1);
        SpawnCurrency((int)(currencyDropped * Player.Instance.stats.currencyDropMult));
        yield return null;
        Destroy(gameObject);
    }
    private void SpawnCurrency(int amtDropped)
    {
        // Calculate an increment value based on this example: if coins are 1, 5, 10, then we do powers of 3, 9*1 + 3*5 + 1*10 = 34, so incrementValue = 34
        int incrementValue = 0;
        int numCoinsUsing = data.currencyValues.Length;
        for(int numCoinsTrying = numCoinsUsing;  numCoinsTrying > 0; numCoinsTrying--)
        {
            for (int i = 0; i < numCoinsTrying; i++)
            {
                incrementValue += ((int)Mathf.Pow(3, numCoinsTrying - i - 1)) * data.currencyValues[i].currencyAmt;
            }
            if(incrementValue <= amtDropped)
            {
                numCoinsUsing = numCoinsTrying;
                break;
            }
            else
            {
                incrementValue = 0;
            }
        }
        int startingSpawnCountOfEachCurrency = amtDropped / incrementValue;
        int remainingCurrency = amtDropped % incrementValue;
        for(int i = 0; i < numCoinsUsing; i++)
        {
            InstantiateCurrency(i, startingSpawnCountOfEachCurrency * ((int)Mathf.Pow(3, numCoinsUsing - i - 1)));
        }

        // greedy coin change algorithm for remaining currency
        for (int coinIndex = numCoinsUsing - 1; coinIndex >= 0; coinIndex--)
        {
            int count = remainingCurrency / data.currencyValues[coinIndex].currencyAmt;
            for(int i = 0; i < count; i++)
            {
                InstantiateCurrency(coinIndex, 1);
            }
            remainingCurrency %= data.currencyValues[coinIndex].currencyAmt;
        }
    }

    private void InstantiateCurrency(int index, int numTimes)
    {
        for(int i = 0; i < numTimes; i++)
        {
            Currency currency = Instantiate(data.currencyValues[index], transform.position, Quaternion.identity);
            currency.SpawnAtRandomSpeed();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.DamagePlayer(data.contactDamage);
        }

        if (collision.gameObject.CompareTag("Attack"))
        {
            if (!anim.GetBool("isDamaged"))
            {
                anim.SetBool("isDamaged", true);
                rb.angularVelocity = 0;
            }
            Debug.Log("enemy hit");
            float damage = 0;
            if (collision.gameObject.TryGetComponent<Projectile>(out Projectile projectile))
            {
                damage = projectile.GetDamage();
                if (!projectile.IsPiercing())
                {
                    projectile.EndOfLifespanBehavior();
                }
            }
            else
            {
                damage = collision.gameObject.GetComponent<Attack>().GetDamage();
            }
            rb.AddTorque(damage, ForceMode2D.Impulse);
        }
    }
}
