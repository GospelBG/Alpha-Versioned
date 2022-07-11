using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Material defaultMat;
    public Material hoverMat;

    public static bool unselectedState = false;
    public static bool selectedState = true;

    public bool currentState;

    public void switchState(bool State) {
        if (State.Equals(selectedState)) {
            gameObject.GetComponent<Renderer>().material = hoverMat;

        } else if (State.Equals(unselectedState)) {
            gameObject.GetComponent<Renderer>().material = defaultMat;
        }

        currentState = State;
    }
}
