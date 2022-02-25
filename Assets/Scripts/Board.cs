using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{

    [SerializeField] GameObject cellPrefab;

    public Cell[,] cells = new Cell[8, 8];

    void SetupBoard()
    {
        Color prevColor = Color.black;
        Color baseColor = Color.white;
        for (int x = 1; x <= 8; x++)
        {

            for (int y = 1; y <= 8; y++)
            {
                GameObject gameCell = Instantiate(cellPrefab, transform);
                RectTransform rectTransform = gameCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((x * 50) + 50, (y * 50) + 50);


                if (prevColor == baseColor)
                {
                    gameCell.GetComponent<Image>().color = Color.black;
                    prevColor = gameCell.GetComponent<Image>().color;
                }
                else
                {
                    prevColor = baseColor;
                }

                if (y == 8)
                {
                    if (prevColor == Color.black)
                    {
                        prevColor = Color.white;
                    }
                    else
                    {
                        prevColor = Color.black;
                    }
                }

            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupBoard();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
