using UnityEngine;

public class PlayerInstanceur : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlayerSkin defaultSkin;

    [EasyButtons.Button]
    public void InstancePlayers ()
    {
        //Check si l object
        for (int i = 0; i < gameData.playersData.Count; i++)
        {
            PlayerSkin playerSkin = gameData.playersData[i].playerSkin;

            if (playerSkin.skinPrefab == null)
            {
                playerSkin = defaultSkin;
            }

            gameData.playersData[i].playerObject = Instantiate(playerPrefab, new Vector3(0, 0.5f, i), Quaternion.identity, null);

            gameData.playersData[i].playerObject.GetComponent<PlayerController>().InitializePlayer(new PlayerController.InitializeData()
            {
                PlayerInput = gameData.playersData[i].playerInput,
                PlayerSkin = playerSkin
            });
        }
    }
}
