using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorController : MonoBehaviour
{
    // Velocidades del jugador
    public float velocidadJugador = 1f;
    public float fuerzaSaltoJugador = 1f;

    // Layers
    public LayerMask saltable;

    // Raycasts
    public float raycastSaltoLength = 1f;

    // Elementos
    private Rigidbody rb;
    
    // Verificaciones
    private bool estaEnSuelo;
    public bool sePuedeMover = true;
    public bool haMuerto = false;

    // Combate
    

    // Audio
    public AudioClip saltoSonido;
    public AudioClip muerteSonido;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        salto();
    }

    // -------------------------- MOVIMIENTO INICIO -------------------------- 
    void caminar()
    {
        
    }

    void salto()
    {
        
    }
    // -------------------------- MOVIMIENTO FINAL -------------------------- 

    // -------------------------- GIZMOS INICIO -------------------------- 
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
    }
    // -------------------------- GIZMOS FINAL -------------------------- 
}
