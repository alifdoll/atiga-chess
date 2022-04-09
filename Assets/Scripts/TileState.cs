using UnityEngine;
public class TileState : MonoBehaviour
{
    public enum State
    {
        Available,
        Unavailable,
        Move,
        Enemy,
        Selected
    }
}