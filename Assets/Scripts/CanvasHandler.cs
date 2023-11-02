using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasHandler : MonoBehaviour
{
    [Header("Years variables")]
    [SerializeField] int yearsInitial=2023;
    [SerializeField] int yearsFinal = 2030;
    [SerializeField] int gametimeSeconds = 300;
    [SerializeField] TextMeshProUGUI years;

    int secondsPerYear;
    int currentYear;
    float currentTime;

    [Header("Temperature variables")]
    [SerializeField] Slider temperature;

    [SerializeField] Slider temperatureFinal;
    [SerializeField] Transform finishSing;
    [SerializeField] DefaultButton finishButton;
    [SerializeField] FadeManager fadeManager;
    //[SerializeField] float totalTemperature;

    bool onFinish;

    private void Start()
    {
        secondsPerYear = gametimeSeconds / (yearsFinal - yearsInitial);
        currentYear = yearsInitial;
        years.text = currentYear+"";
    }
    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= secondsPerYear)
        {
            if (currentYear < yearsFinal)
                UpdateYears();
            else
                EndGame();
        }
    }
    public void UpdateYears()
    {
        if (onFinish) return;

        currentYear++;
        currentTime = 0;
        years.text = currentYear + "";

        if (currentYear == yearsFinal)
            EndGame();
    }
    public void EndGame()
    {
        if (onFinish) return;

        onFinish = true;
        //GameManager.Instance.pauseMode = true;
        currentTime = 0;

        finishSing.DOScale(1, 0.5f)
            .OnComplete(() => {
                StartCoroutine(TemperatureUp());
                //finishButton.transform.DOScale(1, 0.5f);
            }
        );
    }
    IEnumerator TemperatureUp()
    {
        while (temperature.value - temperatureFinal.value <= 0.01f)
        {
            temperatureFinal.value = Mathf.Lerp(temperatureFinal.value, temperature.value, 0.1f);
            yield return new WaitForEndOfFrame();
        }
        finishButton.Interacteable = true;
        temperatureFinal.value = temperature.value;
    }
    public void NextLevel()
    {
        finishButton.Interacteable = false;
        GameManager.Instance.pauseMode = false;
        fadeManager.FadeOut();
    }
    public void UpdateTemperature(float number)=>
        temperature.value = Mathf.Clamp(number + temperature.value, 0, 1);
}
