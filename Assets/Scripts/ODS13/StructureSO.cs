using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Structure", menuName = "ScriptableObjects/ODS13 - Structure", order = 1)]
public class StructureSO : ScriptableObject
{
    public SpriteStructure initial;
    public SpriteStructure positive1;
    public SpriteStructure positive2;
    public SpriteStructure negative;
}
