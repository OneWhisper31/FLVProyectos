using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EspacioEscolar : MonoBehaviour
{
    public string structureName;//usado para destacar cuando no se gastaron puntos en las misma
    public ODS4Buildings[] levelsImage;//tienen que ocuparse en orden

    //nivel 1 significa que no aumento nada y nivel 4 siginifica que la categoria esta al max
    public int currentLevel;

    public void LevelUpCategory()
    {
        if (currentLevel >= levelsImage.Length)
            return;

        StopAllCoroutines();

        var currentLevelImage = levelsImage[currentLevel];
        var unlockedText = levelsImage[currentLevel].unlockedText;

        unlockedText.DOScale(1, 1).OnComplete(
                () => unlockedText.DOScale(0.99f, 3).OnComplete(
                () => unlockedText.DOScale(0, 1)));

        if (currentLevelImage.fadeType == 0)
            StartCoroutine(FadeIn(currentLevelImage.image));
        else
            StartCoroutine(FadeOut(currentLevelImage.image));

        currentLevel++;
    }
    public void LevelDownCategory()
    {
        if (currentLevel < 0)
            return;

        StopAllCoroutines();
        currentLevel--;
        var current = levelsImage[currentLevel];

        if (current.fadeType == 0)
            StartCoroutine(FadeOut(current.image));
        else
            StartCoroutine(FadeIn(current.image));

    }

    IEnumerator FadeOut(Image image)
    {
        var color = image.color;
        color.a = 1;
        image.color = color;
        while (color.a >= 0.001f)
        {
            color.a = Mathf.Lerp(image.color.a, 0, 0.05f);
            image.color = color;
            yield return new WaitForSeconds(0.03f);
        }
        color.a = 0;
        image.color = color;
    }
    IEnumerator FadeIn(Image image)
    {
        var color = image.color;
        color.a = 0;
        image.color = color;

        while (color.a <= 0.998f)
        {
            color.a = Mathf.Lerp(image.color.a, 1, 0.05f);
            image.color = color;
            yield return new WaitForEndOfFrame();
        }
        color.a = 1;
        image.color = color;
    }
}
public enum FadeType
{
    FadeIn,
    FadeOut
}
[System.Serializable]
public struct ODS4Buildings
{
    public FadeType fadeType;
    public Image image;
    public Transform unlockedText;
}
