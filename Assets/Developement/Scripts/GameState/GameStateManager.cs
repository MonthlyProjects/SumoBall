using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    #region Properties

    public static GameStateManager Instance { get; private set; }
    [SerializeField] private List<GameState> _currentActiveStates;
    [SerializeField] private GameState defaultStateValues;
    [SerializeField] private GameStateDeboger gameStateDeboger;
    [SerializeField] private GameStatePriority gameStatePriority;
    [SerializeField] private TimeScaleModifier timeScaleModifier;

    public GameStateDeboger CurrentGameStateValues { get { return gameStateDeboger; } }
    public GameStatePriority AllGameStates { get {  return gameStatePriority; } }

    #endregion

    #region Init
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ResetStates();
    }

    public void ResetStates()
    {
        foreach (GameState state in gameStatePriority.gameStatesSortedByPriority)
        {
            state.IsActive = false;
        }
        _currentActiveStates = new List<GameState>();
        _currentActiveStates.Add(defaultStateValues);
        SetGameStateValues();
    }
    #endregion

    #region Accessible functions

    [EasyButtons.Button]
    public void AddState(GameState state)
    {
        if (!_currentActiveStates.Contains(state))
        {
            _currentActiveStates.Add(state);
            state.IsActive = true;

            SortStatesByPriority();
            SetGameStateValues();
        }
        else
        {
            Debug.LogWarning("Trying To Add Already Active States ! " + state.ToString());
        }
    }

    [EasyButtons.Button]
    public void RemoveState(GameState state)
    {
        if (_currentActiveStates.Contains(state))
        {
            _currentActiveStates.Remove(state);
            state.OnGameStateInactive?.Invoke();
            state.IsActive = false;

            SetGameStateValues();
        }
        else
        {
            Debug.LogWarning("Trying To Remove a non active States ! " + state.ToString());
        }
    }
    #endregion

    #region Logic
    private void SortStatesByPriority()
    {
        _currentActiveStates.Sort((x, y) =>
        {
            int indexX = gameStatePriority.gameStatesSortedByPriority.IndexOf(x);
            int indexY = gameStatePriority.gameStatesSortedByPriority.IndexOf(y);
            return indexX.CompareTo(indexY);
        });
        gameStateDeboger.ActiveStates = _currentActiveStates;
    }

    private void SetGameStateValues()
    {
        timeScaleModifier.RemoveTimeScale();
        gameStateDeboger.StateValues.ResetValues();
        for (int i = _currentActiveStates.Count - 1; i >= 0; i--)
        {
            //TimeScale
            SetTimeScale(_currentActiveStates[i].StateValues.TimeScale, i == _currentActiveStates.Count - 1);

            //InputGame
            SetInputGame(_currentActiveStates[i].StateValues.InputGameActive, i == _currentActiveStates.Count - 1);

            //Cursor Visible
            SetCursorVisible(_currentActiveStates[i].StateValues.ShowCursor, i == _currentActiveStates.Count - 1);

            //UI Input
            SetInputUI(_currentActiveStates[i].StateValues.ActivateUIInput, i == _currentActiveStates.Count - 1);
        }
    } 

    private void SetTimeScale(StateValueData<float> stateValue, bool isDefaultState)
    {
        if (stateValue.Override)
        {
            timeScaleModifier.TimeScale = stateValue.Value;
            timeScaleModifier.AddTimeScale();

            if (!isDefaultState)
            {
                gameStateDeboger.StateValues.TimeScale.Override = true;
                gameStateDeboger.StateValues.TimeScale.Value = stateValue.Value;
            }
        }
    }
    private void SetInputGame(StateValueData<bool> stateValue, bool isDefaultState)
    {
        if (stateValue.Override)
        {
            //InputManager.Instance.ActiveGameInputs(stateValue.Value);

            if (!isDefaultState)
            {
                gameStateDeboger.StateValues.InputGameActive.Override = true;
                gameStateDeboger.StateValues.InputGameActive.Value = stateValue.Value;
            }
        }
    }
    private void SetCursorVisible(StateValueData<bool> stateValue, bool isDefaultState)
    {
        if (stateValue.Override)
        {
#if !UNITY_EDITOR
            //Cursor.visible = stateValue.Value;
#endif
            if (!isDefaultState)
            {
                gameStateDeboger.StateValues.ShowCursor.Override = true;
                gameStateDeboger.StateValues.ShowCursor.Value = stateValue.Value;
            }
        }
    }
    private void SetInputUI(StateValueData<bool> stateValue, bool isDefaultState)
    {
        if (stateValue.Override)
        {
            //InputManager.Instance.ActiveUIInputs(_currentActiveStates[i].StateValues.ActivateUIInput.value);
            if (isDefaultState)
            {
                gameStateDeboger.StateValues.ActivateUIInput.Override = true;
                gameStateDeboger.StateValues.ActivateUIInput.Value = stateValue.Value;
            }
        }
    }
    #endregion

}
