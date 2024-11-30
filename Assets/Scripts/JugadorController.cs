using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
    public MenuPausaController menuPausaController;

    // Verificaciones
    private bool estaEnSuelo;
    private bool sePuedeMover;
    private bool estaMuerto;

    // Jugador
    public int MAXVIDAS;
    public int vidaActual;
    public Vector3 spawnPoint = new Vector3(145f, 2.35f, 60f);

    // Referencias
    public Transform camaraTransform;
    public Transform lluviaParticulas;
    public GameObject moneda;

    // Eventos
    public UnityEvent<int> cambioVida;

    // Audios
    public AudioClip muerteSonido;
    public AudioClip danoSonido;
    public AudioClip caminarSonido;
    public AudioClip sonidoFondo;

    private void Start() {
        vidaActual = MAXVIDAS;
        sePuedeMover = true;
        estaMuerto = false;
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;

        cambioVida.Invoke(vidaActual);
    }

    private void Update() {
        // Comprobar muerte
        comprobarMuerte();

        // Si se puede mover y no ha muerto
        if (sePuedeMover && !estaMuerto) {
            caminar();
            salto();
        }

        lluviaParticulas.position = transform.position;
    }

    // -------------------------- MOVIMIENTO INICIO --------------------------
    void caminar() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direccionMovimiento = camaraTransform.forward * vertical + camaraTransform.right * horizontal;
        direccionMovimiento.y = 0;

        if (direccionMovimiento.magnitude > 0.1f) {
            direccionMovimiento.Normalize();

            Vector3 nuevaVelocidad = direccionMovimiento * velocidadJugador;
            nuevaVelocidad.y = rb.velocity.y;
            rb.velocity = nuevaVelocidad;
        }
        else {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    void salto()
    {
        estaEnSuelo = Physics.Raycast(transform.position, Vector3.down, raycastSaltoLength, saltable);

        if (Input.GetButtonDown("Jump") && estaEnSuelo) {
            rb.velocity = new Vector3(rb.velocity.x, fuerzaSaltoJugador, rb.velocity.z);
        }
    }
    // -------------------------- MOVIMIENTO FINAL --------------------------

    // -------------------------- JUGADOR INICIO --------------------------
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("agua")) {
            bajarVida();
            tpearJugador(spawnPoint);
        }

        if (other.gameObject.CompareTag("Casa")) {
            SceneManager.LoadScene("Fin");

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void bajarVida() {
        int vidaTemporal = vidaActual - 1;

        if (vidaTemporal < 0) {
            vidaActual = 0;
        }
        else {
            vidaActual = vidaTemporal;
            cambioVida.Invoke(vidaActual);
            comprobarMuerte();
            if (comprobarMuerte()) {
                ControladorSonido.Instance.ejecutarSonido(muerteSonido);
            }
            
        }
        ControladorSonido.Instance.ejecutarSonido(danoSonido);
    }

    bool comprobarMuerte() {
        if (vidaActual <= 0) {
            matar();
            menuPausaController.mostrarMenuMuerte();
            return true;
        }
        return false;
    }

    void matar() {
        estaMuerto = true;
        sePuedeMover = false;
        rb.velocity = Vector3.zero;
    }

    void tpearJugador(Vector3 posicionTP) {
        transform.position = posicionTP;
    }
    // -------------------------- JUGADOR FINAL --------------------------

    // -------------------------- GIZMOS INICIO --------------------------
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastSaltoLength);
    }
    // -------------------------- GIZMOS FINAL --------------------------
}
