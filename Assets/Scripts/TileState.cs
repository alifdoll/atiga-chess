using UnityEngine;
using System.Collections.Generic;
public class TileState : MonoBehaviour
{
    public enum State
    {
        Available,
        Unavailable,
        MoveTile,
        OutOfBounds,
        Enemy
    }
}