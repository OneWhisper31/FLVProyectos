using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


[CreateAssetMenu(fileName = "Introduction", menuName = "ScriptableObjects/Introduction", order = 1)]
public class IntroductionSO : ScriptableObject
{
    public ODSType type;

    public List<Dialoge> dialoges;

    public VideoClip introAnim; 
}
public enum ODSType
{//Segun numero va en orden
    ODS4,
    ODS7,
    ODS13
}
