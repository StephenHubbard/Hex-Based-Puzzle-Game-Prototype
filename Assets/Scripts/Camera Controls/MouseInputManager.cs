using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputManager : InputManager
{
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private float rightClickRotationSpeed = .1f;

    Vector2Int screen;
    float lastMousePositionX;
    float lastMousePositionY;
    float currentMousePositionX;
    float currentMousePositionY;

    // Events
    public static event MoveInputHandler OnMoveInput;
    public static event RotateInputHandler OnRotateInput;
    public static event ZoomInputHandler OnZoomInput;

    private void Awake() {
        screen = new Vector2Int(Screen.width, Screen.height);
    }

    private void Update() {
        Vector3 mp = Input.mousePosition;
        bool mouseValid = (mp.y <= screen.y * 1.05f && mp.y >= screen.y * -0.05f && mp.x <= screen.x * 1.05f && mp.x >= screen.x * -0.05f);

        if (!mouseValid) return;

        // movement with mouse on edge screen 
        
        // if (mp.y > screen.y * 0.95f) {
        //     OnMoveInput?.Invoke(Vector3.forward);
        // }
        // else if (mp.y < screen.y * 0.05f) {
        //     OnMoveInput?.Invoke(-Vector3.forward);
        // }

        // if (mp.x > screen.x * 0.95f) {
        //     OnMoveInput?.Invoke(Vector3.right);
        // }
        // else if (mp.x < screen.x * 0.05f) {
        //     OnMoveInput?.Invoke(-Vector3.right);
        // }

        MouseRotation(mp);

        // zoom
        if (Input.mouseScrollDelta.y > 0) {
            OnZoomInput?.Invoke(-zoomSpeed);
        }
        else if (Input.mouseScrollDelta.y < 0) {
            OnZoomInput?.Invoke(zoomSpeed);
        }
    }

    private void MouseRotation(Vector3 mp) {
        // rotation x
        if (Input.GetMouseButtonDown(1)) {
            lastMousePositionX = mp.x;
        } else if (Input.GetMouseButton(1)) {
            if (mp.x < lastMousePositionX) {
                currentMousePositionX = mp.x;
                float difference = Mathf.Abs(lastMousePositionX - currentMousePositionX);
                OnRotateInput?.Invoke(-1f * difference * rightClickRotationSpeed);
                lastMousePositionX = currentMousePositionX;
            }
            else if (mp.x > lastMousePositionX) {
                currentMousePositionX = mp.x;
                float difference = Mathf.Abs(lastMousePositionX - currentMousePositionX);
                OnRotateInput?.Invoke(1f * difference * rightClickRotationSpeed);
                lastMousePositionX = currentMousePositionX;
            }
        }
    }
}
