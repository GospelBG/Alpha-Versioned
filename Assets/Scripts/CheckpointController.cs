using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public int ID;

    public static string CheckpointTag = "Checkpoint";

    void Start() {
        PlayerController.checkpoints.Add(new Checkpoint(ID, gameObject));
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals(CheckpointTag)) {
            PlayerController.lastCheckpoint = ID;

            Debug.Log("Triggered checkpoint "+PlayerController.lastCheckpoint.ToString());
        } else {
            Debug.Log(col.gameObject.tag);
        }
    }
}
