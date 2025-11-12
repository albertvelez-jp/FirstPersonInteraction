using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform playerCamera;
    public float mouseSensitivity;

    public float minVerticalAngle = -60f;
    public float maxVerticalAngle = 80f;

    private float actualAngle = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        actualAngle -= mouseY;
        actualAngle = Mathf.Clamp(actualAngle, minVerticalAngle, maxVerticalAngle);

        playerCamera.localRotation = Quaternion.Euler(actualAngle, 0f, 0f);
    }
}
