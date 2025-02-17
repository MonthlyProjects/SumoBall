using System.Linq;
using UnityEngine;

public static class GameObjectExtension
{
    public static void DestroyChildren(this GameObject t)
    {
        t.transform.Cast<Transform>().ToList().ForEach(c => Object.Destroy(c.gameObject));
    }

    public static void DestroyChildrenImmediate(this GameObject t)
    {
        t.transform.Cast<Transform>().ToList().ForEach(c => Object.DestroyImmediate(c.gameObject));
    }
    public static bool TryGetComponentInParent<T>(GameObject user, out T component) where T : Component
    {
        try
        {
            component = user.GetComponentInParent<T>();
            return component != null;
        }
        catch
        {
            component = null;
            return false;
        }
    }
    public static bool TryGetComponentInChildrens<T>(GameObject user, out T component) where T : Component
    {
        try
        {
            component = user.GetComponentInChildren<T>();
            return component != null;
        }
        catch
        {
            component = null;
            return false;
        }
    }
}
