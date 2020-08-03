using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PosManage : MonoBehaviour
{
    public RectTransform current;

    public Vector3 pos;

    public PosNumber Posnumber;
    public BtnNumber haveWhatStone;

    public MoveStone moveSInstance;
    public PosManage btnMInstance;
    public bool HavingStone;

    void Start()
    {
      
    }

  
    public void SavePosition()
    {
        if (moveSInstance.Aselected == true)
        {
            moveSInstance.WhereToGo = current.localPosition;
            if (moveSInstance.currentPos == moveSInstance.WhereToGo)//같은 위치를 클릭
            {
                moveSInstance.Aselected = false;
            }
            else //moveSInstance.currentPos!=moveSInstance.WhereToGo 다른 위치를 선택
            {
                MovePosition();
                moveSInstance.ChangeTurn();
                moveSInstance.Aselected = false;
            }
        }
        else
        {
        }
        //moveSInstance.WhereToGo = current.localPosition;

        
    }

    public void MovePosition()
    {
        moveSInstance.WhichToMove.localPosition = moveSInstance.WhereToGo;
    }

    
}
