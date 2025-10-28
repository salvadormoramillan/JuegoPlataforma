using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public TMP_Text textopuntos;
    private int puntos;

    void Start()
    {
        puntos = 0;
        textopuntos.text = "0";
    }

    public void sumapuntos()
    {
        puntos++;
        textopuntos.text = puntos.ToString();
    }
    public void reiniciapuntos()
{
    puntos = 0;
    ActualizarUI();
}


    private void ActualizarUI()
    {
        if (textopuntos != null)
            textopuntos.text = "" + puntos;
    }
}