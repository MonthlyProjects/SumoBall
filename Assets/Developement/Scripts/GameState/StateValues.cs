using System;

[Serializable]
public class StateValues
{
    public StateValueTimeScale TimeScale;
    public StateValueInputGameActive InputGameActive;
    public StateValueShowCursor ShowCursor;
    public StateValueActivateUIInput ActivateUIInput;

    public void ResetValues()
    {
        TimeScale       = new ();
        InputGameActive = new ();
        ShowCursor      = new ();
        ActivateUIInput = new ();
    }
}
[Serializable]
public abstract class StateValueDataBase
{
    public bool Override;
}

[Serializable]
public abstract class StateValueData<T> : StateValueDataBase
{
    public T Value;
}

//TODO : DONT FORGET TO IMPLEMENT THE LOGIC OF SETTING THE VALUE IN THE GAMESTATEMANAGER WHEN CREATING A NEW STATE VALUE!! /!\ DONT REMOVE THIS LINE

#region StateValues Classes
[Serializable]
public class StateValueTimeScale : StateValueData<float>
{
}

[Serializable]
public class StateValueInputGameActive : StateValueData<bool>
{
}

[Serializable]
public class StateValueShowCursor : StateValueData<bool>
{
}
[Serializable]
public class StateValueActivateUIInput : StateValueData<bool>
{
}
#endregion