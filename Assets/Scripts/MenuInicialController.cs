using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicialController : MonoBehaviour
{
    public void jugar() {
        SceneManager.LoadScene("Nivel1");
    }

    public void salir() {
        Application.Quit();
    }

    public void volverAJugar() {
        SceneManager.LoadScene("Inicio");
        Time.timeScale = 1f;
    }
}
