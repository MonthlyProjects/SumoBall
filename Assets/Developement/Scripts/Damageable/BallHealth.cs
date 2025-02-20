using UnityEngine;
using UnityEngine.Events;

public class BallHealth : MonoBehaviour
{
    public UnityEvent<Vector3> OnDeath;
    [SerializeField] private GameObject deathParticle;
    public void TakeDamage(float damage)
    {
        Kill();
    }
    public void Kill()
    {
        OnDeath?.Invoke(transform.position);
        Instantiate(deathParticle).transform.position = transform.position;
        gameObject.SetActive(false);
    }
}
