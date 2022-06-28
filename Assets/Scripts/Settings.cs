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
    private int localeIndex;


    [SerializeField]
    public Slider Music;
    private float MusicVol;

    [SerializeField]
    public Slider SFX;
    private float SFXVol;

    public static Canvas canvas;

    // Start is called before the first frame update
    IEnumerator Start() {
        canvas = GetComponent<Canvas>();

        // Load Volume Values
        Debug.Log(PlayerPrefs.GetFloat("music").ToString() +  PlayerPrefs.GetFloat("sfx").ToString());

        MusicVol = PlayerPrefs.GetFloat("music");
        Music.value = MusicVol;

        SFXVol = PlayerPrefs.GetFloat("sfx");
        SFX.value = SFXVol;

        Debug.Log(MusicVol.ToString() + SFXVol.ToString());

        // Enable Slider listeners
        Music.onValueChanged.AddListener(delegate {SetVolume();});
        SFX.onValueChanged.AddListener(delegate {SetVolume();});

        // Load Language Selector
        yield return LocalizationSettings.InitializationOperation;
        setLang(PlayerPrefs.GetInt("locale", 9));        

        for (int i = 0; i < langSelector.options.Count; i++) {
            if (PlayerPrefs.GetInt("locale", 9) == 9 && LocalizationSettings.SelectedLocale.name.ToLower().Replace("-", " / ").Equals(LocalizationSettings.AvailableLocales.Locales[i].name.ToLower())) {
                langSelector.value = i;
            }

            if (i == PlayerPrefs.GetInt("locale", 9)) {
                langSelector.value = i;
            }
        }

        langSelector.onValueChanged.AddListener(setLang); // Enable Listener
    }

    public void setLang(int index) {
        if (index == 9) {
            return;
        }
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        localeIndex = index;


        PlayerPrefs.SetInt("locale", index);

        langSelector.options.Clear();

        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++) {   
            if (i == 0) { //Localize selector label
                var opLabel = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Strings", "lang_"+langSelector.value);
                langSelector.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = opLabel.Result;
            }

            var op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Strings", "lang_"+i);

            langSelector.options.Add(new TMP_Dropdown.OptionData(op.Result));
        }
    }

    public void SetVolume() {
        MusicVol = Music.value;
        SFXVol = SFX.value;

        PlayerPrefs.SetFloat("music", MusicVol);
        Debug.Log(MusicVol.ToString());

        PlayerPrefs.SetFloat("sfx", SFXVol);
        Debug.Log(SFXVol.ToString());
    }

    public void returnToMenu() {
        PlayerPrefs.Save();

        MainMenu.replaceRenderStatic("MainMenu");
        setSceneVisibility("hide scene");
    }

    public static void setSceneVisibility(string state) {
        switch (state) {
            case "show scene":
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                //MainMenu.canvas.GetComponent<Animation>().StopPlayback();
                break;

            case "hide scene":
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                break;

        }
    }
}
