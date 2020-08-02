using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnManage : MonoBehaviour
{
    public RectTransform current;
    public Vector3 pos;
    public PosNumber currentNumber;
    public StnNumber haveWhatStone;
    public TeamColor currentColor;
    public MoveStone moveSInstance;
    public BtnManage btnMInstance;
    public HaveStone HS;
    

    void Start()
    {
        
    

        //Debug.Log(aaa.localPosition);
        //moveSInstance.currentPos = aaa.localPosition; //잘됨, 돌 이동에 참고하길
    }

    public void resetPositionAB() // Aselected, Bselected를 false로 만들고 currentPos, WhereToGo를 초기화 하는 함수
    {
        moveSInstance.Aselected = false;
        moveSInstance.Bselected = false;
        moveSInstance.currentPos = new Vector3(0, 0, 0);
        moveSInstance.WhereToGo = new Vector3(0, 0, 0);
    }


    public void savePosition()
    {
        if (moveSInstance.Aselected == false && moveSInstance.Bselected == false) // Aselected X Bselected X
        {
            
            Debug.Log("A를 선택하셨습니다.");
            moveSInstance.Aselected = true;
            moveSInstance.currentPos = current.localPosition;
        }
        else if (moveSInstance.Aselected == true && moveSInstance.Bselected==false) // Aselected O Bselected X
        {
            Debug.Log("A는 선택되었지만 B는 선택되지 않았습니다");
            moveSInstance.WhereToGo = current.localPosition;

            if (moveSInstance.currentPos==moveSInstance.WhereToGo)
            {
                Debug.Log("A를 두 번 선택하셨습니다 A 선택을 취소합니다");
                resetPositionAB(); // Aselected X Bselected X
            }

            else //moveSInstance.currentPos!=moveSInstance.WhereToGo
            {
                Debug.Log("A와 B 모두 선택하셨습니다 A를 B 위치로 이동시키겠습니다");
                moveSInstance.Bselected = true; // Aselected O Bselected O
                Debug.Log(moveSInstance.WhereToGo);

                Debug.Log("이동을 완료하였습니다. A와 B를 초기화합니다.");
                resetPositionAB(); // Aselected X Bselected X
            }
        }

    }

    public void ClickDown()
    {
        

    }


}
