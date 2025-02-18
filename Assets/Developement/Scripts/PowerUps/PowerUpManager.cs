using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private PowerUpCrate powerUpCrateObject;
    [SerializeField] private float minDurationBetweenSpawns;
    [SerializeField] private float maxDurationBetweenSpawns;
    [SerializeField] private List<Transform> _spawnPoints;
    private List<Transform> _unusedSpawnPoints;
    Coroutine waitToDrop;
    private void Awake()
    {
        StartWaitingForNextPowerUpToDrop();
        _unusedSpawnPoints = new List<Transform>();
        for(int i=0; i< _spawnPoints.Count; i++)
        {
            _unusedSpawnPoints.Add( _spawnPoints[i]);
        }
    }
    private void StartWaitingForNextPowerUpToDrop()
    {
        float secondsToWait = Random.Range(minDurationBetweenSpawns, maxDurationBetweenSpawns);
        waitToDrop = StartCoroutine(DropPowerUpInXSeconds(secondsToWait));
    }
    private IEnumerator DropPowerUpInXSeconds(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        if(_unusedSpawnPoints.Count > 0)
        {
            DropPowerUp();
        }
        StartWaitingForNextPowerUpToDrop();
    }
    private void DropPowerUp()
    {
        Transform spawnPoint = _unusedSpawnPoints[Mathf.FloorToInt(Random.Range(0,_unusedSpawnPoints.Count))];
        _unusedSpawnPoints.Remove(spawnPoint);
        PowerUpCrate crate = Instantiate(powerUpCrateObject);
        crate.transform.position = spawnPoint.position;
        crate.transform.parent = this.transform;
        crate.OnCratePickedUp.AddListener(()=>CratePickedUpEvent(spawnPoint));
    }
    private void CratePickedUpEvent(Transform spawnPoint)
    {
        _unusedSpawnPoints.Add(spawnPoint);
        StopCoroutine(waitToDrop);
        StartWaitingForNextPowerUpToDrop();
    }
}
