using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class AnimODS : MonoBehaviour
{
    //[SerializeField] VideoClip video;

    [SerializeField] Transform fadeManager;
    [SerializeField] Transform intro;

    [SerializeField] Introduction introduction;

    VideoPlayer videoPlayer;
    RawImage image;


    void Start()
    {
        videoPlayer = GetComponentInChildren<VideoPlayer>();
        image = videoPlayer.GetComponent<RawImage>();
        videoPlayer.clip = introduction.introSO[(int)introduction.typeODS].introAnim;

        videoPlayer.loopPointReached += (x) =>
        {
            FadeOut();
        };
    }

    public void FadeOut() => StartCoroutine(_FadeOut());

    IEnumerator _FadeOut()
    {
        var color = image.color;
        color.a = 1;
        image.color = color;
        while (color.a >= 0.001f)
        {
            color.a = Mathf.Lerp(image.color.a, 0, 0.05f);
            image.color = color;
            yield return new WaitForEndOfFrame();
        }
        color.a = 0;
        image.color = color;

        //finish
        fadeManager.gameObject.SetActive(true);
        intro.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
