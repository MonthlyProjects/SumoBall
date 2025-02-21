using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInfoPanel : MonoBehaviour
{
    [SerializeField] private Image playerSkinImage;
    [SerializeField] private TextMeshProUGUI killCount;
    [SerializeField] private GameObject playerDeadImage;

    public void InitializeUI(PlayerData playerData)
    {
        killCount.text = "0";
        playerData.playerObject.GetComponent<PlayerScoreRoundController>().OnScoreAdded.AddListener(UpdateKillCountText);

        playerSkinImage.color = playerData.playerSkin.color;

        playerDeadImage.SetActive(false);
        playerData.playerObject.GetComponent<BallHealth>().OnDeath.AddListener((Vector3 v) => EnablePlayerDeadImage());

    }
    private void UpdateKillCountText(float value)
    {
        killCount.text = value.ToString();
    }
    private void EnablePlayerDeadImage()
    {
        playerDeadImage.SetActive(true);
    }
}
