using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnPos_ForCar : MonoBehaviour
{
    //GameObject에 대한 정보
    public GameObject ImObject;
    public Outline BtnOutline;
    public CanvasGroup MyCanvasGroup;
    public Transform current;
    public Vector3 MyPos;

    public STONEOX TurnPointOX; //POS

    public BtnColor HaveWhatStone; //POS

    public ManageStone_ForCar MSInstance;
    public LimitPos_ForCar LPInstance;

    public bool HaveTurnAcess;

    private void Start()
    {
        ImObject = this.gameObject;
        MyCanvasGroup = ImObject.AddComponent<CanvasGroup>();
        current = this.transform;
        MyPos = current.localPosition;
        this.HaveTurnAcess = false;               

        if (tag == "BTN_FORCAR_B"||tag== "BTN_FORCAR_W")
        {
            BtnOutline = ImObject.AddComponent<Outline>();
            BtnOutline.effectDistance = new Vector2(3, -3);
            this.BtnOutline.enabled = false;
        }
    }

    private void Update()
    {
        
    }

    public void ClickBtn()
    {       

        //이동할 버튼과 버튼의 현재위치를 MS에 전달
        MSInstance.MoveBTN = this.ImObject;
        MSInstance.BtnSelected = true;
        //BtnPos를 찾기
        for (int o = 0; o < MSInstance.POSS.Length; o++)
        {
            if (MSInstance.POSS[o].transform.localPosition == MSInstance.MoveBTN.transform.localPosition)
            {
                MSInstance.MoveBtnPos = MSInstance.POSS[o];
                break;
            }
        }
        this.BtnOutline.enabled = true;
        MSInstance.CurrentPos = ImObject.transform.localPosition;

        
        //이동할 수 있는 곳을 미리 보여주기
        for (int i = 0; i < LPInstance.TurnPOSS.Count; i++)
        {
            //회전점에 있을 경우
            if (MSInstance.MoveBtnPos== LPInstance.TurnPOSS[i])
            {
                Invoke("CallLimitPos_O", 0.001f);
                Debug.Log("회전점에 위치하고 있습니다");
                MSInstance.ShowAvailablePos = true;
                break;
            }
        }
        //회전점에 있지 않을 경우
        if (MSInstance.ShowAvailablePos==false)
        {
            Debug.Log("회전점에 위치하고 있지 않습니다");
            Invoke("CallLimitPos_X", 0.001f);
            MSInstance.ShowAvailablePos = true;
        }
        MSInstance.TimetoChoosePos();
    }

    public void ClickPos()
    {
        MSInstance.PosToGo = this.MyPos;
        if (this.MyPos == MSInstance.CurrentPos) // 선택한 돌과 똑같은 위치를 선택할 경우 선택을 취소함.
        {
            MSInstance.MoveBTN.GetComponent<BtnPos_ForCar>().BtnOutline.enabled = false;
            Debug.Log("다시 입력해주세요");
            MSInstance.BtnSelected = false;
            MSInstance.ShowAvailablePos = false;
        }
        else // 선택한 돌과 다른 위치를 선택할 경우 그 위치로 이동한다
        {
            if (LPInstance.ifTurnAvailablePOSS.Contains(this.ImObject)) // 선택한 자리가 돌아서 갈 수 있는 곳에 있을 경우
            {
                for (int i = 0; i < LPInstance.ifTurnAvailablePOSS.Count; i++)
                {
                    if (LPInstance.ifTurnAvailablePOSS[i]== ImObject)
                    {
                        for (int l = 0; l <5; l++)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                if (LPInstance.MakedGrid[l, j] == MSInstance.MoveBtnPos)
                                {
                                    LPInstance.CallHowTurnMove(l, j);
                                    break;
                                }
                            }
                        }
                        MSInstance.MoveBTN.transform.localPosition=Vector3.MoveTowards(MSInstance.MoveBTN.transform.localPosition, MyPos, 0.05f);
                        MSInstance.MoveBTN.GetComponent<BtnPos_ForCar>().BtnOutline.enabled = false;
                        MSInstance.MovePos = this.ImObject;
                        MSInstance.MoveStone();
                        MSInstance.BtnSelected = false;
                        MSInstance.ShowAvailablePos = false;
                        MSInstance.WhoseTurn = !MSInstance.WhoseTurn;
                        break;
                    }
                }
            }
            else
            {
                MSInstance.MoveBTN.GetComponent<BtnPos_ForCar>().BtnOutline.enabled = false;
                MSInstance.MovePos = this.ImObject;
                MSInstance.MoveStone();
                MSInstance.BtnSelected = false;
                MSInstance.ShowAvailablePos = false;
                MSInstance.WhoseTurn = !MSInstance.WhoseTurn;
            }          
            
        }
        MSInstance.CheckBlackWhite();
    }

    private void CallLimitPos_O() // 클릭한 POS가 회전점에 위치해 있음
    {
        LPInstance.ShowAvailablePosForTurnBTN();
    }

    public void CallLimitPos_X() // 클릭한 POS가 회전점에 있지 않음
    {
        LPInstance.ShowAvailablePos();
    }
}
