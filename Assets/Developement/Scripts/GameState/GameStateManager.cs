using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IGameStateCallBack
{
    void OnApplyGameStateOverride(GameStateOverride stateOverride);
    void OnApplyCallback();
}



public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    private List<IGameStateCallBack> callbacks = new List<IGameStateCallBack>();

    [SerializeReference] private List<GameState> currentGameStates = new List<GameState>();

    [SerializeField] private GameStateOverride gameStateOverride = new GameStateOverride();
    public GameStateOverride GameStateOverride { get { return gameStateOverride; } }
    public static GameState ActiveGameState => Instance.currentGameStates[0];
    public static Action<GameState> OnStateChanged;

    private bool isApplicationQuit = false;

    public GameObject pauseStateObject;
    public GameObject runTimeStateObject;
    public GameObject mainMenuStateObject;
    public GameObject finishStateObject;
    public GameObject trainingModeStateObject;
    public GameObject noControllerDetectedStateObject;
    public GameObject loadingStateObject;


    private IGameStateCallBack _lastCallBackCalled;
    //------------------------
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        RefreshState();
        StartCoroutine(RunTimeState());
    }
    //------------------------
    void SetPauseActive(bool isActive)
    {
        pauseStateObject.SetActive(isActive);
    }

    public void RegisterCallback(IGameStateCallBack callback)
    {
        callbacks.Add(callback);
        
    }

    public void UnRegisterCallback(IGameStateCallBack callback)
    {
        callbacks.Remove(callback);
    }

    //------------------------
    public void ApplyState(GameState state)
    {
        currentGameStates.Add(state);
        currentGameStates.Sort();
        RefreshState();
    }

    public bool RemoveState(GameState state)
    {
        bool value = currentGameStates.Remove(state);
        RefreshState();
        return value;

    }

    public void RemoveAllState(Type stateType)
    {
        for(int i = currentGameStates.Count; i-- > 0;)
        {
            if(currentGameStates[i].GetType() == stateType)
            {
                currentGameStates.RemoveAt(i);
            }
        }
        RefreshState();
    }

    public bool GetCurrentGameState(out GameState state)
    {
        if(currentGameStates.Count == 0)
        {
            state = null;
            return false;
        }
        state = currentGameStates[0];
        return true;
    }

    private void RefreshState()
    {
        //check if not quitting
        if(isApplicationQuit)
        {
            return;
        }

        //Reset.
        gameStateOverride.Reset();

        //State Apply
        for (int i = currentGameStates.Count; i-- > 0;)
        {
            currentGameStates[i].ApplyOverride(gameStateOverride);
        }
       
        
        //Final Apply.
        gameStateOverride.Apply();

        //CallBack.
        for (int i = callbacks.Count; i-- > 0;)
        {
            callbacks[i].OnApplyGameStateOverride(gameStateOverride);
        }

        if (currentGameStates.Count > 0 && _lastCallBackCalled != currentGameStates[0].GameStateBehaviourInstance)
        {
            _lastCallBackCalled = currentGameStates[0].GameStateBehaviourInstance;
            currentGameStates[0].GameStateBehaviourInstance.OnApplyCallback();
            OnStateChanged?.Invoke(currentGameStates[0]);
        }
    }
    IEnumerator RunTimeState()
    {
        yield return new WaitForSeconds(0.5f);
        runTimeStateObject.SetActive(true);
    }
}
