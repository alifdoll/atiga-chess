using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UiDisplayWinner : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winner;

    // Start is called before the first frame update
    void Start()
    {
        var winner_check = FindObjectOfType<GameManager>()
        .GetComponent<GameManager>().WINNER;

        if (winner_check == Team.WHITE)
        {
            winner.text = "PLAYER 1 WIN";
        }
        else
        {
            winner.text = "PLAYER 2 WIN";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
