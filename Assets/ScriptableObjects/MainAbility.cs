using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/MainAbility")]

public class MainAbility : AbilitySystem
{
    Player player;
    AnimationClip clip;

    public override void Use()
    {
        player.attackPower += 3;
    }
}
