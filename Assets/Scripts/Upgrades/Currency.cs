using UnityEngine;

public class Currency : MonoBehaviour
{
    public int currencyAmt;
    public float moveSpeed;
    private void Update()
    {
        if (Player.Instance.ItemInRange(transform))
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, moveSpeed * Time.deltaTime);
        }
    }
    private void Collect()
    {
        Player.Instance.inventory.IncreaseCurrency(currencyAmt);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collect();
            Destroy(gameObject);
        }
    }
}
