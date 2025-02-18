using UnityEngine;
using UnityEngine.Events;

public class PowerUpCrate : MonoBehaviour
{
    public UnityEvent OnCratePickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<BallController>(out BallController ball))
        {
            //DistributePowerUpToBall
            OnCratePickedUp.Invoke();
            Destroy(this.gameObject);
        }
    }
}
