using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The types of events that can trigger an effect
/// </summary>
public enum TriggerType
{
    None,
    Place,
    Destroy,
    Adjacent,
    Connected,
    Chain,
    Combo
}