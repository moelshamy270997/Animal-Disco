using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private static float currentSpeed = 5;
    private float rotationDuration = 0.5f, scallingDuration = 0.5f;
    private bool isDancing = false;
    public float rotationSpeed = 360f; // degrees per second


    void Update()
    {
        if (!isDancing)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                PerformRotateDance();

            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                PerformScallingDance();

            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                PerformCircleDance();
        }
        MovePlayer();
    }

    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0, currentSpeed) * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            transform.position += new Vector3(0, -currentSpeed) * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(currentSpeed, 0) * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
            transform.position += new Vector3(-currentSpeed, 0) * Time.deltaTime;
    }

    void PerformRotateDance()
    {
        if (isDancing) return;
        StartCoroutine(RotateDance(transform));
    }

    
    void PerformScallingDance()
    {
        if (isDancing) return;
        StartCoroutine(ScallingDance(transform));
    }

    void PerformCircleDance()
    {
        if (isDancing) return;
        StartCoroutine(CircleDance(transform));
    }
    
    IEnumerator RotateDance(Transform transform)
    {
        isDancing = true;
        float elapsedTime = 0f;
        while (elapsedTime < rotationDuration)
        {
            float angle = 360f * (Time.deltaTime / rotationDuration);
            transform.Rotate(Vector3.forward, angle);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.identity;
        isDancing = false;
        
    }

    IEnumerator CircleDance(Transform transform)
    {
        float timeElapsed = 0f;
        while(timeElapsed < 1f)
        {
            float angle = timeElapsed * rotationSpeed;
            Vector3 newPosition = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0f ) * 5f;
            transform.position = newPosition;
            timeElapsed += Time.deltaTime/3;
            PerformRotateDance();
            yield return null;
        }
    }
    IEnumerator ScallingDance(Transform transform)
    {
        isDancing = true;
        float timeElapsed = 0f;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 0.5f;

        while (timeElapsed < scallingDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timeElapsed / scallingDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;

        timeElapsed = 0;
        while (timeElapsed < scallingDuration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, timeElapsed / scallingDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
        isDancing = false;
    }

    public static void ChangeSpeed(float speed)
    {
        currentSpeed = speed;
    }
    public static void IncreaseScale(Transform transform)
    {
        transform.localScale = transform.localScale * 1.02f;
    }
    public static void DecreaseScale(Transform transform)
    {
        transform.localScale = transform.localScale / 1.02f;
    }
}
