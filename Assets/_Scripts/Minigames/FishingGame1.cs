using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FishingGame1 : MonoBehaviour
{
    [SerializeField] private float hookSpeed = 0.5f;
    private float hookValue = 0;
    private float t;
    public Slider hookSlider;

    [System.Serializable]
    public enum fishState
    {
        UP,
        DOWN,
        DONE
    }

    public fishState currentFishState;

    private void Update()
    {
        if (hookSlider.value > 0.97f)
        {
            currentFishState = fishState.DOWN;
        }
        else if (hookSlider.value < 0.03f)
        {
            currentFishState = fishState.UP;
        }

        if (currentFishState == fishState.DOWN)
        {
            hookValue = Mathf.Lerp(0, 1, t);
            hookSlider.value = hookValue;

            t -= hookSpeed * Time.deltaTime;
        }
        else if (currentFishState == fishState.UP)
        {
            hookValue = Mathf.Lerp(0, 1, t);
            hookSlider.value = hookValue;

            t += hookSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            currentFishState = fishState.DONE;

            if (hookValue < 0.63f && hookValue > 0.41)
            {
                Debug.Log("Fish!");
            }
            else
            {
                Debug.Log("No fish :C");
            }
        }
    }
}
