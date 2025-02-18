using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<BallHealth>(out BallHealth ball))
        {
            ball.TakeDamage(1);
        }
    }
}
