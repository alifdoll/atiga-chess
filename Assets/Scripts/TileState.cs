using UnityEngine;
using System.Collections.Generic;
public class TileState : MonoBehaviour
{
    public enum State
    {
        Occupied,
        Available,
        Unavailable,
        MoveTile,
        OutOfBounds,
        Enemy,
    }
}