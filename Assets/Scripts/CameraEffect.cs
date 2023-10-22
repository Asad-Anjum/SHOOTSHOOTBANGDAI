using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    //Screen Shake Variables
    private Transform cameraTransform;
    private Vector3 originalCameraPosition;

    public float shakeDuration = 0.5f;
    public float shakeAmount = 0.1f;

    //Screen Color Variables
    public Color flashColor;
    public float flashDuration = 0.1f;
    public Camera cam;
    private Color originalBackgroundColor;

    void Awake()
    {
        cameraTransform = GetComponent<Transform>();
    }

    void Start()
    {
        cam = Camera.main;
        originalBackgroundColor = cam.backgroundColor;
    }

    public void Shake()
    {
        originalCameraPosition = cameraTransform.localPosition;
        StartCoroutine(DoShake());
    }

    IEnumerator DoShake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            Vector3 randomPos = originalCameraPosition + Random.insideUnitSphere * shakeAmount;

            cameraTransform.localPosition = randomPos;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalCameraPosition;
    }
    

    public void Flash()
    {
        StartCoroutine(DoFlash());
    }

    IEnumerator DoFlash()
    {
        cam.backgroundColor = flashColor;
        yield return new WaitForSeconds(flashDuration);
        cam.backgroundColor = originalBackgroundColor;
    }
}

