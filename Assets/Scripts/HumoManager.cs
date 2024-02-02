using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumoManager : MonoBehaviour
{
    public GameObject[] prefabsHumo;
    private readonly float intervaloAparicion = 1.8f;
    private readonly Vector2 rangoX = new(-0.45f, 0.4f);
    private readonly Vector2 rangoY = new(-0.16f, 0.1f);
    private readonly float distanciaMinimaMovimiento = 2.0f;
    public readonly float distanciaMaximaMovimiento = 2.6f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnSmoke), 0, intervaloAparicion);
    }

    void SpawnSmoke()
    {
        Vector3 posicionLocalHumo = new(
            Random.Range(rangoX.x, rangoX.y),
            Random.Range(rangoY.x, rangoY.y),
            0
        );

        // Convertir la posición local a una posición global
        Vector3 posicionGlobalHumo = transform.TransformPoint(posicionLocalHumo);

        GameObject humo = Instantiate(prefabsHumo[Random.Range(0, prefabsHumo.Length)], posicionGlobalHumo, Quaternion.identity, transform);
        // Gira el humo en Y 180 o 0 grados
        humo.transform.Rotate(0, Random.Range(0, 2) * 180, 0);
        StartCoroutine(AparecerHumo(humo));
    }

    IEnumerator AparecerHumo(GameObject humo)
    {
        // Calcula la posición final del humo
        Vector3 posicionInicial = humo.transform.position;
        float velocidadMovimiento = Random.Range(0.4f, 0.8f);
        float distanciaMovimiento = Random.Range(distanciaMinimaMovimiento, distanciaMaximaMovimiento);
        Vector3 posicionFinal = posicionInicial + (Vector3.up * distanciaMovimiento);

        float distanciaRecorrida = 0.0f;

        // Configura el color del humo
        var renderer = humo.GetComponent<Renderer>();
        Color colorOriginal = renderer.material.color;
        Color transparente = new(colorOriginal.r, colorOriginal.g, colorOriginal.b, 0);
        // Inicia con opacidad 0
        renderer.material.color = transparente;

        // Calcula la duración total del movimiento
        float duracionMovimiento = distanciaMovimiento / velocidadMovimiento;
        // Calcula el tiempo en el que se inicia el fade-out
        float inicioFadeOut = duracionMovimiento * 0.75f;

        while (distanciaRecorrida < distanciaMovimiento)
        {
            float paso = velocidadMovimiento * Time.deltaTime;
            humo.transform.position = Vector3.MoveTowards(humo.transform.position, posicionFinal, paso);
            distanciaRecorrida += paso;

            float progresoActual = distanciaRecorrida / velocidadMovimiento;

            if (progresoActual <= inicioFadeOut)
            {
                // Fade-in y mantenimiento de la opacidad
                float progresoFadeIn = progresoActual / inicioFadeOut;
                renderer.material.color = new Color(colorOriginal.r, colorOriginal.g, colorOriginal.b, Mathf.Clamp01(progresoFadeIn));
            }
            else
            {
                // Fade-out
                float progresoFadeOut = (progresoActual - inicioFadeOut) / (duracionMovimiento - inicioFadeOut);
                renderer.material.color = new Color(colorOriginal.r, colorOriginal.g, colorOriginal.b, 1 - Mathf.Clamp01(progresoFadeOut));
            }

            yield return null;
        }

        Destroy(humo);
    }
}
