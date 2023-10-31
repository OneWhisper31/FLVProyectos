using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using DG.Tweening;

public class Introduction : MonoBehaviour
{
    [SerializeField] List<Dialoge> dialoges;

    [SerializeField] Image izqCharacter;
    [SerializeField] Image derCharacter;

    [SerializeField] GameObject popUpIzq;
    [SerializeField] GameObject popUpDer;

    [SerializeField] IntroButton button;

    [SerializeField] TextMeshProUGUI popUpTextIzq;
    [SerializeField] TextMeshProUGUI popUpTextDer;

    [SerializeField] Image fadeout;
    
    [SerializeField] UnityEvent onEnd;
    public Scenes nextScene;

    public void LoadScene(){
        button.interacteable = false;
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        var color = fadeout.color;
        color.a=0;
        while (fadeout.color.a<=0.998f)
        {
            color.a = Mathf.Lerp(fadeout.color.a, 1, 0.05f);
            fadeout.color = color;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(nextScene.ToString());
    }

    public void InitDialoge(){
        //button.interacteable = true;
        popUpIzq.transform.localScale = Vector3.zero;
        popUpDer.transform.localScale = Vector3.zero;

        OnNext();
    }

    // Start is called before the first frame update
    public void OnNext()
    {
        button.interacteable = false;
        if (dialoges.Count <= 0)
        {
            onEnd?.Invoke();
            return;
        }

        Dialoge dialoge = dialoges[0];
        dialoges.RemoveAt(0);

        izqCharacter.sprite = dialoge.izqCharacter;
        derCharacter.sprite = dialoge.derCharacter;

        switch (dialoge.popUpDir)
        {
            case PopUpDir.Izq:
                popUpTextIzq.text = dialoge.text;

                popUpIzq.SetActive(true);
                popUpIzq.transform.DOScale(1, 0.5f)
                    .OnComplete(() => popUpDer.transform.DOScale(0, 0.5f)
                    .OnComplete(() => { button.interacteable = true; popUpDer.SetActive(false); }));
                break;
            case PopUpDir.Der:
                popUpTextDer.text = dialoge.text;

                popUpDer.SetActive(true);
                popUpDer.transform.DOScale(1, 0.5f)
                    .OnComplete(() => popUpIzq.transform.DOScale(0, 0.5f)
                    .OnComplete(() => { button.interacteable = true; popUpIzq.SetActive(false); }));
                break;
            default:
                break;
        }
    }
}
[System.Serializable]
public struct Dialoge
{
    public Sprite izqCharacter;
    public Sprite derCharacter;
    public PopUpDir popUpDir;
    public string text;
}
public enum PopUpDir
{
    Izq,
    Der
}
public enum Scenes
{
    ODS13,
    Introduction,
    Trivia
    //agregar mas escenas
}
