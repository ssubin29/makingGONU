using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnManage : MonoBehaviour
{

    public RectTransform CurrentAPos;

    public CanvasGroup PosGroup;
    public CanvasGroup WhiteGroup;
    public CanvasGroup BlackGroup;
    
    public MoveStone moveSInstance;

    public Outline BtnOutline;

    public TeamColor MyColor;

    private void ResetPositionAB() // Aselected, Bselected를 false로 만들고 currentPos, WhereToGo를 초기화 하는 함수
    {
        moveSInstance.Aselected = false;
        moveSInstance.Bselected = false;
        moveSInstance.currentPos = new Vector3(0, 0,0);
        moveSInstance.WhereToGo = new Vector3(0, 0,0);
    }

    public void CanvasGroupOn(CanvasGroup cg)////////////////
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void StoneOff(CanvasGroup cg)
    {
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void StoneOn(CanvasGroup cg)
    {
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    private void Start()
    {
        CanvasGroupOff(PosGroup);
    }

    public void MoveBtnToPos()
    {
 
        if (moveSInstance.Aselected == false && moveSInstance.Bselected == false)
        {            
            Debug.Log("이동할 돌 A를 선택하셨습니다.");

            BtnOutline.enabled=true;
            
            CanvasGroupOn(PosGroup);/////////

            if (moveSInstance.WhoseTurn==false)
            {
                StoneOn(WhiteGroup);
                StoneOff(BlackGroup);
            }
            else
            {
                StoneOn(BlackGroup);
                StoneOff(WhiteGroup);
            }


            moveSInstance.Aselected = true;
            moveSInstance.currentPos = CurrentAPos.localPosition;
            moveSInstance.WhichToMove = CurrentAPos;

        }

        else if (moveSInstance.Aselected == true && moveSInstance.Bselected == false)
        {
            Debug.Log("A는 선택되었지만 B는 선택되지 않은 상태입니다");
            ResetPositionAB();

            /*if (moveSInstance.currentPos == moveSInstance.WhereToGo) // 똑같은 돌 두 번 선택
            {
                Debug.Log("A를 두 번 선택하셨습니다 A 선택을 취소합니다");
                ResetPositionAB(); // Aselected X Bselected X
            }

            else //moveSInstance.currentPos!=moveSInstance.WhereToGo 서로 다른 선택
            {
                Debug.Log("A와 B 모두 선택하셨습니다 A를 B 위치로 이동시키겠습니다");

                moveSInstance.Bselected = true; // Aselected O Bselected O
                
                Debug.Log("이동을 완료하였습니다. A와 B를 초기화합니다.");

                ResetPositionAB(); // Aselected X Bselected X
            }*/
        }
    }

    

}
