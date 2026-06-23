using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDir = Player.Instance.transform.position - transform.position;
        rb.linearVelocity = moveDir.normalized * data.moveSpeed;
        if(Mathf.Abs(rb.angularVelocity) >= data.killSpinSpeed)
        {
            //Die();
        }
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Die();
        }
    }
    private void Die()
    {
        StartCoroutine(DieLogic());
    }
    private IEnumerator DieLogic()
    {
        SpawnCurrency();
        yield return null;
        Destroy(gameObject);
    }
    private void SpawnCurrency()
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
            if(incrementValue <= data.currencyDropped)
            {
                numCoinsUsing = numCoinsTrying;
                break;
            }
            else
            {
                incrementValue = 0;
            }
        }
        int startingSpawnCountOfEachCurrency = data.currencyDropped / incrementValue;
        int remainingCurrency = data.currencyDropped % incrementValue;
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
            Debug.Log("enemy hit");
        }
    }
}
