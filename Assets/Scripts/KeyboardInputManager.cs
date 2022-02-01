using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : InputManager
{
    public static event MoveInputHandler OnMoveInput;
    public static event RotateInputHandler OnRotateInput;
    public static event ZoomInputHandler OnZoomInput;

    private void Update() {
        // move
        if (Input.GetKey(KeyCode.W)) {
            OnMoveInput?.Invoke(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S)) {
            OnMoveInput?.Invoke(-Vector3.forward);
        }
        if (Input.GetKey(KeyCode.A)) {
            OnMoveInput?.Invoke(-Vector3.right);
        }
        if (Input.GetKey(KeyCode.D)) {
            OnMoveInput?.Invoke(Vector3.right);
        }
        // rotation
        if (Input.GetKey(KeyCode.E)) {
            OnRotateInput?.Invoke(-1f);
        }
        if (Input.GetKey(KeyCode.Q)) {
            OnRotateInput?.Invoke(1f);
        }
        // zoom
        if (Input.GetKey(KeyCode.Z)) {
            OnZoomInput?.Invoke(-1f);
        }
        if (Input.GetKey(KeyCode.X)) {
            OnZoomInput?.Invoke(1f);
        }
    }
}
