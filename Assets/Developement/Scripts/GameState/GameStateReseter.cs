using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateReseter : MonoBehaviour
{
    [SerializeField] private bool resetGameOnStart;
    public bool HasBeenReset = false;
    private void Start()
    {
        if (resetGameOnStart)
        {
            StartCoroutine(WaitForGameStateNotNull());
        }
    }

    IEnumerator WaitForGameStateNotNull()
    {
        while (GameStateManager.Instance == null)
        {
            yield return null;
        }
        GameStateManager.Instance.ResetStates();
        HasBeenReset = true;
    }
}
