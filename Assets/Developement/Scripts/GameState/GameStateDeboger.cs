using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameState/NewDeboger")]
public class GameStateDeboger : ScriptableObject
{
    public StateValues StateValues;
    public List<GameState> ActiveStates;

}
[Serializable]
public struct StateValueOverrideDeboger
{
    public string Name;
    public string Value;

    public StateValueOverrideDeboger(string name, string value)
    {
        Name = name;
        Value = value;
    }
}