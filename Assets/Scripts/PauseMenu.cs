using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject SettingsRender;
    public static GameObject menu;

    public static int visible = 1;
    public static int invisible = 0;

    void Start() {
        menu = gameObject.transform.parent.gameObject;
        setVisibility(invisible);
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    public static void setVisibility(int visibility) {
        if (visibility == visible) {
            GameController.UIMode = true;
            menu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

        } else if (visibility == invisible) {
            GameController.UIMode = false;
            menu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Save() {

    } 

    public void goToSettings() {
        Settings.resetScene();
        GetComponent<Animation>().Play("MenuToSettings");
    }

    public void returnToMenu() {
        
    }

    public void replaceRender(string SceneName) {
        MainMenu.replaceRenderStatic(SceneName, SettingsRender, gameObject);
    }
}
