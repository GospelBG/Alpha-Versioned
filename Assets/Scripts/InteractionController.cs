using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    void OnTriggerEnter (Collider col) {
        GameObject obj = col.gameObject;
        if (obj.GetComponent<Interactable>() != null) {
            obj.GetComponent<Interactable>().switchState(Interactable.selectedState);
        }
    }

    void OnTriggerExit (Collider col) {
        GameObject obj = col.gameObject;
        if (obj.GetComponent<Interactable>() != null) {
            obj.GetComponent<Interactable>().switchState(Interactable.unselectedState);
        }
    }
}
