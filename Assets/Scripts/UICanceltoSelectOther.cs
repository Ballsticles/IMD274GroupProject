using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICanceltoSelectOther : MonoBehaviour, ICancelHandler
{
    public Selectable objectToSelect;
    
    public void OnCancel(BaseEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(objectToSelect.gameObject);
    }
}
