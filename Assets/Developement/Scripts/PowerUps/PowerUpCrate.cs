using UnityEngine;
using UnityEngine.Events;

public class PowerUpCrate : MonoBehaviour
{
    public UnityEvent OnCratePickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerPowerUpController>(out PlayerPowerUpController ball))
        {
            //DistributePowerUpToBall
            OnCratePickedUp.Invoke();

            //Random choose between all existing powerups
            switch(Random.Range(0,4))
            {
                case 0:
                    ball.DistributeEffect<PowerUpFreeze>();
                    break;
                case 1:
                    ball.DistributeEffect<PowerUpGoldenStar>();
                    break;
                case 2:
                    ball.DistributeEffect<PowerUpMitraillette>();
                    break;
                case 3:
                    ball.DistributeEffect<PowerUpScaleUp>();
                    break;
                default:
                    break;
            }


            Destroy(this.gameObject);
        }
    }
}
