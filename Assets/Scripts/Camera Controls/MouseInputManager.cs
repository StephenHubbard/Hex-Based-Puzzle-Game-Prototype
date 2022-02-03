using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputManager : InputManager
{
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private float rotateSpeedX = .3f;
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

    private Camera cam;

    private void Awake() {
        screen = new Vector2Int(Screen.width, Screen.height);
        cam = Camera.main;
    }

    private void Update() {
        Vector3 mp = Input.mousePosition;
        bool mouseValid = (mp.y <= screen.y * 1.05f && mp.y >= screen.y * -0.05f && mp.x <= screen.x * 1.05f && mp.x >= screen.x * -0.05f);

        if (!mouseValid) return;

        MouseRotationX(mp);
        MouseRotationY(mp);

        // zoom
        if (Input.mouseScrollDelta.y > 0) {
            OnZoomInput?.Invoke(-zoomSpeed);
        }
        else if (Input.mouseScrollDelta.y < 0) {
            OnZoomInput?.Invoke(zoomSpeed);
        }
    }

    private void MouseRotationX(Vector3 mp) {
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

    private void MouseRotationY(Vector3 mp) {
        if (Input.GetMouseButtonDown(1)) {
            lastMousePositionY = mp.y;
        } else if (Input.GetMouseButton(1)) {
            if (mp.y < lastMousePositionY && cam.transform.eulerAngles.x < 80) {
                currentMousePositionY = mp.y;
                float difference = Mathf.Abs(lastMousePositionY - currentMousePositionY);
                cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x + (difference * rotateSpeedX), cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
                lastMousePositionY = currentMousePositionY;
            }
            else if (mp.y > lastMousePositionY && cam.transform.eulerAngles.x > -80) {
                currentMousePositionY = mp.y;
                float difference = Mathf.Abs(lastMousePositionY - currentMousePositionY);
                cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x - (difference * rotateSpeedX), cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
                lastMousePositionY = currentMousePositionY;
            }
        }

        if (cam.transform.eulerAngles.x > 81) {
            cam.transform.eulerAngles = new Vector3(80, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
        } else if (cam.transform.eulerAngles.x < -81) {
            cam.transform.eulerAngles = new Vector3(-80, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
        }
    }
}
