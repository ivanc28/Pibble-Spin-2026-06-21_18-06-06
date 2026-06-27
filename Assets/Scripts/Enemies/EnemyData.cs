using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "ScriptableData/Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float minMoveSpeed;
    public float maxMoveSpeed;
    public float contactDamage;
    public float killSpinSpeed;
    public int minCurrencyDropped;
    public int maxCurrencyDropped;
    public Currency[] currencyValues;
    public float spinSpeedIncrease;
    [Header("SFX")]
    public AudioClip[] squeakClips;
    public AudioClip[] killClip;
}
