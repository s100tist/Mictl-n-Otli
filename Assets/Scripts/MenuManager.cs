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

    [Header("Créditos")]
    [SerializeField] private GameObject creditos;
    [SerializeField] private float duracionAnimacionCreditos = 1f;
    [SerializeField] private AudioClip sonidoCreditos;

    [Header("Códice")]
    [SerializeField] private GameObject codice;
    [SerializeField] private float duracionAnimacionCodice = 1f;
    [SerializeField] private AudioClip sonidoCodice;

    private readonly float DURACION_ANIMACION = 2f;

    void Start()
    {
        EntradaTitulo();
        EntradaMenu();
    }

    void EntradaTitulo()
    {
        tituloTexto = titulo.GetComponent<TextMeshProUGUI>();
        // Oculta el título
        tituloTexto.color = new Color(tituloTexto.color.r, tituloTexto.color.g, tituloTexto.color.b, 0);
        // Realiza el fade-in del título
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
        // Oculta el menú
        menu.GetComponent<CanvasGroup>().alpha = 0;
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


    public void Creditos()
    {
        // Muestra los créditos
        SonidoControlador.instancia.ReproducirSonido(sonidoCreditos);
        LeanTween.alphaCanvas(creditos.GetComponent<CanvasGroup>(), 1, duracionAnimacionCreditos).setEase(LeanTweenType.easeInExpo);
        creditos.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void CerrarCreditos()
    {
        // Oculta los créditos
        SonidoControlador.instancia.ReproducirSonido(sonidoCreditos);
        LeanTween.alphaCanvas(creditos.GetComponent<CanvasGroup>(), 0, duracionAnimacionCreditos).setEase(LeanTweenType.easeInExpo);
        creditos.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void Codice()
    {
        // Muestra el códice
        SonidoControlador.instancia.ReproducirSonido(sonidoCodice);
        LeanTween.alphaCanvas(codice.GetComponent<CanvasGroup>(), 1, duracionAnimacionCodice).setEase(LeanTweenType.easeInExpo);
        codice.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void CerrarCodice()
    {
        // Oculta el códice
        SonidoControlador.instancia.ReproducirSonido(sonidoCodice);
        LeanTween.alphaCanvas(codice.GetComponent<CanvasGroup>(), 0, duracionAnimacionCodice).setEase(LeanTweenType.easeInExpo);
        codice.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
