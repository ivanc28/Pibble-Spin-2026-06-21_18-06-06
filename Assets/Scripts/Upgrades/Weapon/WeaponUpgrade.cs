using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Upgrade", menuName = "ScriptableData/Upgrades/Weapon")]
public class WeaponUpgrade : Upgrade
{
    public Attack.Weapons weapon;
    public override void Apply(Player player)
    {
        player.ClaimWeapon(weapon);
    }
    public override bool IsAvailable()
    {
        return base.IsAvailable() && Player.Instance.GetAvailableAttacks().Count < 3;
    }
}
