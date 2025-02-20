using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LoadingRound : MonoBehaviour
{
    [SerializeField] private float loadingTime;
    [SerializeField] private RectTransform loadingImage;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameState loadingRoundState;
    [SerializeField] private GameState inRoundState;


    private void Start()
    {
        StartLoading();
    }
    public void StartLoading()
    {
        StartCoroutine(Loading());
        GameStateManager.Instance.AddState(loadingRoundState);
    }

    IEnumerator Loading()
    {
        float time = 0;
        loadingImage.gameObject.SetActive(true);
        while(time < loadingTime)
        {
            time += Time.deltaTime;
            loadingImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, time * rotationSpeed * 360));
            yield return null;
        }
        GameStateManager.Instance.RemoveState(loadingRoundState);
        GameStateManager.Instance.AddState(inRoundState);
        loadingImage.gameObject.SetActive(false);

    }
}
