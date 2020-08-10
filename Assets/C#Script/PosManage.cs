using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PosManage : MonoBehaviour
{
    public Vector3 MyPos;

    public CanvasGroup[] POSS_ifWhite;
    public CanvasGroup[] POSS_ifBlack;

    public Outline forOutlineOut;

    public RectTransform current;

    public CanvasGroup MyCanvasGroup;

    public PosNumber currentPosnumber;
    
    public BtnColor haveWhatStone;

    public MoveStone moveSInstance;
    public LimitPos LimitPInstance;

    public bool HavingStone;


    void Start()
    {
        MyPos = this.current.localPosition;       
    }
      
    public void SavePosition()
    {
        if (moveSInstance.Aselected == true)
        {
            moveSInstance.WhereToGo = current.localPosition;
            if (moveSInstance.currentPos == moveSInstance.WhereToGo)//같은 위치를 클릭
            {
                moveSInstance.Aselected = false;
                Debug.Log("다시 자리를 입력해주세요");
                //btnMInstance.MoveBtnToPos();
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
        moveSInstance.WhichToMove.localPosition = moveSInstance.WhereToGo; // 돌의 이동

        GameObject[] POS=GameObject.FindGameObjectsWithTag("POS");
        for (int i = 0; i < POS.Length; i++)
        {
            if (POS[i].transform.localPosition== moveSInstance.currentPos)
            {
                Debug.Log(POS[i].transform.localPosition);
                POS[i].GetComponent<PosManage>().haveWhatStone = BtnColor.XXX;//돌이 이동했다면 이동한 뒤는 XXX로
            }
        }

        // WhoseTurn이 true면 검정이고 false면 하양이므로 이동시킨 위치에 돌을 옮겼단 열거형으로 바꿈
        if (moveSInstance.WhoseTurn == true)
        {
            this.haveWhatStone = BtnColor.black;
        }
        else
        {
            this.haveWhatStone = BtnColor.white;
        }        
    }

    public void CheckAvailablePos()
    {
        
    }

}
