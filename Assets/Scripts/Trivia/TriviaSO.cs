using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trivia", menuName = "ScriptableObjects/Trivia", order = 1)]
public class TriviaSO : ScriptableObject
{
    public List<Questions> questions;
}
