using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "Character/Data")]
public class CharacterData : ScriptableObject
{
    [Header("Sprites")]
    [SerializeField] private Sprite spritePuzzleBoard;

    // returns the puzzle board Sprite
    public Sprite GetSpritePuzzleBoard()
    {
        return spritePuzzleBoard;
    }
}
