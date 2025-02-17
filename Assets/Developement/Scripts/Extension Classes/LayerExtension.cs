using UnityEngine;

public static class LayerExtension
{
    public static bool IsInLayer(GameObject gameObject,LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << gameObject.layer));
    }
}
