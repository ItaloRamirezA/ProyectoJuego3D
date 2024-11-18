using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorController : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 5f;
    private Rigidbody rb;
    public LayerMask saltable; // Capa para identificar el suelo
    public float distanciaChequeoSuelo = 0.5f; // Ajustar distancia del raycast

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
        // Raycast desde la base del jugador
        Vector3 origenRaycast = transform.position - new Vector3(0, transform.localScale.y / 2, 0);
        return Physics.Raycast(origenRaycast, Vector3.down, distanciaChequeoSuelo, saltable);
    }
}
