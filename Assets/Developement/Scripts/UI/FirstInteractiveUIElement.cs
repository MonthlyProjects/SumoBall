using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FirstInteractiveUIElement : MonoBehaviour
{
    [SerializeField] private Button firstSelectedButton;

    private void OnEnable()
    {
        SetFirstSelected();
    }

    private void Start()
    {
        SetFirstSelected();
    }

    private void SetFirstSelected()
    {
        if (firstSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedButton.gameObject);
        }
        else
        {
            Debug.LogWarning("FirstSelectedButton: No button assigned in the inspector!", this);
        }
    }
}
