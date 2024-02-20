using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class AedanTemporaryChanges : MonoBehaviour
{
    /*
     
     private CursorLockMode beforePauseLockMode = CursorLockMode.Locked;private bool pauseMenuIsOpen;
    
    void Start() //dont copy this method across, just the contents into Start()
    {
        
        ShowOrHidePauseMenu(false);
        
    }

    private void PauseGame(bool pause)
    {

        Time.timeScale = pause ? 0 : 1;
        pausePanel.GetComponent<CanvasGroup>().alpha = pause ? 1 : 0;
        pausePanel.GetComponent<CanvasGroup>().blocksRaycasts = pause;
        pausePanel.GetComponent<CanvasGroup>().interactable = pause;
        pausePanel.GetComponent<PauseMenuButtons>().CloseSettingsMenu();
        EventSystem.current.SetSelectedGameObject(null);
        

        if (pause)
        {
            beforePauseLockMode = Cursor.lockState; //Remembers what the cursor mode was before pausing
        }

        Cursor.lockState = pause ? CursorLockMode.None : beforePauseLockMode;

        centerUIDot.GetComponent<CanvasGroup>().alpha = pause ? 0 : 1;
        
    }
    
    private void ShowOrHidePauseMenu(bool show)
    {
        pausePanel.GetComponent<CanvasGroup>().alpha = show ? 1 : 0; //A shortened if statement
        pausePanel.GetComponent<CanvasGroup>().blocksRaycasts = show;
        pausePanel.GetComponent<CanvasGroup>().interactable = show;
    }
    
    
   private void PauseKey(InputAction.CallbackContext context)
    {
        PauseGame(!pauseMenuIsOpen);
        
        pauseMenuIsOpen = !pauseMenuIsOpen;
    }
    
    
    */

}
