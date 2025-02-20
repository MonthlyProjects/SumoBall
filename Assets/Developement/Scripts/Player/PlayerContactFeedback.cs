using UnityEngine;

public class PlayerContactFeedback : MonoBehaviour
{
    [SerializeField] private ParticleSystem playerContactVfx;
    [SerializeField] private BallController ballController;

    private void OnEnable()
    {
        ballController.OnContactWithOtherPlayer.AddListener(LaunchFX);
    }
    private void OnDisable()
    {
        ballController.OnContactWithOtherPlayer.RemoveListener(LaunchFX);
    }

    private void LaunchFX(Vector3 position)
    {
        playerContactVfx.transform.forward = position - transform.position;
        playerContactVfx.transform.position = position;
        playerContactVfx.Play();
    }
}
