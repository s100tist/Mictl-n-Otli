using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStartController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private readonly float durationShake = 0.08f;
    private readonly float force = 0.2f;
    private readonly float rotationAmount = 2f;
    private readonly float displacement = 0.03f;

    private float cameraLeftTime, playerLeftTime, shakeForce, cameraShakeTime, playerShakeTime, cameraRotation, playerRotation;
    private Vector3 cameraIniPos, playerIniPos;
    private bool cameraShake, playerShake;

    // Start is called before the first frame update
    void Start()
    {
        cameraShake = false;
        playerShake = false;
    }

    void Update()
    {
        // Add shake when arrow keys are pressed
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartShake(durationShake, force);
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

                float cantidadX = iniPos.x + UnityEngine.Random.Range(-shakeDisplacement, shakeDisplacement) * shakeForce;
                float cantidadY = iniPos.y + UnityEngine.Random.Range(-shakeDisplacement, shakeDisplacement) * shakeForce;
                cantidadX = Mathf.MoveTowards(cantidadX, iniPos.x, cameraShakeTime * Time.deltaTime);
                cantidadY = Mathf.MoveTowards(cantidadY, iniPos.x, cameraShakeTime * Time.deltaTime);
                position.position = new Vector3(cantidadX, cantidadY, iniPos.z);

                shakeRotation = Mathf.MoveTowards(shakeRotation, 0F, cameraShakeTime * rotationAmount * Time.deltaTime);
                position.rotation = Quaternion.Euler(0F, 0F, shakeRotation * UnityEngine.Random.Range(-1F, 1F));
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

                float cantidadX = iniPos.x + UnityEngine.Random.Range(-shakeDisplacement, shakeDisplacement) * shakeForce;
                float cantidadY = iniPos.y + UnityEngine.Random.Range(-shakeDisplacement, shakeDisplacement) * shakeForce;
                cantidadX = Mathf.MoveTowards(cantidadX, iniPos.x, playerShakeTime * Time.deltaTime);
                cantidadY = Mathf.MoveTowards(cantidadY, iniPos.x, playerShakeTime * Time.deltaTime);
                transform.position = new Vector3(cantidadX, cantidadY, iniPos.z);

                shakeRotation = Mathf.MoveTowards(shakeRotation, 0F, playerShakeTime * rotationAmount * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0F, 0F, shakeRotation * UnityEngine.Random.Range(-1F, 1F));
            }
            else
            {
                transform.position = iniPos;
                shake = false;
            }
        }
    }
}