using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CombatManager))]
public class UIIDManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UIID uiid;
    public int index;

    private CombatManager combatManager;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        combatManager.UpdateTooltipUI(uiid, index);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        combatManager.UnloadTooltipUI();
    }
}
