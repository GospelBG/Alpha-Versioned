using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text Version;
    [SerializeField]
    private GameObject SettingsRender;

    public static GameObject staticRender;

    public static GameObject canvas;


    // Start is called before the first frame update
    void Start() {
        staticRender = SettingsRender;
        canvas = gameObject;

        Version.text = "v"+Application.version;

        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
        // TODO: Check for existing saves. (Enable / Disable load button)
    }

    public void Exit() {
        Application.Quit();
    }

    public void newSave() {
        // Create save + start game
    }

    public void loadSave() {
        // Start game using a save file
    }

    public void goToSettings() {
        GetComponent<Animation>().Play("MenuToSettings");
    }

    public void replaceRender (string SceneName) {
        replaceRenderStatic(SceneName);
    }

    public static void replaceRenderStatic (string SceneName) {
        if (SceneName.Equals("Settings")) {
            staticRender.SetActive(false);

            Settings.setSceneVisibility("show scene");
        } else if (SceneName.Equals("MainMenu")) {
            staticRender.SetActive(true);

            canvas.GetComponent<Animation>().Play("SettingsToMenu");
        }
    }
}
