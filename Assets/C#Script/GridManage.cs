using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManage : MonoBehaviour
{
    [SerializeField]
    private int rows = 5;
    [SerializeField]
    private int cols = 3;
    [SerializeField]
    private float btnSize = 1;



    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();    
    }

    private void GenerateGrid()
    {
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("whiteStone"));

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = (GameObject)Instantiate(referenceTile, transform);

                float posX = col * btnSize;
                float posY = row * btnSize;

                tile.transform.position = new Vector2(posX, posY);
            }
        }
        Destroy(referenceTile);


        float gridW = cols * btnSize;
        float gridH = rows * btnSize;
        transform.position = new Vector2(-gridW/2+btnSize/2,gridH/2-btnSize/2);


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
