using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoPasos;
    [SerializeField] private AudioClip sonidoSalto;
    // Constantes
    private readonly float FUERZA_SALTO = 6;
    private readonly float VELOCIDAD_MOVIMIENTO = 5.2f;

    // Banderas
    private bool enSuelo = false;
    private bool morirá = false;

    // Variables
    private float velocidadActual;
    public GameObject mensajeFinal;
    private Rigidbody2D rb;
    private GameManager gameManager;
    private AudioSource audioSource;

    void Start()
    {
        // Mueve al jugador a la posición inicial
        transform.position = new Vector3(-1.1f, -1f, 0);

        // Obtiene el componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gameManager.IsGameOver()) return;

        // Mueve al jugador con las teclas de Flecha Izquierda y Derecha
        float horizontalInput = Input.GetAxis("Horizontal");
        float targetVelocity = horizontalInput * VELOCIDAD_MOVIMIENTO;
        float smoothVelocity = Mathf.SmoothDamp(GetComponent<Rigidbody2D>().velocity.x, targetVelocity, ref velocidadActual, 0.05f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(smoothVelocity, GetComponent<Rigidbody2D>().velocity.y);
        // Reproduce el sonido de pasos
        if (horizontalInput != 0 && enSuelo)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = sonidoPasos;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else if (enSuelo)
            audioSource.Stop();

        // Salta con la tecla de Flecha Arriba
        if (enSuelo && Input.GetKeyDown(KeyCode.UpArrow))
        {
            enSuelo = false;
            audioSource.clip = sonidoSalto;
            audioSource.loop = false;
            audioSource.Play();
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, FUERZA_SALTO);
        }

        // Verifica si el jugador ya no está en el suelo porque está cayendo
        if (rb.velocity.y < -4f) {
            enSuelo = false;
            audioSource.Stop();
        }


        // Verifica si el jugador perderá por caida
        if (!enSuelo && rb.velocity.y < -17f)
            morirá = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el jugador toca el suelo
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;

            if (morirá)
            {
                mensajeFinal.SetActive(true);
                gameManager.GameOver();
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tecpatl"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Limite Muerte"))
        {
            mensajeFinal.SetActive(true);
            gameManager.GameOver();
        }
    }
}
