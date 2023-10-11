using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
    [SerializeField] List<Dialoge> dialoges;

    [SerializeField] Image izqCharacter;
    [SerializeField] Image derCharacter;

    [SerializeField] GameObject popUpIzq;
    [SerializeField] GameObject popUpDer;

    [SerializeField] TextMeshProUGUI popUpTextIzq;
    [SerializeField] TextMeshProUGUI popUpTextDer;

    public Scenes nextScene;

    // Start is called before the first frame update
    public void OnNext()
    {
        if (dialoges.Count <= 0)
        {
            SceneManager.LoadScene(nextScene.ToString());
            return;
        }

        Dialoge dialoge = dialoges[0];
        dialoges.RemoveAt(0);

        izqCharacter.sprite = dialoge.izqCharacter;
        derCharacter.sprite = dialoge.derCharacter;

        switch (dialoge.popUpDir)
        {
            case PopUpDir.Izq:
                popUpIzq.SetActive(true);
                popUpDer.SetActive(false);

                popUpTextIzq.text = dialoge.text;
                break;
            case PopUpDir.Der:
                popUpIzq.SetActive(false);
                popUpDer.SetActive(true);

                popUpTextDer.text = dialoge.text;
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
    ODS13
    //agregar mas escenas
}
