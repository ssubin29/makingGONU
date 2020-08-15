using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnPos_ForOnly : MonoBehaviour
{
    public GameObject ImObject;

    public CanvasGroup MyCanvasGroup;

    public Outline BtnOutline;

    public Transform current;
    public Vector3 MyPos;
        
    public STONEOX HaveStone;

    public ManageStone_ForOnly MSInstance;
    public LimitPos_ForOnly LPInstance;

    private void Start()
    {
        ImObject =this.gameObject;
        MyCanvasGroup = ImObject.AddComponent<CanvasGroup>();       
        current = this.transform;
        MyPos=current.localPosition;

        if (tag=="BTN_FORONLY")
        {
            BtnOutline = ImObject.AddComponent<Outline>();
            BtnOutline.effectDistance = new Vector2(3, -3);
            this.BtnOutline.enabled = false; 
        }

        if (this.HaveStone==STONEOX.NULL)
        {
            this.MyCanvasGroup.alpha = 0;
            this.MyCanvasGroup.interactable = false; ;
            this.MyCanvasGroup.blocksRaycasts = false;
            this.MyCanvasGroup.ignoreParentGroups = true;
        }

    }

    public void ClickBtn()
    {        
        this.BtnOutline.enabled = true;

        //이동할 버튼과 버튼의 현재위치를 MS에 전달
        MSInstance.MoveBTN = ImObject;
        MSInstance.CurrentPos = ImObject.transform.localPosition;

        //이동할 수 있는 곳을 미리 보여주기
        MSInstance.BtnSelected = true;
        Invoke("CallLimitPos", 0.001f);//MoveBtnPos를 찾을 시간을 부여
        MSInstance.TimetoChoosePos();
    }    

    public void CallLimitPos()
    {
        LPInstance.ShowAvailablePos();
    }

    public void ClickPos()
    {
        MSInstance.PosToGo = this.MyPos;
        if (this.MyPos== MSInstance.CurrentPos)
        {
            MSInstance.MoveBTN.GetComponent<BtnPos_ForOnly>().BtnOutline.enabled = false;
            Debug.Log("다시 입력해주세요");
            MSInstance.BtnSelected = false;
            MSInstance.TimetoChooseBtn();
        }
        else
        {
            MSInstance.MoveBTN.GetComponent<BtnPos_ForOnly>().BtnOutline.enabled = false;
            MSInstance.MovePos = this.ImObject;
            MSInstance.MoveStone();
            MSInstance.BtnSelected = false;
            STONEOX STOX = MSInstance.MoveBtnPos.GetComponent<BtnPos_ForOnly>().HaveStone;
            MSInstance.MoveBtnPos.GetComponent<BtnPos_ForOnly>().HaveStone = MSInstance.MovePos.GetComponent<BtnPos_ForOnly>().HaveStone;
            MSInstance.MovePos.GetComponent<BtnPos_ForOnly>().HaveStone = STOX;
            MSInstance.TimetoChooseBtn();
            LPInstance.FinalCheck();
        }
        MSInstance.FindBtnPos = false;
    }
       

    

}
