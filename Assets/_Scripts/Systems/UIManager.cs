
using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private PlayerInputActions playerControls;
    private InputAction pauseAction;
    
    [SerializeField] private TMP_Text dialogue;
    public GameObject pausePanel, centerUIDot;
    private bool pauseMenuIsOpen;

    private void Awake()
    {
        instance = this;
        playerControls = new PlayerInputActions();
    }
    
    private void Start()
    {
        if (centerUIDot == null)
        {
            centerUIDot = GameObject.Find("CenterUIDot");
        }
        if (pausePanel == null)
        {
            pausePanel = GameObject.Find("PausePanel");
        }
        
        if (SceneManager.GetActiveScene().name != "Bootscreen")
        { 
            centerUIDot.GetComponent<CanvasGroup>().alpha = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        pauseMenuIsOpen = false;
    }

    public void NextSubtitle(string subtitle)
    {
        dialogue.text = subtitle;
    }

    
    private void PauseGame(bool pause)
    {
        switch (Cursor.lockState)
        {
            case CursorLockMode.Locked:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case CursorLockMode.None:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case CursorLockMode.Confined:
                Cursor.visible = true;
                break;
            default:
                Cursor.visible = true;
                throw new ArgumentOutOfRangeException();
        }
            
        centerUIDot.GetComponent<CanvasGroup>().alpha = pause ? 0 : 1;
        pausePanel.GetComponent<CanvasGroup>().alpha = pause ? 1 : 0;
        pausePanel.GetComponent<CanvasGroup>().blocksRaycasts = pause;
        pausePanel.GetComponent<CanvasGroup>().interactable = pause;
        pausePanel.GetComponent<PauseMenuButtons>().CloseSettingsMenu();
        EventSystem.current.SetSelectedGameObject(null);
        centerUIDot.GetComponent<CanvasGroup>().alpha = pause ? 0 : 1;
        
        pauseMenuIsOpen = !pauseMenuIsOpen;
        
        Time.timeScale = pause ? 0 : 1;
    }
    
    private void OnEnable()
    {
        pauseAction = playerControls.Misc.Pause;
        pauseAction.Enable();
        pauseAction.performed += PauseKey;
    }

    private void OnDisable()
    {
        pauseAction.Disable();
    }


    private void PauseKey(InputAction.CallbackContext context)
    {
        PauseGame(!pauseMenuIsOpen);
    }
}
