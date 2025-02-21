using UnityEngine;

public class PlayerSkinLoader : MonoBehaviour
{
    [SerializeField] private Renderer playerRenderer;

    private MaterialPropertyBlock _propBlock;

    public void LoadSkin(PlayerSkin playerSkin)
    {
        if (playerRenderer == null) return;

        _propBlock = new MaterialPropertyBlock();

        // R�cup�rer les propri�t�s actuelles du mat�riau
        playerRenderer.GetPropertyBlock(_propBlock);

        Debug.Log(playerSkin.color.ToString());
        // Modifier la couleur
        _propBlock.SetColor("_Color", playerSkin.color);

        // Appliquer les nouvelles propri�t�s au Renderer
        playerRenderer.SetPropertyBlock(_propBlock);
    }
}

