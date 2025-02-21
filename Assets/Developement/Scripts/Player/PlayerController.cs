using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BallController ballController;
    [SerializeField] private PlayerInputController inputController;
    [SerializeField] private PlayerSkinLoader skinLoader;
    
    public void InitializePlayer (InitializeData initializeData)
    {
        SO_InputVector so_input = ScriptableObject.CreateInstance<SO_InputVector>();

        ballController.Initialize(so_input);
        inputController.Initialize(initializeData.PlayerInput, so_input);
        skinLoader.LoadSkin(initializeData.PlayerSkin);
    }

    private void OnDestroy()
    {
        DestroyPlayer();
    }

    [EasyButtons.Button]
    public void DestroyPlayer ()
    {
        StopPlayer();

        ballController.DeInitialize();
        inputController.DeInitialize();

        Destroy(gameObject);
    }

    [EasyButtons.Button]
    public void LauchPlayer ()
    {
        Debug.Log("Je suis appeler pour rouler");
        ballController.Lauch(true);
        inputController.EnableBindingMap(true);

    }

    [EasyButtons.Button]
    public void StopPlayer()
    {
        ballController.Lauch(false);
        inputController.EnableBindingMap(false);
    }

    public struct InitializeData
    {
        public PlayerInput PlayerInput;
        public PlayerSkin PlayerSkin;
    }
}
