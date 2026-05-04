using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LockCursorOnSelect : MonoBehaviour, ISelectHandler, ICancelHandler, IDeselectHandler
{
    private Selectable thisSelectable;
    private Navigation oldNavigation;

    private void OnEnable()
    {
        thisSelectable = GetComponent<Selectable>();
        oldNavigation = thisSelectable.navigation;
    }


    public void OnSelect(BaseEventData eventData)
    {
        oldNavigation = thisSelectable.navigation;
        
        var newNavigation = thisSelectable.navigation;
        newNavigation.mode = Navigation.Mode.None;
        thisSelectable.navigation = newNavigation;

    }

    

    public void OnCancel(BaseEventData eventData)
    {
        var newNavigation = thisSelectable.navigation;
        newNavigation = oldNavigation;
        thisSelectable.navigation = newNavigation;
        
    }

    public void OnDeselect(BaseEventData eventData)
    {
        var newNavigation = thisSelectable.navigation;
        newNavigation = oldNavigation;
        thisSelectable.navigation = newNavigation;
    }
}
