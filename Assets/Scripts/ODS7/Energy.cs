using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public Image sprite;
    public Animator anim;
    public EnergySO EnergySO { get=> energySO; 
        set{
            energySO=value;
            anim.runtimeAnimatorController = energySO.animator;
            sprite.sprite = energySO.initialSprite;
        }}
    EnergySO energySO;
}
