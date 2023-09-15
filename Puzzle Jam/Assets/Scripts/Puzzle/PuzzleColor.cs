using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds information on a PuzzlePiece Color
/// </summary>
[CreateAssetMenu(fileName = "New Puzzle Color", menuName = "Puzzle/Color")]
public class PuzzleColor : ScriptableObject
{
    [Header("Color")]
    [SerializeField] private Color color;
    [Header("Name")]
    [SerializeField] private string colorName;

    /// <returns>The Color value</returns>
    public Color GetColor()
    {
        return color;
    }

    /// <returns>The color name</returns>
    public string GetName()
    {
        return colorName;
    }
}
