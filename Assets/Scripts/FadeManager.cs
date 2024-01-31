using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField] Image fadeout;

    [SerializeField] bool initialFadeIn;
    [SerializeField] bool initialPause;

    [SerializeField] Scenes nextScene;

    public void Start()
    {
        if (initialFadeIn)
            FadeIn();
        if (initialPause&& GameManager.Instance!=null)
            GameManager.Instance.pauseMode = true;
    }

    public void FadeIn()=> StartCoroutine(_FadeIn());
    public void FadeOut() => StartCoroutine(_FadeOut());

    IEnumerator _FadeOut()
    {
        var color = fadeout.color;
        color.a = 0;
        fadeout.color = color;

        while (color.a <= 0.998f)
        {
            color.a = Mathf.Lerp(fadeout.color.a, 1, 0.05f);
            fadeout.color = color;
            yield return new WaitForEndOfFrame();
        }
        color.a = 1;
        fadeout.color = color;

        GameManager.Instance.pauseMode = false;
        SceneManager.LoadScene(nextScene.ToString());
    }
    IEnumerator _FadeIn()
    {
        var color = fadeout.color;
        color.a = 1;
        fadeout.color = color;
        while (color.a >= 0.001f)
        {
            color.a = Mathf.Lerp(fadeout.color.a, 0, 0.05f);
            fadeout.color = color;
            yield return new WaitForEndOfFrame();
        }
        color.a = 0;
        fadeout.color = color;

        //SceneManager.LoadScene(nextScene.ToString());
    }


}
