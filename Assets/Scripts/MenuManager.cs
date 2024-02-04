using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Título")]
    [SerializeField] private GameObject titulo;
    private TextMeshProUGUI tituloTexto;
    [SerializeField] private GameObject fondo;
    private Image fondoImagen;
    [SerializeField] private float delayTitulo = 1.5f;

    [Header("Menú")]
    [SerializeField] private GameObject menu;
    [SerializeField] private float delayMenu = 3f;

    private readonly float DURACION_ANIMACION = 2f;

    void Start()
    {
        EntradaTitulo();
        EntradaMenu();
    }

    void EntradaTitulo()
    {
        // Realiza el fade-in del título
        tituloTexto = titulo.GetComponent<TextMeshProUGUI>();
        LeanTween.value(titulo, 0, 1, DURACION_ANIMACION).setDelay(delayTitulo).setOnUpdate((float val) =>
        {
            Color color = tituloTexto.color;
            color.a = val;
            tituloTexto.color = color;
        });

        // Realiza el fade-in del fondo
        fondoImagen = fondo.GetComponent<Image>();
        LeanTween.alpha(fondoImagen.rectTransform, 0.5f, DURACION_ANIMACION).setDelay(delayTitulo);
    }

    void EntradaMenu()
    {
        // Realize el fade-in del menú
        LeanTween.alphaCanvas(menu.GetComponent<CanvasGroup>(), 1, DURACION_ANIMACION).setDelay(delayMenu).setEase(LeanTweenType.easeInExpo).setOnComplete(() =>
        {
            // Activa raycast target para que los botones sean clickeables
            menu.GetComponent<CanvasGroup>().blocksRaycasts = true;
        });
    }

    public void IniciarJuego()
    {
        SceneManager.LoadScene(1);
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

}
