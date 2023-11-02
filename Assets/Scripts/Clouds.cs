using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField][Range(0,1)] float speed;

    [SerializeField] ParticleSystem cloudsParticle;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.pauseMode)
        {
            cloudsParticle.Pause();
            return;
        }

        transform.Rotate(Vector3.forward * speed);
    }
}
