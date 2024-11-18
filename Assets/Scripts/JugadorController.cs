using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorController : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 5f;
    private Rigidbody rb;
    public LayerMask capaSuelo; // Capa para identificar el suelo
    public float distanciaChequeoSuelo = 0.1f; // Distancia del raycast para el chequeo

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direccionMovimiento = Camera.main.transform.right * horizontal
                                       + Camera.main.transform.forward * vertical;
        direccionMovimiento.y = 0; // Evita movimiento vertical
        rb.AddForce(direccionMovimiento.normalized * velocidad);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && EstaEnElSuelo())
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }

    private bool EstaEnElSuelo()
    {
        // Raycast hacia abajo desde la posición del jugador para verificar si toca el suelo
        return Physics.Raycast(transform.position, Vector3.down, distanciaChequeoSuelo, capaSuelo);
    }
}
