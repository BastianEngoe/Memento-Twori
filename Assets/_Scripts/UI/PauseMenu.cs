using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector] public int currentSettingsTab;
    [SerializeField] private GameObject audioSettings, displaySettings, controlSettings;
    [SerializeField] private GameObject audioTab, displayTab, controlTab, settingCategoryTabs;
    [SerializeField] private Sprite[] audioTabSprite, displayTabSprite, controlTabSprite, tabsSprite;
    private void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        
        SwitchToSettingsTab(0);
    }
    
    private void SetTabState(GameObject settings, GameObject tab, Sprite[] tabSprite, int tabNumber)
    {
        bool isActive = currentSettingsTab == tabNumber;
        CanvasGroup canvasGroup = settings.GetComponent<CanvasGroup>();
        canvasGroup.alpha = isActive ? 1 : 0;
        canvasGroup.interactable = isActive;
        canvasGroup.blocksRaycasts = isActive;
        tab.GetComponent<Image>().sprite = isActive ? tabSprite[0] : tabSprite[2];
    }

    public void SwitchToSettingsTab(int tab)
    {
        currentSettingsTab = tab; // In case we need to know which tab is currently active

        settingCategoryTabs.GetComponent<Image>().sprite = tabsSprite[tab];
        SetTabState(audioSettings, audioTab, audioTabSprite, 0);
        SetTabState(displaySettings, displayTab, displayTabSprite, 1);
        SetTabState(controlSettings, controlTab, controlTabSprite, 2);
    }
}
