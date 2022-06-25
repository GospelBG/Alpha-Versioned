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
    private float MusicVol;

    [SerializeField]
    public Slider SFX;
    private float SFXVol;

    private bool isLoading;

    // Start is called before the first frame update
    IEnumerator Start() {
        // Load Volume Values
        MusicVol = (PlayerPrefs.GetFloat("music"));
        Music.value = MusicVol;

        SFXVol = (int) (PlayerPrefs.GetFloat("sfx"));
        SFX.value = SFXVol;
        Debug.Log(MusicVol.ToString() + SFXVol.ToString());

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
        if (langName == "") {
            langName = "";
        }

        for (int i = 0; i < langSelector.options.Count; i++) {
            if (langSelector.options[i].text == langName) {
                langSelector.value = i;
            }
        }
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
        MusicVol = Music.value;
        SFXVol =SFX.value;

        PlayerPrefs.SetFloat("music", MusicVol);
        Debug.Log(MusicVol.ToString());

        PlayerPrefs.SetFloat("sfx", SFXVol);
        Debug.Log(SFXVol.ToString());
    }

    public void returnToMenu() {
        isLoading = true;

        PlayerPrefs.Save();

        //TODO: Return to menu
    }
}
