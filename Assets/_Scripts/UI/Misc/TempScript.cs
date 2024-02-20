using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScript : MonoBehaviour
{
    private GameObject pausePanel;
    
    private void Start()
    {
        pausePanel = GameObject.Find("PausePanel");
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            //toggle between
            
            if (pausePanel.GetComponent<CanvasGroup>().alpha == 1)
            {
                pausePanel.GetComponent<CanvasGroup>().alpha = 0;
                pausePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
                pausePanel.GetComponent<CanvasGroup>().interactable = false;
            }
            else
            {
                pausePanel.GetComponent<CanvasGroup>().alpha = 1;
                pausePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
                pausePanel.GetComponent<CanvasGroup>().interactable = true;
            }
        }
    }
}
