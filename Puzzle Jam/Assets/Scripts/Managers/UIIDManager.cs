using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class UIIDManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UIID uiid;
    public int index;

    public CombatManager combatManager;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        combatManager.UpdateTooltipUI(uiid, index);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        combatManager.UnloadTooltipUI();
    }
}
