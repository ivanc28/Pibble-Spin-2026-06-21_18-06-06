using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableData/Player/Player Data")]
public class PlayerData : ScriptableObject
{
    public float moveSpeed;
    public float moveAccelAmount;
    public float moveDecelAmount;
    public float baseHealth;

    public float basePickupRange;
}
