using UnityEngine;

public class PlayerSkinLoader : MonoBehaviour
{
    [SerializeField] private Renderer playerRenderer;

    private MaterialPropertyBlock _propBlock;

    public void LoadSkin(PlayerSkin playerSkin)
    {
        if (playerRenderer == null) return;

        _propBlock = new MaterialPropertyBlock();

        // Récupérer les propriétés actuelles du matériau
        playerRenderer.GetPropertyBlock(_propBlock);

        Debug.Log(playerSkin.color.ToString());
        // Modifier la couleur
        _propBlock.SetColor("_Color", playerSkin.color);

        // Appliquer les nouvelles propriétés au Renderer
        playerRenderer.SetPropertyBlock(_propBlock);
    }
}

