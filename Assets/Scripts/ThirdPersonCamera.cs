using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform characterBody;
    public Transform characterHead;
    //Configura sensibilidade da camera
    float sensitivityX = 1.5f;
    float sensitivityY = 1.5f;

    float rotationX = 0;
    float rotationY = 0;
    float angleYmin = -25;
    float angleYmax = 25;

    float smoothRotx = 0;
    float smoothRoty = 0;
    float smoothCoefx = 0.005f;
    float smoothCoefy = 0.005f;

    public Vector3 GetForwardDirection() => transform.forward;
    public Vector3 GetPosition() => characterHead.position;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    private void LateUpdate() {
        transform.position = characterHead.position;    
    }

    // Update is called once per frame
    void Update()
    {
        float verticalDelta = Input.GetAxisRaw("Mouse Y") * sensitivityY;
        float horizontalDelta = Input.GetAxisRaw("Mouse X") * sensitivityX;

        smoothRotx = Mathf.Lerp(smoothRotx, horizontalDelta, smoothCoefx);
        smoothRoty = Mathf.Lerp(smoothRoty, verticalDelta, smoothCoefy);

        rotationX += smoothRotx;
        rotationY += smoothRoty;

        rotationY = Mathf.Clamp(rotationY, angleYmin, angleYmax);

        characterBody.localEulerAngles = new Vector3(0, rotationX, 0);
        transform.localEulerAngles = new Vector3(-rotationY, rotationX,0);
        
    }
}
