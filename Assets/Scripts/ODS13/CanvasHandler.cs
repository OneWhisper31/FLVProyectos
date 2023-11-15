using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

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

    [Header("Gei variables")]
    [SerializeField] TextMeshProUGUI gei;
    [SerializeField] Image ozono;

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
        GameManager.Instance.pauseMode = true;
        currentTime = 0;

        finishSing.DOScale(1, 0.5f)
            .OnComplete(() => {
                StartCoroutine(TemperatureUp());
                finishButton.Interacteable = true;
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
        temperatureFinal.value = temperature.value;
    }
    public void NextLevel()
    {
        fadeManager.FadeOut();
        finishButton.Interacteable = false;
    }
    public void UpdateTemperature(float number)
    {
        int numberGei = 0;
        int.TryParse(gei.text, out numberGei);

        gei.text = ""+ Mathf.Clamp(Mathf.CeilToInt(numberGei + number * 20),0,99);
        temperature.value = Mathf.Clamp(number + temperature.value, 0, 1);

        var ozonoColor = ozono.color;
        ozonoColor.a = Mathf.Clamp(number + temperature.value, 0.2f, 1);
        ozono.color = ozonoColor;
    }
}
