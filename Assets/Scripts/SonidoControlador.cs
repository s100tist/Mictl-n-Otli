using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoControlador : MonoBehaviour
{
    public static SonidoControlador instancia;
    private AudioSource audioSource;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void ReproducirSonido(AudioClip sonido)
    {
        audioSource.PlayOneShot(sonido);
    }

}
