using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "ScriptableData/Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float moveSpeed;
    public float contactDamage;
    public float killSpinSpeed;
    public int currencyDropped;
    public Currency[] currencyValues;
}
