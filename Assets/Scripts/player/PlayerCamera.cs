using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Mathf;

public class PlayerCamera : MonoBehaviour
{
    //Sensibility attributes
    public float sensX; 
    public float sensY;
    //Current camera orientation
    public Transform orientation;
    //Current camera rotation
    public float xRotation;
    public float yRotation;

    private void Start(){
        //Lock cursor on schreen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update(){
        //Get Input x framerate x sensibility
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        //unity rotation handeling 
        yRotation += mouseX;
        xRotation -= mouseY;
        //Make the mouse not rotate all the way on looking up
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        //rotate can and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0,yRotation,0);
    }
}
