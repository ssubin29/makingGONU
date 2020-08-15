using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PosManage : MonoBehaviour
{
    public Vector3 MyPos;

    public GameObject[] POSS_ifWhite;
    public GameObject[] POSS_ifBlack;
    public GameObject[] POS;
    public GameObject[] objects;

    public RectTransform current;

    public CanvasGroup MyCanvasGroup;

    public PosNumber currentPosnumber;
    
    public BtnColor haveWhatStone;

    public MoveStone moveSInstance;

    public AudioSource StoneAudio;


    int currentN = 0;

    void Start()
    {
        MyPos = this.current.localPosition;
        this.POS = GameObject.FindGameObjectsWithTag("POS");
        moveSInstance.Cselected = true;
    }

    private void Update()
    {
        if (moveSInstance.Aselected == true && moveSInstance.Cselected == true)
        {
            for (int k = 0; k < POS.Length; k++)
            {
                if (POS[k].GetComponent<PosManage>().MyPos==moveSInstance.currentPos)
                {
                    currentN = k;
                    Debug.Log(k);
                    if (moveSInstance.WhoseTurn == true)
                    {
                        FindAvailablePos_ForBlack();
                    }
                    else
                    {
                        FindAvailablePos_ForWhite();
                    }
                    moveSInstance.Cselected = false;
                    break;
                }
            }          
            
        }
        else
        {

        }
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
                moveSInstance.WhoToMove.GetComponent<BtnManage>().BtnOutline.enabled = false;
                CanvasGroup PS = moveSInstance.WhoToMove.GetComponent<BtnManage>().PosGroup;


                if (moveSInstance.WhoseTurn==true)
                {
                    CanvasGroup ReGroup = moveSInstance.WhoToMove.GetComponent<BtnManage>().BlackGroup;
                    ReGroup.interactable = true;
                    ReGroup.blocksRaycasts = true;
                    
                }
                else
                {
                    CanvasGroup ReGroup = moveSInstance.WhoToMove.GetComponent<BtnManage>().WhiteGroup;
                    ReGroup.interactable = true;
                    ReGroup.blocksRaycasts = true;
                }
                moveSInstance.WhoToMove.GetComponent<BtnManage>().CanvasGroupOff(PS);

            }
            else //moveSInstance.currentPos!=moveSInstance.WhereToGo 다른 위치를 선택
            {
                moveSInstance.Aselected = false;
                MovePosition();
                moveSInstance.WhoToMove.GetComponent<BtnManage>().BtnOutline.enabled = false;
                moveSInstance.ChangeTurn();
                CheckAvailablePos();
            }
            moveSInstance.Bselected = false;
        }

        else
        {
        }
        //moveSInstance.WhereToGo = current.localPosition;                
    }

    public void MovePosition()
    {
        moveSInstance.WhichToMove.localPosition = moveSInstance.WhereToGo; // 돌의 이동
        StoneAudio.Play();

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

    private void CanvasGroupOn(CanvasGroup cg)////////////////
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    private void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }



    public void FindAvailablePos_ForBlack()
    {
        for (int i = 0; i < POS.Length; i++)
        {
            CanvasGroupOff(POS[i].GetComponent<CanvasGroup>());
        }

        GameObject[] objects = POS[currentN].GetComponent<PosManage>().POSS_ifBlack;
        for (int j = 0; j < objects.Length; j++)
        {
            if (objects[j].GetComponent<PosManage>().haveWhatStone==BtnColor.XXX)
            {
                CanvasGroupOn(objects[j].GetComponent<CanvasGroup>());
            }
            else
            {
                CanvasGroupOff(objects[j].GetComponent<CanvasGroup>());
            }
        }
    }

    public void FindAvailablePos_ForWhite()
    {
        for (int i = 0; i < POS.Length; i++)
        {
            CanvasGroupOff(POS[i].GetComponent<CanvasGroup>());
        }

        GameObject[] objects = POS[currentN].GetComponent<PosManage>().POSS_ifWhite;
        for (int j = 0; j < objects.Length; j++)
        {
            if (objects[j].GetComponent<PosManage>().haveWhatStone == BtnColor.XXX)
            {
                CanvasGroupOn(objects[j].GetComponent<CanvasGroup>());
            }
            else
            {
                CanvasGroupOff(objects[j].GetComponent<CanvasGroup>());
            }
        }
    }

    public void CheckAvailablePos()
    {
        int Count = 0;

        if (moveSInstance.WhoseTurn == true)
        {
            for (int l = 0; l < POS.Length; l++)
            {
                if (POS[l].GetComponent<PosManage>().haveWhatStone == BtnColor.black)
                {
                    GameObject[] BlackObjects = POS[l].GetComponent<PosManage>().POSS_ifBlack;
                    for (int k = 0; k < BlackObjects.Length; k++)
                    {
                        if (BlackObjects[k].GetComponent<PosManage>().haveWhatStone == BtnColor.XXX)
                        {
                            Count++;
                        }
                        else
                        {
                        }
                    }
                }
                else
                {

                }
            }
        }
        else
        {
            for (int l = 0; l < POS.Length; l++)
            {
                if (POS[l].GetComponent<PosManage>().haveWhatStone == BtnColor.white)
                {
                    GameObject[] WhiteObjects = POS[l].GetComponent<PosManage>().POSS_ifBlack;
                    for (int k = 0; k < WhiteObjects.Length; k++)
                    {
                        if (WhiteObjects[k].GetComponent<PosManage>().haveWhatStone == BtnColor.XXX)
                        {
                            Count++;
                        }
                        else
                        {
                        }
                    }
                }
                else
                {

                }
            }
        }

        Debug.Log(Count);

        if (Count==0)
        {
            moveSInstance.CallEnd();
        }
        else
        {

        }
    }

}
