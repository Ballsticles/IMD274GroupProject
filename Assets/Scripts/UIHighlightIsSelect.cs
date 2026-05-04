using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIHighlightIsSelect : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    public static event Action OnSelectEvent;
    private Selectable thisSelectable;

    private void OnEnable()
    {
        thisSelectable = GetComponent<Selectable>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        OnSelectEvent?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!thisSelectable.IsInteractable())
            return;
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }
}
