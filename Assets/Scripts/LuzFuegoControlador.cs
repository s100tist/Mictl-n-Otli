using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuzFuegoControlador : MonoBehaviour
{
    private Light2D luz;
    private readonly float intensidadMinima = 0.2f;
    private readonly float intensidadMaxima = 2f;
    private readonly float velocidadParpadeo = 1.7f;

    private float semilla;

    // Start is called before the first frame update
    void Start()
    {
        luz = GetComponent<Light2D>();
        semilla = Random.Range(0, 65000.0f);
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(semilla, Time.time * velocidadParpadeo);
        luz.intensity = Mathf.Lerp(intensidadMinima, intensidadMaxima, noise);
    }

}
