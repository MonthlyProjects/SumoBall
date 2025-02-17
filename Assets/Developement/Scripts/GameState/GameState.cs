using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class GameState : IComparable<GameState>
{
    #region Global
    /// <summary>
    /// Minimum is the heighest priority
    /// </summary>
    public abstract int Priority { get; }
    public abstract IGameStateCallBack GameStateBehaviourInstance { get; set; }
    public GameState()
    {

    }
    #endregion

    #region Operator

    public static bool operator >(GameState a, GameState b)
    {
        return a.Priority > b.Priority;
    }
    public static bool operator <(GameState a, GameState b)
    {
        return !(a < b);
    }
    public static bool operator >=(GameState a, GameState b)
    {
        return a.Priority >= b.Priority;
    }
    public static bool operator <=(GameState a, GameState b)
    {
        return !(a <= b);
    }

    #endregion

    #region Comparer
    public int CompareTo(GameState other)
    {
        return Priority.CompareTo(other.Priority);
    }
    #endregion

    #region Override
    public abstract void ApplyOverride(GameStateOverride stateOverride);
    public override string ToString()
    {
        return GetType().Name;
    }

    #endregion
}

[Serializable]
public class GameStateOverride
{
    public bool isPaused;
    public float timeScale = 1f;
    public bool inputMovementActive = true;
    public bool showCursor = true;
    public bool inputPracticeModeActive = false;
    public bool inputUIActive = true;

    public void Reset()
    {
        isPaused = false;
        timeScale = 1f;
        inputMovementActive = true;
        showCursor = true;
    }
    public void Apply()
    {
        Time.timeScale = timeScale;
        Cursor.visible = showCursor;
    }
}

public class RuntimeGameState : GameState
{
    public override int Priority => GameStateUtility.RunTimePriority;
    public override IGameStateCallBack GameStateBehaviourInstance { get; set; }

    public override void ApplyOverride(GameStateOverride stateOverride)
    {
        stateOverride.timeScale = 1f;
        stateOverride.showCursor = false;
        stateOverride.inputUIActive = false;
        stateOverride.inputMovementActive = true;
    }
}
public class TrainingModeState : GameState
{
    public override int Priority => GameStateUtility.TrainingModePriority;
    public override IGameStateCallBack GameStateBehaviourInstance { get; set; }

    public override void ApplyOverride(GameStateOverride stateOverride)
    {
        stateOverride.inputPracticeModeActive = true;
    }
}
public class FinishState : GameState
{
    public override int Priority => GameStateUtility.FinishPriority;
    public override IGameStateCallBack GameStateBehaviourInstance { get; set; }

    public override void ApplyOverride(GameStateOverride stateOverride)
    {
        stateOverride.timeScale = 0.2f;
        stateOverride.showCursor = true;
        stateOverride.inputMovementActive = false;
        stateOverride.inputPracticeModeActive = false;
    }
}
public class PauseGameState : GameState
{
    public override int Priority => GameStateUtility.PausePriority;
    public override IGameStateCallBack GameStateBehaviourInstance { get; set; }

    public override void ApplyOverride(GameStateOverride stateOverride)
    {
        stateOverride.isPaused = true;
        stateOverride.timeScale = 0f;
        stateOverride.showCursor = true;
        stateOverride.inputPracticeModeActive = false;
        stateOverride.inputMovementActive = false;
        stateOverride.inputUIActive = true;
    }
}
public class MainMenuState : GameState
{
    public override int Priority => GameStateUtility.MainMenuPriority;
    public override IGameStateCallBack GameStateBehaviourInstance { get; set; }

    public override void ApplyOverride(GameStateOverride stateOverride)
    {
        stateOverride.timeScale = 1f;
        stateOverride.showCursor = true;
        stateOverride.inputPracticeModeActive = false;
        stateOverride.inputMovementActive = false;
        stateOverride.inputUIActive = true;
    }
}
public class NoControllerDetectedState : GameState
{
    public override int Priority => GameStateUtility.NoControllerPriority;
    public override IGameStateCallBack GameStateBehaviourInstance { get; set; }

    public override void ApplyOverride(GameStateOverride stateOverride)
    {
        stateOverride.timeScale = 0f;
        stateOverride.showCursor = true;
    }
}
public class LoadingState : GameState
{
    public override int Priority => GameStateUtility.NoControllerPriority;
    public override IGameStateCallBack GameStateBehaviourInstance { get; set; }

    public override void ApplyOverride(GameStateOverride stateOverride)
    {
        stateOverride.inputMovementActive = false;
        stateOverride.inputPracticeModeActive = false;
        stateOverride.inputUIActive = false;
    }
}

