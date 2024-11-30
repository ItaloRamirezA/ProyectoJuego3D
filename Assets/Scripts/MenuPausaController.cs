using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausaController : MonoBehaviour
{
    public GameObject botonPausa;
    public GameObject menuPausa;
    public GameObject menuMuerte;
    private bool juegoPausado;
    
    public CamaraController camaraController;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (juegoPausado) {
                reanudar();
            } else {
                pausa();
            }
        }
    }

    public void pausa() {
        juegoPausado = true;

        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);

        camaraController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void reanudar() {
        juegoPausado = false;

        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);

        camaraController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void quit() {
        juegoPausado = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Inicio");
    }

    public void reiniciar() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Nivel1");
    }

    public void mostrarMenuMuerte() {
        botonPausa.SetActive(false);
        menuMuerte.SetActive(true);
        
        Time.timeScale = 0f;

        camaraController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
