using System;
using R3;
using UnityEngine;

public class InputReactiveExample : MonoBehaviour
{
    private IDisposable  _subscription;

    private void OnEnable()
    {
        _subscription = Observable.EveryUpdate()
           .Where(_ => Input.GetKeyDown(KeyCode.Space))
           .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => Shoot());
    }

    private void Shoot()
    {
        Debug.Log("Пиу-пиу! 🔫");
    }

    private void OnDisable()
    {
        _subscription.Dispose(); // обязательно!
    }
}