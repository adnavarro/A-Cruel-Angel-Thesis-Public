using System;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [HideInInspector] public static Vector2 MaxPosition;
    [HideInInspector] public static Vector2 MinPosition;
    public float smoothing = 1f;
    private DateTime _timeForStopEffect;

    private void Start()
    {
        _timeForStopEffect = DateTime.Now;
    }

    private void LateUpdate()
    {
        if (transform.position != lookAt.position)
        {
            if (DateTime.Now >= _timeForStopEffect)
            {
                NormalMovement();
            }
            else
            {
                DrunkMovement();
            }
        }
    }

    private void NormalMovement()
    {
        var lookAtPosition = new Vector3(lookAt.position.x, lookAt.position.y, transform.position.z);
        lookAtPosition.x = Mathf.Clamp(lookAtPosition.x, MinPosition.x, MaxPosition.x);
        lookAtPosition.y = Mathf.Clamp(lookAtPosition.y, MinPosition.y, MaxPosition.y);
        transform.position = Vector3.Lerp(transform.position, lookAtPosition, smoothing);
    }

    private void DrunkMovement()
    {
        var lookAtPosition = new Vector3(lookAt.position.x, lookAt.position.y, transform.position.z);
        lookAtPosition.x = Mathf.Clamp(lookAtPosition.x, MinPosition.x, MaxPosition.x);
        lookAtPosition.y = Mathf.Clamp(lookAtPosition.y, MinPosition.y, MaxPosition.y);
        var step = smoothing * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, lookAtPosition, step);
    }

    public void SetEffectTime(float effectTime)
    {
        _timeForStopEffect = DateTime.Now.AddSeconds(effectTime);
    }
}