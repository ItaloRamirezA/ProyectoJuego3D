using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Vector2 sensibilidad;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X");

        if (horizontal != 0) {
            transform.Rotate(Vector3.up * horizontal * sensibilidad.x);
        }
    }
}
