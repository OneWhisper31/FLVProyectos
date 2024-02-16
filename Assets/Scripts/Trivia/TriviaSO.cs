using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trivia", menuName = "ScriptableObjects/Trivia", order = 1)]
public class TriviaSO : ScriptableObject
{
    public Sprite izq;
    public Sprite der;

    public List<Questions> questions;
}
