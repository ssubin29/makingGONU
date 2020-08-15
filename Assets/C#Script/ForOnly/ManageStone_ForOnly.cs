using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageStone_ForOnly : MonoBehaviour
{
    public GameObject[] POSS;
    public GameObject[] BTNS;

    public GameObject MoveBTN;
    public GameObject MovePos;
    public GameObject MoveBtnPos;

    public Vector3 CurrentPos;
    public Vector3 PosToGo;

    public LimitPos_ForOnly LPInstance;
    public ManagePanel_ForOnly MPInstance;

    public Text HelpTextS;
    public Text HelpTextT;

    public bool BtnSelected;
    public bool PosSelsected;
    public bool FindBtnPos;

    public CanvasGroup BtnGroup;
    public CanvasGroup PosGroup;

    public AudioSource Sound;

    public int StoneCount;
    public int TurnCount;

    // Start is called before the first frame update
    void Start()
    {
        //게임시작준비
        TurnCount = 1;
        StoneCount = 32;
        ShowStoneCount(StoneCount);
        ShowTurnCount(TurnCount);
        BTNS = GameObject.FindGameObjectsWithTag("BTN_FORONLY");
        POSS = GameObject.FindGameObjectsWithTag("POS_FORONLY");
        TimetoChooseBtn();
    }

    void Update()
    {
        if (this.BtnSelected == true&&this.FindBtnPos==false)
        {
            Debug.Log("BTNPOS 탐색");
            for (int o = 0; o < POSS.Length; o++)
            {
                if (POSS[o].transform.localPosition==MoveBTN.transform.localPosition)
                {
                    MoveBtnPos = POSS[o];
                    FindBtnPos = true;
                    break;
                }
            }
        }
    }

    public void TimetoChooseBtn()
    {
        CanvasGroupOn(BtnGroup);
        CanvasGroupOff(PosGroup);
    }
    public void TimetoChoosePos()
    {
        PosGroup.alpha = 0.5f;
        PosGroup.interactable = true;
        PosGroup.blocksRaycasts = true;
        CanvasGroupOffNotAlpha(BtnGroup);
    }

    public void ShowStoneCount(int CT)
    {
        string CTS = CT.ToString();
        HelpTextS.text = "남아 있는 돌의 개수 : " + CTS;
    }

    public void ShowTurnCount(int CT)
    {
        string CTS = CT.ToString();
        HelpTextT.text = CTS + "번째 시도";
    }

    public void MoveStone()
    {
        if (LPInstance.TheAvailableMovePos.Count!=0)
        {
            DestroyStone(); // 가운데에 낀 돌을 찾아 삭제합니다
        }        
        MoveBTN.transform.localPosition = PosToGo;
        TurnCount++;
        ShowTurnCount(TurnCount);
        Sound.Play();
    }

    public void DestroyStone()
    {
        List<GameObject> ListA = LPInstance.TheAvailableMovePos;
        int ACount = ListA.Count;
        List<GameObject> ListB = LPInstance.TheAvailableDestroyPos;
        while (ACount != 0)
        {
            if (ListA[ACount - 1] == MovePos)
            {
                break;
            }
            ACount--;
            Debug.Log(ACount);
        }

        BTNS = GameObject.FindGameObjectsWithTag("BTN_FORONLY");

        for (int f = 0; f < BTNS.Length; f++)
        {
            if (ListB[ACount - 1].transform.localPosition == BTNS[f].transform.localPosition)////
            {
                for (int r = 0; r < POSS.Length; r++)
                {
                    if (BTNS[f].transform.localPosition==POSS[r].transform.localPosition)
                    {
                        //CanvasGroup MCG = BTNS[f].GetComponent<BtnPos_ForOnly>().MyCanvasGroup;
                        Destroy(BTNS[f]);
                        POSS[r].GetComponent<BtnPos_ForOnly>().HaveStone = STONEOX.X;
                        StoneCount--;
                        ShowStoneCount(StoneCount);
                        Debug.Log("가운데에 낀 돌을 삭제합니다");
                        break;
                    }                    
                }
                break;                
            }
        }

        //만약 StoneCount가 0이라면 끝을 알리는 패널을 불러옴
        if (StoneCount == 0)
        {
            MPInstance.ShowTheWinPanel(StoneCount,TurnCount);
        }
        {

        }
    }

    public void CalltheCount()
    {
        MPInstance.ShowTheLosePanel(StoneCount, TurnCount);
    }


    public void CanvasGroupOn(CanvasGroup cg)
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

    private void CanvasGroupOnNotAlpha(CanvasGroup cg)
    {
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    private void CanvasGroupOffNotAlpha(CanvasGroup cg)
    {
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    
}
