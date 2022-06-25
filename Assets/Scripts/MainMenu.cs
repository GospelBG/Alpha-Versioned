using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text Version;

    // Start is called before the first frame update
    void Start()
    {
        Version.text = "v"+Application.version;

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
        // Load settings scene (+ transition)
    }
}
