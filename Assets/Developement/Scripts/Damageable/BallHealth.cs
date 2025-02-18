using UnityEngine;
using UnityEngine.Events;

public class BallHealth : MonoBehaviour
{
    public UnityEvent<Vector3> OnDeath;
    public void TakeDamage(float damage)
    {
        Kill();
    }
    public void Kill()
    {
        OnDeath?.Invoke(transform.position);
        gameObject.SetActive(false);
    }
}
