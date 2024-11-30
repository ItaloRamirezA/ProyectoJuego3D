using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausaController : MonoBehaviour
{
    public GameObject botonPausa;
    public GameObject menuPausa;
    private bool juegoPausado;

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

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void reanudar() {
        juegoPausado = false;

        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void quit() {
        juegoPausado = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Inicio");
    }
}
