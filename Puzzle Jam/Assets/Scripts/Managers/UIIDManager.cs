using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class UIIDManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Identification Information")]
    [SerializeField] private UIID uiid;
    [SerializeField] private int index;
    [Header("Game Manager")]
    [SerializeField] private CombatManager combatManager;

    // updates the information in the tooltip when the pointer enters the object
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        combatManager.UpdateTooltipUI(uiid, index);
    }

    // removes the information in the tooltip when the pointer exits the object
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        combatManager.UnloadTooltipUI();
    }

    // triggers information to combat manager
    public void OnClick()
    {
        combatManager.ObjectClicked(uiid, index);
    }
}
