using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MinigameTimer : MonoBehaviour
{
    public Slider timerSlider;
    private float timerValue = 1;
    public Image timerFill;

    private float timerDuration = 60f;

    private void Start()
    {
        DOTween.To(() => timerValue, x => timerValue = x, 0, timerDuration).SetEase(Ease.Linear)
            .OnComplete(FishMegaGameController.instance.CompleteMinigame);
        timerFill.DOColor(Color.red, timerDuration).SetEase(Ease.Linear);
    }

    public void ResetTimer()
    {
        timerDuration = 60f;
    }

    private void Update()
    {
        timerSlider.value = timerValue;
    }
}
