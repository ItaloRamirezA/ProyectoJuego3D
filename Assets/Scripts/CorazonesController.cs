using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CorazonesController : MonoBehaviour
{
    public List<Image> listaCorazones;
    public GameObject corazonesPrefab;
    public JugadorController jugadorController;
    public int indexActual;
    public Sprite corazonLleno;
    public Sprite corazonVacio;

    private void Awake() {
        jugadorController.cambioVida.AddListener(cambiarCorazones);
    }

    private void cambiarCorazones(int vidaActual)
    {
        if (!listaCorazones.Any()) {
            crearCorazones(vidaActual);
        } else {
            cambiarVida(vidaActual);
        }
    }
    private void crearCorazones(int cantidadMaximaVida)
    {
        for (int i = 0; i < cantidadMaximaVida; i++)
        {
            GameObject corazon = Instantiate(corazonesPrefab, transform);
            listaCorazones.Add(corazon.GetComponent<Image>());
        }
        indexActual = cantidadMaximaVida - 1;
    }

    private void cambiarVida(int vidaActual)
    {
        if (vidaActual <= indexActual) {
            quitarCorazones(vidaActual);
        } else {
            agregarCorazones(vidaActual);
        }
    }

    private void agregarCorazones(int vidaActual)
    {
        for (int i = indexActual; i < vidaActual; i++)
        {
           indexActual = i;
           listaCorazones[indexActual].sprite = corazonLleno;
        }
    }

    private void quitarCorazones(int vidaActual)
    {
        for (int i = indexActual; i >= vidaActual; i--)
        {
           indexActual = i;
           listaCorazones[indexActual].sprite = corazonVacio;
        }
    }
}
