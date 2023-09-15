using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages player input
/// </summary>
[RequireComponent(typeof(CombatManager))]
public class InputManager : MonoBehaviour
{
    private CombatManager combatManager;

    private void Start()
    {
        combatManager = GetComponent<CombatManager>();
    }
}
