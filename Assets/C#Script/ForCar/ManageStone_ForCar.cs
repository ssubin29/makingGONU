using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageStone_ForCar : MonoBehaviour
{
    public GameObject[] POSS;
    public GameObject[] BlackBTNS;
    public GameObject[] WhiteBTNS;

    public GameObject MoveBTN;
    public GameObject MovePos;
    public GameObject MoveBtnPos;

    public Vector3 CurrentPos;
    public Vector3 PosToGo;

    public LimitPos_ForCar LPInstance;

    public bool BtnSelected;
    public bool PosSelsected;
    public bool ShowAvailablePos;

    public bool WhoseTurn;

    public CanvasGroup WhiteGroup;
    public CanvasGroup BlackGroup;
    public CanvasGroup PosGroup;

    public CanvasGroup BlackText;
    public CanvasGroup WhiteText;

    public int BlackStoneCount;
    public int WhiteStoneCount;

    public AudioSource Sound;

    // Start is called before the first frame update
    void Start()
    {
        BlackStoneCount = 4;
        WhiteStoneCount = 4;
        BlackBTNS = GameObject.FindGameObjectsWithTag("BTN_FORCAR_B");
        WhiteBTNS = GameObject.FindGameObjectsWithTag("BTN_FORCAR_W");
        POSS = GameObject.FindGameObjectsWithTag("POS_FORCAR");

        //WhoseTurn이 true면 흑돌 차례 false면 백돌 차례(시작 준비)
        CheckBlackWhite();
    }

    public void CheckBlackWhite()
    {
        if (WhoseTurn == true)
        {
            TimetoChooseBlack();
        }
        else
        {
            TimetoChooseWhite();
        }
    }

    private void Update()
    {
        
    }


    public void MoveStone()
    {
        MoveBTN.transform.localPosition = PosToGo;
        Sound.Play();
        //enum 속성의 BtnColor가 이동
        BtnColor BC1 = MovePos.GetComponent<BtnPos_ForCar>().HaveWhatStone;
        BtnColor BC2 = MoveBtnPos.GetComponent<BtnPos_ForCar>().HaveWhatStone;
        MovePos.GetComponent<BtnPos_ForCar>().HaveWhatStone= BC2;
        MoveBtnPos.GetComponent<BtnPos_ForCar>().HaveWhatStone = BC1;
    }

    //CanvasGroup을 다룰 함수 모음들!!
    public void TimetoChooseWhite()
    {
        CanvasGroupOn(WhiteGroup);
        CanvasGroupOn(WhiteText);
        CanvasGroupOffNotAlpha(BlackGroup);
        CanvasGroupOff(BlackText);
        CanvasGroupOff(PosGroup);
    }

    public void TimetoChooseBlack()
    {
        CanvasGroupOn(BlackGroup);
        CanvasGroupOn(BlackText);
        CanvasGroupOffNotAlpha(WhiteGroup);
        CanvasGroupOff(WhiteText);
        CanvasGroupOff(PosGroup);
    }

    public void TimetoChoosePos()
    {
        CanvasGroupOn(PosGroup);
        CanvasGroupOffNotAlpha(BlackGroup);
        CanvasGroupOffNotAlpha(WhiteGroup);
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        if (cg==PosGroup)
        {
            cg.alpha = 0.5f;
        }
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
