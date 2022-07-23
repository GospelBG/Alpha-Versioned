using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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

    [SerializeField]
    public Slider Sens;

    public static Canvas canvas;

    public static GameObject content;


    public static GameObject lastRenderer;
    public static GameObject lastCanvas;

    void Update() {
        if (GameController.UIMode && Keyboard.current.escapeKey.wasPressedThisFrame) {
            returnToMenu();
        }
    }

    // Start is called before the first frame update
    IEnumerator Start() {
        content = gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        canvas = GetComponent<Canvas>();

        // Load Volume Values
        MusicVol = PlayerPrefs.GetFloat("music");
        Music.value = MusicVol;

        SFXVol = PlayerPrefs.GetFloat("sfx");
        SFX.value = SFXVol;

        // Load Gameplay config
        float sensValue = PlayerPrefs.GetFloat("sensibility", 10);
        Sens.value = sensValue;

        // Enable Slider listeners
        Music.onValueChanged.AddListener(delegate {SetVolume();});
        SFX.onValueChanged.AddListener(delegate {SetVolume();});

        // Load Language Selector
        yield return LocalizationSettings.InitializationOperation;
        setLang(PlayerPrefs.GetInt("locale", 9));

        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++) {
            if (PlayerPrefs.GetInt("locale", 9) == 9 && LocalizationSettings.SelectedLocale.name.ToLower().Replace("-", " / ").Equals(LocalizationSettings.AvailableLocales.Locales[i].name.ToLower())) {
                langSelector.value = i;
            }

            if (i == PlayerPrefs.GetInt("locale", 9)) {
                langSelector.value = i;
            }
        }

        langSelector.onValueChanged.AddListener(setLang); // Enable Listener
    }

    public static void resetScene() {
        content.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void setLang(int index) {
        if (index == 9) {
            for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++) {   
                if (i == 0) { //Localize selector label
                    var opLabel = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Strings", "lang_"+langSelector.value);
                    langSelector.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = opLabel.Result;
                }

                var op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Strings", "lang_"+i);

                langSelector.options.Add(new TMP_Dropdown.OptionData(op.Result));
            }

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

    public void setSens() {
        PlayerPrefs.SetFloat("sensibility", Sens.value);
    }

    public void returnToMenu() {
        PlayerPrefs.Save();

        MainMenu.replaceRenderStatic("MainMenu", lastRenderer, lastCanvas);
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
