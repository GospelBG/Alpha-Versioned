using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint {
    public int ID;
    public GameObject obj;

    public Checkpoint (int newID, GameObject newObject) {
        ID = newID;
        obj = newObject;
    }
}