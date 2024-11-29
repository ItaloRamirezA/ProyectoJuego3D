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
    Rigidbody rb;
    
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
        cronometro += 1 * Time.deltaTime;
        if (cronometro >= 4) {
            rutina = Random.Range(0, 2);
            cronometro = 0;
        }

        switch (rutina) {
            case 0:
                animator.SetBool("caminar", false);
                break;
            case 1:
                grado = Random.Range(0, 360);
                angulo = Quaternion.Euler(0, grado, 0);
                rutina++;
                break;
            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                Vector3 movimiento = transform.forward * velocidad * Time.deltaTime;
                rb.MovePosition(rb.position + movimiento);
                animator.SetBool("caminar", true);
                break;
        }
    }
}
