using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void cargarEscenaFinal() {
        SceneManager.LoadScene("Fin");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
