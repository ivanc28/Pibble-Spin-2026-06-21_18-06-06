using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableData/Player/Player Data")]
public class PlayerData : ScriptableObject
{
    public float moveSpeed;
    public float moveAccelAmount;
    public float moveDecelAmount;
    public float baseHealth;
    public float maxSpinCharge;
    public float spinChargeDecreaseRate;
    public float spinChargeIncreaseRate;
    public float baseSpinsPerSecond;
    public int maxAttacks;

    public float basePickupRange;
}
