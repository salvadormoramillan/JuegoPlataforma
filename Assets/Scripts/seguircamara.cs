using UnityEngine;

public class seguircamara : MonoBehaviour
{
    public Transform jugador;
    public float suavizado = 0.1f;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - jugador.position;
    }

    void LateUpdate()
    {
        if (jugador != null)
        {
            Vector3 nuevaPos = jugador.position + offset;
            transform.position = Vector3.Lerp(transform.position, nuevaPos, suavizado);
        }
    }
}
