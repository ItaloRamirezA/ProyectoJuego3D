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

    // Referencia a la cámara
    public Transform camaraTransform; // Asegúrate de asignar la cámara en el editor

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
        
        // Si se puede mover y no ha muerto
        if (sePuedeMover && !estaMuerto) {
            caminar();
            salto();
        }
    }

    // -------------------------- MOVIMIENTO INICIO --------------------------
    void caminar()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A/D (izquierda/derecha)
        float vertical = Input.GetAxis("Vertical"); // W/S (adelante/atras)

        // Obtener la dirección de movimiento respecto a la cámara
        // No rotamos el jugador, solo movemos en la dirección de la cámara
        Vector3 direccionMovimiento = camaraTransform.forward * vertical + camaraTransform.right * horizontal;
        direccionMovimiento.y = 0;  // Asegurarse de que el movimiento no afecte la altura (eje Y)

        // Normalizar la dirección para evitar que el jugador se mueva más rápido en diagonal
        if (direccionMovimiento.magnitude > 0.1f)
        {
            direccionMovimiento.Normalize();  // Normalizar para que el movimiento no sea más rápido en diagonal

            // Aplicar la nueva velocidad al jugador
            Vector3 nuevaVelocidad = direccionMovimiento * velocidadJugador;
            nuevaVelocidad.y = rb.velocity.y; // Mantener la velocidad actual en el eje Y (salto)
            rb.velocity = nuevaVelocidad;
        }
        else
        {
            // Si no hay movimiento, mantener la velocidad en cero
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
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
