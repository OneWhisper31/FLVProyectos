using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Hitbox : MonoBehaviour
{
    [SerializeField]bool isAtmosphere;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var raylight = collision.GetComponent<Raylight>();

        if (raylight == null)
            return;

        if (isAtmosphere)
        {
            if (Random.Range(0, 101) > GameManager.Instance.chanceHandler)
            {
                raylight.Bounce();
            }
        }
        else
        {
            //raylight.Bounce();
        }
    }
}
