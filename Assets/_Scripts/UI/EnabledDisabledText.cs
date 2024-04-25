using UnityEngine;
using UnityEngine.UI;

public class EnabledDisabledText : MonoBehaviour
{
    [SerializeField] private string nameOfPlayerPrefsSetting;
    [SerializeField] private Sprite[] textSpritesToFlipBetween;
    private Image image;
    private int lastSettingValue;

    private void Start()
    {
        image = GetComponent<Image>();
        lastSettingValue = PlayerPrefs.GetInt(nameOfPlayerPrefsSetting, 0);
        UpdateImageSprite(lastSettingValue);
        nameOfPlayerPrefsSetting = nameOfPlayerPrefsSetting.Replace(" ", "");
    }

    private void Update()
    {
        if (image == null)
        {
            Debug.LogWarning("Aedan says: Image component is not assigned.");
            return;
        }

        if (!PlayerPrefs.HasKey(nameOfPlayerPrefsSetting))
        {
            Debug.LogWarning("Aedan says: PlayerPrefs key does not exist.");
            return;
        }

        int currentSettingValue = PlayerPrefs.GetInt(nameOfPlayerPrefsSetting, 0);
        if (currentSettingValue != lastSettingValue)
        {
            UpdateImageSprite(currentSettingValue);
            lastSettingValue = currentSettingValue;
        }
    }

    private void UpdateImageSprite(int settingValue)
    {
        image.sprite = textSpritesToFlipBetween[settingValue];
    }
}