using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cell", menuName = "ScriptableObjects/ODS7 - Cell", order = 1)]
public class CellSO : ScriptableObject
{
    public RuntimeAnimatorController animator;
    public Sprite sprite;

    [Tooltip("flase significa cerrado y 1 abierto. El orden es: Izq,Arr,Der,Aba")]
    public bool[] enterEnergy;
    //public string enterKeyAnim;
    //public string exitKeyAnim;
}
