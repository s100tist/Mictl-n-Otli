using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotonMenuControlador : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float escalaHover = 1.04f;
    [SerializeField] private float escalaClick = 0.96f;
    [SerializeField] private float duracionAnimacion = 0.1f;
    [SerializeField] private AudioClip sonidoHover;
    [SerializeField] private AudioClip sonidoClick;
    private Vector3 escalaOriginal;
    private bool cursorEncima = false;

    // Start is called before the first frame update
    void Start()
    {
        escalaOriginal = transform.localScale;
    }

    // Cuando el cursor entra en el área del botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorEncima = true;
        LeanTween.scale(gameObject, escalaOriginal * escalaHover, duracionAnimacion);
        SonidoControlador.instancia.ReproducirSonido(sonidoHover);
    }

    // Cuando el cursor sale del área del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        cursorEncima = false;
        LeanTween.scale(gameObject, escalaOriginal, duracionAnimacion);
    }

    // Cuando el cursor hace clic en el botón
    public void OnPointerDown(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, escalaOriginal * escalaClick, duracionAnimacion);
        SonidoControlador.instancia.ReproducirSonido(sonidoClick);
    }

    // Cuando el cursor deja de hacer clic en el botón
    public void OnPointerUp(PointerEventData eventData)
    {
        if (cursorEncima)
            LeanTween.scale(gameObject, escalaOriginal * escalaHover, duracionAnimacion);
        else
            LeanTween.scale(gameObject, escalaOriginal, duracionAnimacion);
    }
}