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
    public LayerMask agua;

    // Raycasts
    public float raycastSaltoLength = 1.1f;

    // Elementos
    private Rigidbody rb;

    // Verificaciones
    private bool estaEnSuelo;
    private bool sePuedeMover;
    private bool estaMuerto;

    // Jugador
    public int MAXVIDAS;
    public int vidaActual;
    public Vector3 spawnPoint = new Vector3(145f, 2.35f, 60f);

    private void Start()
    {   
        vidaActual = MAXVIDAS;
        sePuedeMover = true;
        estaMuerto = false;
        rb = GetComponent<Rigidbody>();

        // Para que no rote
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        // Comprobar muerte
        comprobarMuerte();
        
        //Si se puede mover y no ha muerto
        if (sePuedeMover && !estaMuerto) {
            caminar();
            salto();
        }
    }

    // -------------------------- MOVIMIENTO INICIO -------------------------- 
    void caminar()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direccionMovimiento = new Vector3(horizontal, 0, vertical);

        Vector3 nuevaVelocidad = direccionMovimiento.normalized * velocidadJugador;
        nuevaVelocidad.y = rb.velocity.y;
        rb.velocity = nuevaVelocidad;
    }

    void salto()
    {
        // Verificar si el jugador está en el suelo con un Raycast
        estaEnSuelo = Physics.Raycast(transform.position, Vector3.down, raycastSaltoLength, saltable);

        if (Input.GetButtonDown("Jump") && estaEnSuelo) {
            rb.velocity = new Vector3(rb.velocity.x, fuerzaSaltoJugador, rb.velocity.z);
        }
    }
    // -------------------------- MOVIMIENTO FINAL --------------------------

    // -------------------------- JUGADOR INICIO -------------------------- 
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("agua")) {
            bajarVida();
            tpearJugador(spawnPoint);
        }
    }

    void bajarVida()
    {
        vidaActual--;
    }

    void comprobarMuerte()
    {
        if (vidaActual <= 0) {
            matar();
        }
    }

    void matar()
    {
        estaMuerto = true;
        sePuedeMover = false;
        rb.velocity = Vector3.zero;
    }

    void tpearJugador(Vector3 posicionTP)
    {
        transform.position = posicionTP;  
    }
    // -------------------------- JUGADOR FINAL -------------------------- 

    // -------------------------- GIZMOS INICIO -------------------------- 
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastSaltoLength);
    }
    // -------------------------- GIZMOS FINAL -------------------------- 
}
