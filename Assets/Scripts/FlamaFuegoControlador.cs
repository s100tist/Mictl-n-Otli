using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamaFuegoControlador : MonoBehaviour
{
    private Vector3 tamañoMinimo = new(0.64f, 0.76f, 0.7f);
    private Vector3 tamañoMaximo = new(0.76f, 0.64f, 0.7f);
    private readonly float VELOCIDAD_CRECIMIENTO = 1.2f;

    private float semilla;

    void Start()
    {
        semilla = Random.Range(0, 65000.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float escala = Mathf.PerlinNoise(semilla, Time.time * VELOCIDAD_CRECIMIENTO);
        transform.localScale = Vector3.Lerp(tamañoMinimo, tamañoMaximo, escala);
    }
}
