using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasHandlerODS4 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Animator anim;

    public string positiveTitle, neutralTitle;
    [TextArea] public string positiveMessage, neutralMessage, positiveFinal, neutralFinal;


    public void EndGame(string structures = "")
    {
        if (structures == "")
        {
            title.text = positiveTitle;
            text.text = positiveMessage
                + "\n \n" +
                positiveFinal;
        }
        else
        {
            title.text = neutralTitle;
            text.text = neutralMessage + structures
                + "\n \n" +
                neutralFinal;
        }
        anim.SetTrigger("Enter");
    }
    public void EndLevel()
       => SceneManager.LoadScene("Trivia");
    public void ResetLevel()
        => SceneManager.LoadScene("ODS4");
}
