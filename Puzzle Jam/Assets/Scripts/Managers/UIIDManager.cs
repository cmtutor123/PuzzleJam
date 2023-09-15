using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

/// <summary>
/// Manages an object that has a UIID and can be interacted with
/// </summary>
public class UIIDManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Identification Information")]
    [SerializeField] private UIID uiid;
    [SerializeField] private int index;
    [Header("Game Manager")]
    [SerializeField] private CombatManager combatManager;

    /// <summary>
    /// Updates the information in the tooltip when the pointer enters the object
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        combatManager.UpdateTooltipUI(uiid, index);
    }

    /// <summary>
    /// Removes the information in the tooltip when the pointer exits the object
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        combatManager.UnloadTooltipUI();
    }

    /// <summary>
    /// Sends information to the CombatManager when the object is clicked
    /// </summary>
    public void OnClick()
    {
        combatManager.ObjectClicked(uiid, index);
    }
}
