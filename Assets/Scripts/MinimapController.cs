using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class MinimapController : MonoBehaviour
{
    public Transform jugador;

    private void LateUpdate() {
        Vector3 posJugador = jugador.position;
        posJugador.y = transform.position.y;
        transform.position = posJugador;
    }
}
