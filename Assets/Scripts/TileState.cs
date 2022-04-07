using UnityEngine;
using System.Collections.Generic;
public class TileState : MonoBehaviour
{
    public enum State
    {
        Occupied,
        Available,
        Unavailable,
        Move,
        OutOfBounds,
        Enemy,
        Selected
    }
}