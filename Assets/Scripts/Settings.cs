using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using TMPro;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField]
    public TMP_Dropdown langSelector;
    private string langName;

    [SerializeField]
    public Slider Music;
    private int MusicVol;

    [SerializeField]
    public Slider SFX;
    private int SFXVol;

    private bool isLoading;

    // Start is called before the first frame update
    IEnumerator Start() {
        // Load Language Selector
        yield return LocalizationSettings.InitializationOperation;

        langSelector.options.Clear();
        foreach (var lang in LocalizationSettings.AvailableLocales.Locales)
        {
            string name = lang.name.Replace("-", "/");
            langSelector.options.Add(new TMP_Dropdown.OptionData(name));
        }

        langSelector.onValueChanged.AddListener(LocaleSelected);

        langName = PlayerPrefs.GetString("locale");
        for (int i = 0; i < langSelector.options.Count; i++) {
            if (langSelector.options[i].text == langName) {
                langSelector.value = i;
            }
        }


        // Load Volume Values
        MusicVol = (int) (PlayerPrefs.GetInt("music"));
        Music.value = (float) (MusicVol / 100);

        SFXVol = (int) (PlayerPrefs.GetInt("sfx"));
        SFX.value = (float) (SFXVol / 100);
        Debug.Log(MusicVol.ToString() + SFXVol.ToString());
    }

    public void LocaleSelected(int index) {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        langName = LocalizationSettings.AvailableLocales.Locales[index].name;
    }

    public void setLang() {
        int option = langSelector.value;
        string newLang = langSelector.options[option].text;

        PlayerPrefs.SetString("locale", newLang);
    }

    public void SetVolume() {
        if (isLoading) {
            return;
        }
        MusicVol = (int) (Music.value * 100);
        SFXVol = (int) (SFX.value * 100);

        PlayerPrefs.SetInt("music", MusicVol);
        Debug.Log(MusicVol.ToString());

        PlayerPrefs.SetInt("sfx", SFXVol);
        Debug.Log(SFXVol.ToString());
    }

    public void returnToMenu() {
        isLoading = true;

        PlayerPrefs.Save();

        //TODO: Return to menu
    }
}
