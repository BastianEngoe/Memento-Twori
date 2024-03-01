using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingGame4 : MonoBehaviour
{
    [SerializeField] private float hookSpeed = 0.25f;
    private float hookValue = 0;
    private float t = 0;
    public Slider hookSlider;
    
    void Start()
    {
        FishMegaGameController.instance.instructions.text = "Reel it in!";
    }
    
    void Update()
    {
        hookValue = Mathf.Lerp(0, 1, t);
        hookSlider.value = hookValue;

        if (t > 0)
        {
            t -= hookSpeed * Time.deltaTime * FishMegaGameController.instance.masterSpeed;
        }

        if (Input.GetMouseButtonUp(0))
        {
            t += 0.15f;
        }
        
        if(hookValue > 0.9f)
        {
            FishMegaGameController.instance.currentScore += 100;
            FishMegaGameController.instance.NextMinigame();
        }
    }
}
