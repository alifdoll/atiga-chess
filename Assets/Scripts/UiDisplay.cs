using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiDisplay : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI player1;
    [SerializeField] TextMeshProUGUI player2;

    void Update()
    {
        var current_team = GetGridManager().CurrentPlayer;
        if (current_team == Team.WHITE)
        {
            player1.text = "YOUR TURN";
            player2.text = "WAITING";
        }
        else
        {
            player1.text = "WAITING";
            player2.text = "YOUR TURN";
        }
    }

    private GameObject GetGameManager()
    {
        return FindObjectOfType<GameManager>().gameObject;
    }

    private GridManager GetGridManager()
    {
        return FindObjectOfType<GridManager>().GetComponent<GridManager>();
    }
}
