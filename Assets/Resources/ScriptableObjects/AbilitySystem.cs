using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public abstract class AbilitySystem : ScriptableObject
{
    public AnimationClip AnimationClip;
    public abstract void Use(int index);
    
}
