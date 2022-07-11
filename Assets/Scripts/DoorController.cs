using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    public bool opened = false;

    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    void Update() {
        if (!GameController.UIMode && Mouse.current.leftButton.wasPressedThisFrame && GetComponent<Interactable>().currentState) {
            switchState(!opened);
        }
    }

    public void switchState(bool newState) {
        switch (newState) {
            case false:
                anim.Play("OpenedToClosed");
                opened = newState;
                break;

            case true:
                anim.Play("ClosedToOpened");
                opened = newState;
                break;
        }
    }
}
