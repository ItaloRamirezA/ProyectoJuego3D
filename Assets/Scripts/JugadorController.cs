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
    public float raycastSaltoLength = 1.1f;

    // Elementos
    private Rigidbody rb;

    // Verificaciones
    private bool estaEnSuelo;
    public bool sePuedeMover;
    public bool estaMuerto;

    // Transforms
    public Transform camaraTransform;

    // Audio
    public AudioClip saltoSonido;
    public AudioClip muerteSonido;

    private void Start()
    {
        sePuedeMover = true;
        estaMuerto = false;
        rb = GetComponent<Rigidbody>();

        // Para que no rote
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        // Si se puede mover y ha muerto
        if (sePuedeMover && !estaMuerto)
        {
            caminar();
            salto();
        }
    }

    // -------------------------- MOVIMIENTO INICIO -------------------------- 
    void caminar()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direccionMovimiento = camaraTransform.right * horizontal + camaraTransform.forward * vertical;
        direccionMovimiento.y = 0;

        Vector3 nuevaVelocidad = direccionMovimiento.normalized * velocidadJugador;
        nuevaVelocidad.y = rb.velocity.y;
        rb.velocity = nuevaVelocidad;

        // Que mire hacia donde se mueve
        if (direccionMovimiento.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(direccionMovimiento);
        }
    }

    void salto()
    {
        // Verificar si el jugador está en el suelo con un Raycast
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
