using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoController : MonoBehaviour
{
    public int rutina;
    public int cronometro;
    public Animator animator;
    public Quaternion angulo;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
