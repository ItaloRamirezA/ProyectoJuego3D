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

    // Referencias
    public Transform camaraTransform; // Transform de la cámara

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

        // Para que no rote el jugador cuando se mueve
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        // Comprobar muerte
        comprobarMuerte();
        
        // Si se puede mover y no ha muerto
        if (sePuedeMover && !estaMuerto) {
            caminar();
            salto();
        }
    }

    // -------------------------- MOVIMIENTO INICIO --------------------------
    void caminar()
    {
        // Obtener entradas de movimiento (teclas WASD o flechas)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Obtener dirección de la cámara (dirección en la que el jugador está mirando)
        Vector3 direccionMovimiento = camaraTransform.forward * vertical + camaraTransform.right * horizontal;
        direccionMovimiento.y = 0; // Eliminar la componente Y para evitar que se mueva hacia arriba/abajo

        // Normalizar la dirección para mantener la velocidad constante
        Vector3 nuevaVelocidad = direccionMovimiento.normalized * velocidadJugador;
        nuevaVelocidad.y = rb.velocity.y; // Mantener la componente vertical (para el salto)
        rb.velocity = nuevaVelocidad; // Aplicar la nueva velocidad al Rigidbody

        // Hacer que el jugador mire hacia la dirección del movimiento
        if (direccionMovimiento.magnitude > 0.1f)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccionMovimiento);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * 10f);
        }
    }

    void salto()
    {
        // Verificar si el jugador está en el suelo con un Raycast
        estaEnSuelo = Physics.Raycast(transform.position, Vector3.down, raycastSaltoLength, saltable);

        // Si se presiona la tecla de salto y está en el suelo
        if (Input.GetButtonDown("Jump") && estaEnSuelo) {
            rb.velocity = new Vector3(rb.velocity.x, fuerzaSaltoJugador, rb.velocity.z);
        }
    }
    // -------------------------- MOVIMIENTO FINAL --------------------------

    // -------------------------- JUGADOR INICIO -------------------------- 
    private void OnCollisionEnter(Collision other)
    {
        // Si el jugador toca agua, pierde vida y se teletransporta
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
