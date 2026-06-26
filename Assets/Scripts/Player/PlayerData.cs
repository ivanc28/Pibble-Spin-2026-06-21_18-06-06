using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableData/Player/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed;
    public float moveAccelAmount;
    public float moveDecelAmount;
    [Header("Health")]
    public float baseHealth;
    public float iFrameTime;
    [Header("Spinning")]
    public float maxSpinCharge;
    public float spinChargeDecreaseRate;
    public float spinChargeIncreaseRate;
    public float baseSpinsPerSecond;
    [Header("Attacks")]
    public int maxAttacks;
    [Header("Utility")]
    public float basePickupRange;
}
