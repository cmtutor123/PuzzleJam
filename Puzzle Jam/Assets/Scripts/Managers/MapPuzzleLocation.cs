using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPuzzleLocation : MonoBehaviour
{
    private int distance;
    private int number;
    
    public int GetDistance()
    {
        return distance;
    }

    public int GetNumber()
    {
        return number;
    }

    public void SetDistance(int distance)
    {
        this.distance = distance;
    }

    public void SetNumber(int number)
    {
        this.number = number;
    }
}
