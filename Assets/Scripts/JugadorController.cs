using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorController : MonoBehaviour
{
    // Velocidades del jugador
    public float velocidadJugador = 5f;
    public float fuerzaSaltoJugador = 5f;

    // Layers
    public LayerMask saltable;

    // Raycasts
    public float raycastSaltoLength = 1.1f; // Salto

    // Elementos
    private Rigidbody rbJugador;

    // Verificaciones
    private bool estaEnSuelo;
    public bool sePuedeMover = true;
    public bool estaMuerto = false;

    // Audio
    public AudioClip saltoSonido;
    public AudioClip muerteSonido;

    private void Start()
    {
        rbJugador = GetComponent<Rigidbody>();

    }

    private void Update()
    {   
        // Si se puede mvoer y no esta muerto.
        if (sePuedeMover && !estaMuerto)
        {
            // Puede moverse
            caminar();
            salto();
        }
    }

    // -------------------------- MOVIMIENTO INICIO -------------------------- 
    void caminar()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal") * velocidadJugador;

        // Aplicar velocidad al Rigidbody solo en el eje X
        rbJugador.velocity = new Vector3(movimientoHorizontal, rbJugador.velocity.y, 0);
    }

    void salto()
    {
        // Verifica que el jugador toque el suelo con el raycast
        estaEnSuelo = Physics.Raycast(transform.position, Vector3.down, raycastSaltoLength, saltable);

        if (Input.GetButtonDown("Jump") && estaEnSuelo)
        {
            rbJugador.velocity = new Vector3(rbJugador.velocity.x, fuerzaSaltoJugador, rbJugador.velocity.z);

            // Reproducir sonido de salto si está configurado
            if (saltoSonido != null)
            {
                AudioSource.PlayClipAtPoint(saltoSonido, transform.position);
            }
        }
    }
    // -------------------------- MOVIMIENTO FINAL -------------------------- 

    // -------------------------- GIZMOS INICIO -------------------------- 
    void OnDrawGizmos()
    {
        // Dibujar el raycast en el editor para depurar
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastSaltoLength);
    }
    // -------------------------- GIZMOS FINAL -------------------------- 
}
