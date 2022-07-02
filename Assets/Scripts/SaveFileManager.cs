using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveFileManager : MonoBehaviour {
    public void newSave() { // Creates a new save (overwriting the existing one)
        deleteSave();
    }

    public void deleteSave() {
        if (File.Exists(Application.persistentDataPath+"/Saves/save.sav")) {
            File.Delete(Application.persistentDataPath+"/Saves/save.sav");
        }
    }
}
