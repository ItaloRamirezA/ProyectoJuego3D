using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    // Referencia al jugador
    public Transform jugadorTransform;

    void LateUpdate()
    {
        // Actualizar la posición y rotación de la cámara para coincidir con el jugador
        transform.position = jugadorTransform.position;
        transform.rotation = jugadorTransform.rotation;
    }
}
