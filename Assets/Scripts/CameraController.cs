using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform jugador;

    void Start()
    {
        transform.position = new Vector3(0, 0, -10);
        MoveCameraTo(new Vector3(0, 0, -10), 3.2f);
    }

    void Update()
    {
        // Seccion 0
        if (jugador.position.x > -5.7f && jugador.position.x < 5.7f && jugador.position.y > -3.2f)
        {
            MoveCameraTo(new Vector3(0, 0, -10), 3.2f);
        }
        // Seccion 1
        else if (jugador.position.x > 5.7f && jugador.position.x < 34.15f && jugador.position.y > -10.9f)
        {
            MoveCameraTo(new Vector3(19.91f, -2.87f, -10));
        }
        // Seccion 2
        else if (jugador.position.x > 5.7f && jugador.position.x < 34.15f && jugador.position.y < -10.9f && jugador.position.y > -26.9f)
        {
            MoveCameraTo(new Vector3(19.91f, -18.872f, -10));
        }
        // Seccion 3
        else if (jugador.position.x > 34.15f && jugador.position.y > -26.9f)
        {
            MoveCameraTo(new Vector3(48.38f, -18.872f, -10));
        }
        // Seccion 4
        else if (jugador.position.x > 29.3f && jugador.position.y < -26.9f)
        {
            MoveCameraTo(new Vector3(48.89f, -37.83f, -10), 11);
        }
        // Seccion 5
        else if (jugador.position.x > 0.85f && jugador.position.x < 29.3f && jugador.position.y < -26.9f)
        {
            MoveCameraTo(new Vector3(15.02f, -34.88f, -10));
        }
        // Seccion 6
        else if (jugador.position.x > -46.01f && jugador.position.x < 0.85f && jugador.position.y < -23f)
        {
            MoveCameraTo(new Vector3(
                Mathf.Clamp(jugador.position.x, -31.8f, -13.5f),
                Mathf.Clamp(jugador.position.y, -35.9f, -31f),
                -10));
        }
        // Seccion 7
        else if (jugador.position.x < -22.8f && jugador.position.y < -10f && jugador.position.y > -23f)
        {
            MoveCameraTo(new Vector3(-34.34f, -16.46f, -10), 6.5f);
        }
        // Seccion 8
        else if (jugador.position.x > -22.8f && jugador.position.x < 5.7f && jugador.position.y > -21.9f && jugador.position.y < -6f)
        {
            MoveCameraTo(new Vector3(-8.55f, -13.95f, -10));
        }
    }

    private void MoveCameraTo(Vector3 posicion, float size = 8)
    {
        // Mueve suavemente la posición de la cámara a la posición dada
        transform.position = Vector3.Lerp(transform.position, posicion, 0.02f);
        // Cambia suavemente el tamaño de la cámara al tamaño dado
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, size, 0.02f);
    }
}
