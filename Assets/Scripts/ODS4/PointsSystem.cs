using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsSystem : MonoBehaviour
{
    [SerializeField] int _points;
    public int points { get=> _points; set { pointsText.text = value+""; _points = value; } }

    [SerializeField]TextMeshProUGUI pointsText;

    private void Start()
    {
        points = _points;
    }

}
