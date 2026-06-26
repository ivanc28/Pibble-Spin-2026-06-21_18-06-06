using UnityEngine;

public class Shrine : MonoBehaviour
{
    public float interactRange;
    public GameObject upgradeCanvas;
    public GameObject purchaseCanvas;
    private int currencyRequired;
    private bool inRange;
    public void Initialize(int currency)
    {
        currencyRequired = currency;
    }
    private void Update()
    {
        inRange = Player.Instance.StructureInRange(transform, interactRange);
        if (inRange)
        {

        }
        else
        {

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
