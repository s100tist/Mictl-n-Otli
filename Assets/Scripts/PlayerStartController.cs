using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private AudioClip sonidoTelaMovimiento;
    [SerializeField] private AudioClip sonidoTelaEscape;
    [SerializeField] private AudioClip sonidoTierraEscape;

    private readonly float durationShake = 0.08f;
    private readonly float force = 0.2f;
    private readonly float rotationAmount = 2f;
    private readonly float displacement = 0.03f;

    private float cameraLeftTime, playerLeftTime, shakeForce, cameraShakeTime, playerShakeTime, cameraRotation, playerRotation;
    private Vector3 cameraIniPos, playerIniPos;
    private bool cameraShake, playerShake;

    private int chainCounter = 20;
    private bool playing = false;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        cameraShake = false;
        playerShake = false;
    }

    void Update()
    {
        if (playing) return;

        // Add shake when arrow keys are pressed
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (chainCounter > 0)
            {
                chainCounter--;
                SonidoControlador.instancia.ReproducirSonido(sonidoTelaMovimiento);
                StartShake(durationShake, force);
            }
            else
            {
                SonidoControlador.instancia.ReproducirSonido(sonidoTelaEscape);
                SonidoControlador.instancia.ReproducirSonido(sonidoTierraEscape);
                playing = true;
                player.SetActive(playing);
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MakeShake(ref cameraShake, displacement, ref cameraRotation, cameraIniPos, ref cameraLeftTime, ref cameraTransform);
        MakeShake(ref playerShake, displacement * 3, ref playerRotation, playerIniPos, ref playerLeftTime);
    }

    private void StartShake(float duration, float force)
    {
        shakeForce = force;

        if (!cameraShake)
        {
            cameraShakeTime = force / duration;
            cameraRotation = force * rotationAmount;
            cameraIniPos = cameraTransform.position;
            cameraShake = true;
            cameraLeftTime = duration;
        }

        if (!playerShake)
        {
            playerShakeTime = force / (duration * 4);
            playerRotation = force * rotationAmount;
            playerIniPos = transform.position;
            playerShake = true;
            playerLeftTime = duration * 4;
        }
    }

    private void MakeShake(ref bool shake, float shakeDisplacement, ref float shakeRotation, Vector3 iniPos, ref float leftTime, ref Transform position)
    {
        if (shake)
        {
            if (leftTime > 0F)
            {
                leftTime -= Time.deltaTime;

                float cantidadX = iniPos.x + Random.Range(-shakeDisplacement, shakeDisplacement) * shakeForce;
                float cantidadY = iniPos.y + Random.Range(-shakeDisplacement, shakeDisplacement) * shakeForce;
                cantidadX = Mathf.MoveTowards(cantidadX, iniPos.x, cameraShakeTime * Time.deltaTime);
                cantidadY = Mathf.MoveTowards(cantidadY, iniPos.x, cameraShakeTime * Time.deltaTime);
                position.position = new Vector3(cantidadX, cantidadY, iniPos.z);

                shakeRotation = Mathf.MoveTowards(shakeRotation, 0F, cameraShakeTime * rotationAmount * Time.deltaTime);
                position.rotation = Quaternion.Euler(0F, 0F, shakeRotation * Random.Range(-1F, 1F));
            }
            else
            {
                position.position = iniPos;
                shake = false;
            }
        }
    }

    private void MakeShake(ref bool shake, float shakeDisplacement, ref float shakeRotation, Vector3 iniPos, ref float leftTime)
    {
        if (shake)
        {
            if (leftTime > 0F)
            {
                leftTime -= Time.deltaTime;

                float cantidadX = iniPos.x + Random.Range(-shakeDisplacement, shakeDisplacement) * shakeForce;
                float cantidadY = iniPos.y + Random.Range(-shakeDisplacement, shakeDisplacement) * shakeForce;
                cantidadX = Mathf.MoveTowards(cantidadX, iniPos.x, playerShakeTime * Time.deltaTime);
                cantidadY = Mathf.MoveTowards(cantidadY, iniPos.x, playerShakeTime * Time.deltaTime);
                transform.position = new Vector3(cantidadX, cantidadY, iniPos.z);

                shakeRotation = Mathf.MoveTowards(shakeRotation, 0F, playerShakeTime * rotationAmount * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0F, 0F, shakeRotation * Random.Range(-1F, 1F));
            }
            else
            {
                transform.position = iniPos;
                shake = false;
            }
        }
    }
}
