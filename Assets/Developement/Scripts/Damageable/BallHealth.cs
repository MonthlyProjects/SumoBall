using UnityEngine;
using UnityEngine.Events;

public class BallHealth : MonoBehaviour
{
    public UnityEvent<Vector3> OnDeath;
    [SerializeField] private GameObject deathParticle;
    bool isDead;
    public void TakeDamage(float damage)
    {
        if(isDead) return;
        Kill();
    }
    public void Kill()
    {
        isDead = true;
        OnDeath?.Invoke(transform.position);
        Instantiate(deathParticle).transform.position = transform.position;
        gameObject.SetActive(false);
    }
}
