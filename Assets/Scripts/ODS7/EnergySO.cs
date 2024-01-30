using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Energy", menuName = "ScriptableObjects/ODS7 - EnergyType", order = 1)]
public class EnergySO : ScriptableObject
{
    public RuntimeAnimatorController animator;
    public Sprite initialSprite;
    public bool isRenovable;
    
}
//animator.runtimeAnimatorController = newController;