using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Mathf;

public class PlayerCamera : MonoBehaviour
{
    private Transform player;
    public float sensX;
    public float sensY;

    public float xRotation = 0f;
    public float yRotation = 0f;

    private void Start()
    {
        player = transform.root;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation -= mouseY;
        xRotation = mouseX;

        yRotation = Mathf.Clamp(yRotation, -80f, 80f);
        transform.localEulerAngles = Vector3.right * yRotation;
        player.Rotate(Vector3.up * xRotation);

    }
}
