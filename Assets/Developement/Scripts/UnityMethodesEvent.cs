using System;
using UnityEngine.Events;
using UnityEngine;

public class MethodUnityEvent: MonoBehaviour
{
    [SerializeField] private UnityEvent onAwake;
    [SerializeField] private UnityEvent onStart;


    private void Awake()
    {
        onAwake?.Invoke();
    }
    private void Start()
    {
        onStart?.Invoke();
    }
}
