using UnityEngine;

public class InstantiatePlayersInfoPanel : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private PlayerInfoPanel playerInfoPanelPrefab;

    [SerializeField] private RectTransform playersInfoPanelsHolder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameData.OnAllPlayersSpawn.AddListener(SpawnUIPlayersPanels);
    }

    private void SpawnUIPlayersPanels()
    {
        Debug.Log("SpawnPlayersPanelUI");

        foreach (PlayerData playerData in gameData.playersData)
        {
            Debug.Log(playerData.playerObject);
            PlayerInfoPanel spawnedObject = Instantiate(playerInfoPanelPrefab, playersInfoPanelsHolder);
            spawnedObject.InitializeUI(playerData);
        }
    }
}
