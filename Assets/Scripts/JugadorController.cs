using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorController : MonoBehaviour
{
    // Velocidades del jugador
    public float velocidadJugador = 2f;
    public float fuerzaSaltoJugador = 2f;

    // Layers
    public LayerMask saltable;

    // Raycasts
    public float raycastSaltoLength = 1.1f; // Salto

    // Elementos
    private Rigidbody rb;

    // Verificaciones
    private bool estaEnSuelo;
    public bool sePuedeMover;
    public bool estaMuerto;

    // Audio
    public AudioClip saltoSonido;
    public AudioClip muerteSonido;

    private void Start()
    {
        sePuedeMover = true;
        estaMuerto = false;
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {   
        // Si se puede mvoer y no esta muerto.
        if (sePuedeMover && !estaMuerto)
        {
            // Movimiento
            caminar();
            salto();
        }
    }

    // -------------------------- MOVIMIENTO INICIO -------------------------- 
    void caminar()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direccionMovimiento = Camera.main.transform.right * horizontal + Camera.main.transform.forward * vertical;
        direccionMovimiento.y = 0; // Evita movimiento vertical
        rb.AddForce(direccionMovimiento.normalized * velocidadJugador);
        
        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.1f) {
            rb.AddForce(Vector3.up * fuerzaSaltoJugador, ForceMode.Impulse);
        }
    }

    void salto()
    {
        // Verifica que el jugador toque el suelo con el raycast
        estaEnSuelo = Physics.Raycast(transform.position, Vector3.down, raycastSaltoLength, saltable);

        if (Input.GetButtonDown("Jump") && estaEnSuelo)
        {
            rb.velocity = new Vector3(rb.velocity.x, fuerzaSaltoJugador, rb.velocity.z);

        }
    }
    // -------------------------- MOVIMIENTO FINAL -------------------------- 

    // -------------------------- GIZMOS INICIO -------------------------- 
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastSaltoLength);
    }
    // -------------------------- GIZMOS FINAL -------------------------- 
}
