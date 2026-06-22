using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "ScriptableData/Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float moveSpeed;
    public float contactDamage;
}
