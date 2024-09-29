using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/MainAbility")]

public class MainAbility : AbilitySystem
{
    public Player player;
    GameManager gameManager;
    AnimationClip clip;
    Animator animator;
     AnimatorOverrideController animatorOverrideController;
    public AnimationClip[] abilityAnimationClip;
    public int abilityIndex = 0;
    public int keyIndex = 1;
    
    
    
        

    public override void Use(int index)
    {
        ;
        if (index == keyIndex)
        {
            animator = player.GetComponent<Animator>();
            player.attackPower += 3;
            animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = animatorOverrideController;
            abilityIndex = (abilityIndex + 1) % abilityAnimationClip.Length;
            animatorOverrideController["First Attack"] = abilityAnimationClip[abilityIndex];
            animator.Play("First Attack");
            Debug.Log("Main Ability Working");
        }


    }
}
