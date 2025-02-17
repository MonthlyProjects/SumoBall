using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateUtility
{
    //the lower has the priority
    public const int LoadingPriority = 1;
    public const int NoControllerPriority = 5;
    public const int MainMenuPriority = 10;
    public const int PausePriority = 100;
    public const int FinishPriority = 200;
    public const int TrainingModePriority = 500;
    public const int RunTimePriority = 1000;
}
