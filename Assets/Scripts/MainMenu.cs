using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text Version;
    [SerializeField]
    private GameObject SettingsRender;

    public static GameObject staticSettingsRender;

    public static GameObject canvas;

    public GameObject OverwriteConfirmation;

    public Button loadButton;

    // Start is called before the first frame update
    void Start() {
        staticSettingsRender = SettingsRender;
        canvas = gameObject;

        Version.text = "v"+Application.version;

        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
        
        if (File.Exists(Application.persistentDataPath+"/Saves/save.sav")) {
            loadButton.interactable = true;
        } else {
            loadButton.interactable = false;
        }
        
    }

    public void Exit() {
        Application.Quit();
    }

    public void newSave() {
        if (File.Exists(Application.persistentDataPath+"/Saves/save.sav")) {
            OverwriteConfirmation.SetActive(true);
        }
    }

    public void startNewSave() {

    }

    public void loadSave() {
        // Start game using a save file
    }

    public void goToSettings() {
        Settings.resetScene();
        GetComponent<Animation>().Play("MenuToSettings");
    }

    public void replaceRender (string SceneName, GameObject staticRender) {
        replaceRenderStatic(SceneName, staticRender, canvas);
    }

    public static void replaceRenderStatic (string SceneName, GameObject staticRender, GameObject cnvs) {
        if (SceneName.Equals("Settings")) {
            staticRender.SetActive(false);

            Settings.lastRenderer = staticRender;
            Settings.lastCanvas = cnvs;

            Settings.setSceneVisibility("show scene");
        } else if (SceneName.Equals("MainMenu")) {
            staticRender.SetActive(true);

            cnvs.GetComponent<Animation>().Play("SettingsToMenu");
        }
    }

    public void closeWarning() {
        OverwriteConfirmation.SetActive(false);
    }
}
