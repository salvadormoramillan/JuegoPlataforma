using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Cargar la pantalla del juego
    public void Jugar()
    {
        SceneManager.LoadScene("Nivel1");
    }

    // Cargar la pantalla de créditos
    public void Creditos()
    {
        SceneManager.LoadScene("PantallaCreditos");
    }

    public void Volver()
    {
        SceneManager.LoadScene("PantallaInicio");
    }

    // Salir del juego
    public void Salir()
    {
        Debug.Log("Saliendo del juego..."); 
        Application.Quit();
    }
}
