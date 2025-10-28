using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;


public class Jugador : MonoBehaviour
{
    [Header("******* Movimiento ******")]
    public float velocidad = 5f;
    private float movimientoX;
    private Rigidbody2D rb2d;

    [Header("******* Salto ***********")]
    public float fuerzaSalto = 7f;
    private bool enSuelo = false;

    [Header("******* Detección de suelo ***********")]
    public Transform comprobadorSuelo;
    public float radioSuelo = 0.1f;
    public LayerMask capaSuelo;

    [Header("*****Animacion****")]
    private Animator animator;

    [Header("******Sonido*******")]
    public AudioSource audioSource;
    public AudioClip musicaFondo;
    public AudioClip Clipkiwi;
    public AudioClip clipMuerte;

    private Vector3 posicionInicial;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        posicionInicial = transform.position;
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicaFondo;
        audioSource.loop = true;
        audioSource.volume = 0.5f;        
        audioSource.Play();
    }

    void Update()
    {
        rb2d.velocity = new Vector2(movimientoX * velocidad, rb2d.velocity.y);

        if (comprobadorSuelo != null)
        {
            enSuelo = Physics2D.OverlapCircle(comprobadorSuelo.position, radioSuelo, capaSuelo);
            animator.SetBool("estaSaltando", !enSuelo);
        }
        if (movimientoX == 0)
        {
            animator.SetBool("estaCorriendo", false);
        }
    }

    private void OnMove(InputValue inputMovimiento)
    {
        movimientoX = inputMovimiento.Get<Vector2>().x;

        if (movimientoX != 0)
        {
            Vector3 escala = transform.localScale;
            escala.x = Mathf.Sign(movimientoX) * Mathf.Abs(escala.x);
            transform.localScale = escala;
            animator.SetBool("estaCorriendo", true);
        }
    }

    private void OnJump(InputValue inputSalto)
    {
        if (enSuelo)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, fuerzaSalto);
            animator.SetBool("estaSaltando", true);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SueloMuerte"))
        {
            
            transform.position = posicionInicial;
            rb2d.velocity = Vector2.zero;
            
            var gameManager = FindAnyObjectByType<GameManager>();
            audioSource.PlayOneShot(clipMuerte);

            if (gameManager != null)
            {
                gameManager.reiniciapuntos();
            }
        }

        if (collision.transform.CompareTag("Comida"))
        {
            FindAnyObjectByType<GameManager>().sumapuntos();
            audioSource.PlayOneShot(Clipkiwi);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Enemigo"))
        {

            StartCoroutine(SonidoMuerteYReinicio());

        }
        if (collision.CompareTag("Final"))
        {
            int escenaActual = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(escenaActual + 1);
        }

    }
    private IEnumerator SonidoMuerteYReinicio()
    {
        audioSource.Stop();
        AudioSource.PlayClipAtPoint(clipMuerte, Camera.main.transform.position);
        yield return new WaitForSeconds(clipMuerte.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void OnDrawGizmos()
    {
        if (comprobadorSuelo != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(comprobadorSuelo.position, radioSuelo);
        }

    }
}

