using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingGame2 : MonoBehaviour
{
    private float hookSpeed = 0.5f;
    private float hookValue = 0;
    private float t;
    public Slider hookSlider;

    public bool isGoing = true;

    private void Start()
    {
        FishMegaGameController.instance.instructions.text = "Show your fish affection! <3";
    }

    private void Update()
    {
        if (isGoing)
        {
            hookValue = Mathf.Lerp(0, 1, t);
            hookSlider.value = hookValue;

            t += hookSpeed * Time.deltaTime * FishMegaGameController.instance.masterSpeed;

            if (hookValue > 0.99f)
            {
                t = 0;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isGoing = false;

            if (hookValue > 0.4f)
            {
                if (hookValue > 0.6f)
                {
                    if (hookValue > 0.85f)
                    {
                        FishMegaGameController.instance.currentScore += 100;
                        Debug.Log("100");
                    }
                    else
                    {
                        FishMegaGameController.instance.currentScore += 50;
                        Debug.Log("50");
                    }
                }
                else
                {
                    FishMegaGameController.instance.currentScore += 25;
                    Debug.Log("25");
                }
            }

            FishMegaGameController.instance.NextMinigame();
        }
    }
}
