using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesUI : MonoBehaviour
{
    [SerializeField] private GameObject titulo;

    void Start()
    {
        // Fade-in del título 500ms después de que inicie el juego
        LeanTween.alpha(titulo.GetComponent<RectTransform>(), 0, 1f);
        //LeanTween.alpha(titulo, 1, 1).setDelay(0.5f);
    }

}
