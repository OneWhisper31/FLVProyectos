using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CanvasHandlerODS7 : MonoBehaviour
{
    Animator animator;

    public Animator UIAnim;
    public GameObject positiveTitle, neutralTitle, negativeTitle,loseTitle;
    public GameObject positiveText, neutralText, negativeText,loseText;
    public Transform gridTransform;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void EndPositive(TypeOfEnergy type)
    {
        gridTransform.DOMoveY(700, 1f).OnComplete(() =>
        {

            if (type == TypeOfEnergy.Solar)
                animator.SetTrigger("SolarPositiva");
            else
                animator.SetTrigger("EolicaPositiva");
        });
    }
    public void EndNeutral(TypeOfEnergy type)
    {
        gridTransform.DOMoveY(700, 1f).OnComplete(() =>
        {

            if (type == TypeOfEnergy.Solar)
                animator.SetTrigger("SolarNeutra");
            else
                animator.SetTrigger("EolicaNeutra");
        });
    }
    public void EndNegative(TypeOfEnergy type)
    {
        gridTransform.DOMoveY(700, 1f).OnComplete(() =>
        {

            if (type == TypeOfEnergy.Petroleo)
                animator.SetTrigger("Petroleo");
            else
                animator.SetTrigger("Gas");
        });
    }
    public void EndPositiveUI()
    {//llamado por evento del animator padre para desplegar el final de la animacion
        UIAnim.SetTrigger("Enter");
        positiveTitle.SetActive(true);
        positiveText .SetActive(true);
    }
    public void EndNeutralUI()
    {//llamado por evento del animator padre para desplegar el final de la animacion
        UIAnim.SetTrigger("Enter");
        neutralTitle.SetActive(true);
        neutralText.SetActive(true);
    }
    public void EndNegativeUI()
    {//llamado por evento del animator padre para desplegar el final de la animacion
        UIAnim.SetTrigger("Enter");
        negativeTitle.SetActive(true);
        negativeText.SetActive(true);
    }
    public void EndLoseUI()
    {//llamado por evento del animator padre para desplegar el final de la animacion
        UIAnim.SetTrigger("Enter");
        loseTitle.SetActive(true);
        loseText.SetActive(true);
    }
    public void StopAnimEnergy()
        => GameManagerODS7.gm.energyChoose.energy.StopAnim();
    public void EndLevel()
        => SceneManager.LoadScene("Trivia");
    public void ResetLevel()
        => SceneManager.LoadScene("ODS7");
}
