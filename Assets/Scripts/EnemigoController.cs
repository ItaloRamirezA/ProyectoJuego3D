using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoController : MonoBehaviour
{
    private int rutina;
    private float cronometro;
    private Animator animator;
    private Quaternion angulo;
    private float grado;
    public float velocidad;
    private Rigidbody rb;
    public GameObject jugador;

    private float velocidadRotacion = 2f;
    public float distanciaDeteccion = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        comportamiento();
    }

    void comportamiento()
    {
        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.transform.position);

        if (distanciaAlJugador > distanciaDeteccion)
        {
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    animator.SetBool("caminar", false);
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, velocidadRotacion * Time.deltaTime);
                    Vector3 movimientoAleatorio = transform.forward * velocidad * Time.deltaTime;
                    rb.MovePosition(rb.position + movimientoAleatorio);
                    animator.SetBool("caminar", true);
                    break;
            }
        }
        else
        {
            Vector3 direccionHaciaJugador = jugador.transform.position - transform.position;
            direccionHaciaJugador.y = 0;

            Quaternion rotacionHaciaJugador = Quaternion.LookRotation(direccionHaciaJugador);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionHaciaJugador, 2);

            Vector3 posicionActual = transform.position;
            Vector3 movimientoHaciaJugador = direccionHaciaJugador.normalized * velocidad * Time.deltaTime;
            transform.position = new Vector3(posicionActual.x + movimientoHaciaJugador.x, posicionActual.y, posicionActual.z + movimientoHaciaJugador.z);

            animator.SetBool("caminar", true);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            JugadorController jugadorController = other.gameObject.GetComponent<JugadorController>();
            if (jugadorController != null)
            {
                jugadorController.bajarVida();
            }
        }
    }
}
