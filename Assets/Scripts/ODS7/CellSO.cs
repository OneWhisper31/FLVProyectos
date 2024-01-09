using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cell", menuName = "ScriptableObjects/ODS7 - Cell", order = 1)]
public class CellSO : ScriptableObject
{
    public Sprite sprite;
    [Tooltip("Puede tener cuatro cifras que van del 1 a 0, 0 significa cerrado 1 abierto. El orden es: Izq,Arr,Der,Aba")]
    public bool[] enterEnergy;
}
