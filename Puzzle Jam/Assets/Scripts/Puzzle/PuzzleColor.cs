using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle Color", menuName = "Puzzle/Color")]
public class PuzzleColor : ScriptableObject
{
    [Header("Color")]
    [SerializeField] private Color color;
    [Header("Name")]
    [SerializeField] private string colorName;

    // returns the Color
    public Color GetColor()
    {
        return color;
    }

    // returns the name of the PuzzleColor
    public string GetName()
    {
        return colorName;
    }
}
