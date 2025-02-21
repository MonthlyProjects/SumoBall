using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundOverScorePanelDisplayer : MonoBehaviour
{
    [SerializeField] private Image playerSkinImage;
    [SerializeField] private TextMeshProUGUI totalScore;
    [SerializeField] private TextMeshProUGUI rank;

    public void DisplayScore(PlayerData playerData, int rank)
    {
        playerSkinImage.color = playerData.playerSkin.color;
        totalScore.text = playerData.TotalPlayerScore.ToString() + " kills";
        this.rank.text = "#" + rank.ToString();
    }
}
