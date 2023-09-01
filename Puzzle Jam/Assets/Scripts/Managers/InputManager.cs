using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CombatManager))]
public class InputManager : MonoBehaviour
{
    private CombatManager combatManager;

    private void Start()
    {
        combatManager = GetComponent<CombatManager>();
    }
}
