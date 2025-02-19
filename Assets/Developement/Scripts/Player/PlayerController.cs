using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BallController ballController;
    [SerializeField] private PlayerInputController inputController;
    
    public void InitializePlayer (InitializeData initializeData)
    {
        SO_InputVector so_input = ScriptableObject.CreateInstance<SO_InputVector>();

        ballController.Initialize(so_input);
        inputController.Initialize(initializeData.PlayerInput, so_input);
    }

    [EasyButtons.Button]
    public void LauchPlayer ()
    {
        ballController.Lauch(true);
        inputController.Lauch(true);

    }

    [EasyButtons.Button]
    public void StopPlayer()
    {
        ballController.Lauch(false);
        inputController.Lauch(false);
    }

    public struct InitializeData
    {
        public PlayerInput PlayerInput;
        public PlayerSkin PlayerSkin;
    }
}
