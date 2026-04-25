using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetUIElementToSelectOnInteraction : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Selectable elementToSelect;


    private void Reset()
    {
        eventSystem = FindFirstObjectByType<EventSystem>();
        
        if(eventSystem != null )
        {
            Debug.LogError("No EventSystem found in the scene. Please add one to the scene or assign it in the inspector.", this);
        }

    }


    public void JumpToElement()
    {
        if(eventSystem == null)
        {
            Debug.LogError("No EventSystem found. Cannot jump to element.", this);
            return;
        }

        if(elementToSelect == null)
        {
            Debug.LogError("No UI element assigned to select. Please assign one in the inspector.", this);
            return;
        }
        
        eventSystem.SetSelectedGameObject(elementToSelect.gameObject);
    }




}

