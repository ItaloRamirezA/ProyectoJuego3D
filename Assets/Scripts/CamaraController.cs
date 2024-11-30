using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    private Transform camaraTransform;
    public Vector2 sensibilidad;

    void Start()
    {
        camaraTransform = transform.Find("--- MAIN CAMERA ---");
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        movimientoCamara();
    }

    void movimientoCamara()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        if (horizontal != 0) {
            transform.Rotate(Vector3.up * horizontal * sensibilidad.x);
        }

        if (vertical != 0) {
            float angle = camaraTransform.localEulerAngles.x - vertical * sensibilidad.y;
            camaraTransform.localEulerAngles = Vector3.right * angle;
        }
    }
}
