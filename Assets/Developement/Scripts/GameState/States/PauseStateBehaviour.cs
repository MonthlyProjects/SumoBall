using System.Collections.Generic;
using UnityEngine;
public class PauseStateBehaviour : GameStateBehaviour<PauseGameState>
{
    [SerializeField] List<bool> activeInputs;
    protected override void OnPreRegisterApplyState()
    { 
        RegisterInputs();
    }
    protected override void OnPostUnRegisterRemoveState()
    {
        ApplyInputs();
    }

    protected override void OnApplyGameStateOverrideImplement(GameStateOverride stateOverride)
    {
    }

    public void RegisterInputs()
    {
        
    }
    public void ApplyInputs()
    {
        
    }
}
