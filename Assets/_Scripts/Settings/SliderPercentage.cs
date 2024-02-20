using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderPercentage : MonoBehaviour
{
    private TMP_Text textComponent;
    private Slider sliderComponent;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        sliderComponent = transform.parent.GetComponent<Slider>();
        StartCoroutine(UpdatePercentage());
    }

    private IEnumerator UpdatePercentage()
    {
        while (true)
        {
            float percentage = 0;
            if (sliderComponent.maxValue != 0)
            {
                percentage = (sliderComponent.value / sliderComponent.maxValue) * 100;
            }
            textComponent.text = $"{percentage:F0}%";
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
