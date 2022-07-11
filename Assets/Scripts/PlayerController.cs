using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 
public class PlayerController : MonoBehaviour
{
    public float MouseSensitivity;
    public Transform Player;
    public Transform cam;
    private float xRotation = 0f;

    public float movementSpeed = 12f;

    public float gravityForce = -9.81f;

    public float groundDistance = 0.4f;

    public Transform groundCheck;
    public LayerMask groundMask;

    bool isGrounded;

    Vector3 movement = new Vector3();

    public static int lastCheckpoint = 0;

    public static List<Checkpoint> checkpoints = new List<Checkpoint>();

    void Start() {
        MouseSensitivity = PlayerPrefs.GetFloat("sensibility", 10);

        Cursor.lockState = CursorLockMode.Locked;

        goToCheckpoint(lastCheckpoint);
    }

    void Update() {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && movement.y < 0) {
            movement.y = -2f;
        }
        Gravity();

        if (!GameController.UIMode) {
            CameraUpdate();
            PlayerMovement();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame && !GameController.UIMode) {
            PauseMenu.setVisibility(PauseMenu.visible);
        } else if (Keyboard.current.escapeKey.wasPressedThisFrame && GameController.UIMode) {
            PauseMenu.setVisibility(PauseMenu.invisible);
        }
    }
 
    public void CameraUpdate() {
        //Get Mouse X and Y inputs
        float mouseX = Mouse.current.delta.x.ReadValue() * MouseSensitivity * Time.deltaTime;
        float mouseY = Mouse.current.delta.y.ReadValue() * MouseSensitivity * Time.deltaTime;
 
        //set xRotation and clamp it so player can't look directly up or down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
 
        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX);
    }

    public void PlayerMovement() {
        float x () {
            float axis = 0;
            if (Keyboard.current.aKey.isPressed) {
                axis += -1;
            }
            
            if (Keyboard.current.dKey.isPressed) {
                axis += 1;
            }

            return axis;
        }

        float z () {
            float axis = 0;
            if (Keyboard.current.sKey.isPressed) {
                axis += -1;
            }
            
            if (Keyboard.current.wKey.isPressed) {
                axis += 1;
            }

            return axis;
        }

        Vector3 movement = transform.right * x() + transform.forward * z();
        
        gameObject.GetComponent<CharacterController>().Move(movement * movementSpeed * Time.deltaTime);
    }

    public void Gravity() {
        movement.y += gravityForce * Time.deltaTime;
        gameObject.GetComponent<CharacterController>().Move(movement * Time.deltaTime);
    }

    public void goToCheckpoint(int queryID) {
        for (int i = 0; i < checkpoints.Count; i++) {
            if (checkpoints[i].ID == queryID) {
                Player.position = checkpoints[i].obj.transform.position;
            }
        }
    }
}