using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{   
    public static bool UIMode;
    
    void Start() {
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
    }
}
