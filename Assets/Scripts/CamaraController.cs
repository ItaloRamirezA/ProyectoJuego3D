using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{

    // Transforms
    public Transform jugadorTransform;

    // Audio
    public AudioClip musicaFondo;

    void Start()
    {
        moverCamaraConJugador();
    }

    void Update()
    {
        
    }

    void moverCamaraConJugador()
    {
        transform.position = jugadorTransform.position;
    }
}
