using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LimitPos_ForOnly : MonoBehaviour
{
    public GameObject[] POSS;
    public GameObject[,] MakedGrid; //MakeGrid라는 배열 안에 현재의 배열을 집어넣음

    public List<GameObject> TheAvailableMovePos;
    public List<GameObject> TheAvailableDestroyPos;

    public List<GameObject> MustCheckPos;
    public List<int> MustCheckPosX;
    public List<int> MustCheckPosY;

    public ManageStone_ForOnly MSInstance;

    public int HelpSearch = 0;
    public int Count = 0;
    public int POSN = 0;

    void Start()
    {
        POSS = GameObject.FindGameObjectsWithTag("POS_FORONLY");
        MakedGrid = new GameObject[7, 7];
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                MakedGrid[i, j] = POSS[HelpSearch];
                HelpSearch++;
            }
        }
        //Debug.Log(POSS.Length);
        //Debug.Log("Fucking");
    }

    public void ShowAvailablePos()
    {
        FindThePos();//        
    }

    void FindThePos()//
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (MakedGrid[i, j] == MSInstance.MoveBtnPos)
                {
                    Debug.Log("이동가능한 POS를 확인합니다");
                    FindAvailablePos(MakedGrid[i, j], i, j);
                    break;
                }
            }
        }
    }

    public void FinalCheck() //턴이 끝날때마다 모든 위치가 사용가능한지 확인함
    {
        MustCheckPos.Clear();
        for (int Fin = 0; Fin < POSS.Length; Fin++)
        {
            if (POSS[Fin].GetComponent<BtnPos_ForOnly>().HaveStone==STONEOX.O)
            {
                MustCheckPos.Add(POSS[Fin]);
            }
        }

        Count = 0;
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                for (int h = 0; h < MustCheckPos.Count; h++)
                {
                    if (MustCheckPos[h]== MakedGrid[i, j])
                    {
                        FinalCheckAvailablePos(MakedGrid[i, j], i, j);
                    }                    
                }
            }
        }
        if (Count==0)
        {
            Debug.Log("게임이 끝났습니다");
            MSInstance.CalltheCount();
        }
    }

    private void FinalCheckAvailablePos(GameObject MG, int X, int Y)
    {
        //상하좌우로 이동가능한 자리를 찾음
        FindAvailablePosUP(MG, X, Y);
        FindAvailablePosDown(MG, X, Y);
        FindAvailablePosLeft(MG, X, Y);
        FindAvailablePosRight(MG, X, Y);
    }


    private void FindAvailablePos(GameObject MG, int X, int Y)
    {
        TheAvailableMovePos.Clear();
        TheAvailableDestroyPos.Clear();

        //여기서 MG는 누른 버튼이 있는 위치(게임오브젝트)
        Count = 0;

        //상하좌우로 이동가능한 자리를 찾음
        FindAvailablePosUP(MG, X, Y);
        FindAvailablePosDown(MG, X, Y);
        FindAvailablePosLeft(MG, X, Y);
        FindAvailablePosRight(MG, X, Y);

        Debug.Log(Count);
        //Debug.Log(MG);

        //찾은 자리를 통해 POS를 관리
        //먼저 모든 위치의 캔버스 그룹을 OFF함
        for (int i = 0; i < POSS.Length; i++)
        {
            CanvasGroup CG = POSS[i].GetComponent<BtnPos_ForOnly>().MyCanvasGroup;
            MSInstance.CanvasGroupOff(CG);
        }

        //만약 Count가 0이 아니라면 TheAvailableMovePos 안에 이동가능한 위치가 있다는 표시니 ON.
        if (Count != 0)
        {
            do
            {
                Count--;
                CanvasGroup CG = TheAvailableMovePos[Count].GetComponent<BtnPos_ForOnly>().MyCanvasGroup;
                MSInstance.CanvasGroupOn(CG);
            } while (Count!=0);
        }
        //만약 Count가 0이라면 이동가능한 위치가 없다는 소리니 패스
        else
        {
            Debug.Log("Count가 0입니다");
        }

        //마지막으로 사용자가 취소할 수 있도록 버튼 그 자체가 존재하는 위치도 ON
        MSInstance.CanvasGroupOn(MG.GetComponent<BtnPos_ForOnly>().MyCanvasGroup);
    }

    private void FindAvailablePosUP(GameObject MG, int X, int Y)
    {
        if (X > 1)
        {
            if (MakedGrid[X - 2, Y].GetComponent<BtnPos_ForOnly>().HaveStone == STONEOX.X)
            {
                if (MakedGrid[X - 1, Y].GetComponent<BtnPos_ForOnly>().HaveStone == STONEOX.O)
                {
                    TheAvailableMovePos.Add(MakedGrid[X - 2, Y]);
                    TheAvailableDestroyPos.Add(MakedGrid[X - 1, Y]);
                    Count++;
                    Debug.Log("위에 해당사항 있음");
                }
            }
        }
    }

    private void FindAvailablePosRight(GameObject MG, int X, int Y)
    {
        if (Y <= 4)
        {
            if (MakedGrid[X, Y + 2].GetComponent<BtnPos_ForOnly>().HaveStone == STONEOX.X)
            {
                if (MakedGrid[X, Y + 1].GetComponent<BtnPos_ForOnly>().HaveStone == STONEOX.O)
                {
                    TheAvailableMovePos.Add(MakedGrid[X, Y + 2]);
                    TheAvailableDestroyPos.Add(MakedGrid[X, Y + 1]);
                    Count++;
                    Debug.Log("우에 해당사항 있음");
                }
            }
        }
    }

    private void FindAvailablePosDown(GameObject MG, int X, int Y)
    {
        if (X < 5)
        {
            if (MakedGrid[X + 2, Y].GetComponent<BtnPos_ForOnly>().HaveStone == STONEOX.X)
            {
                if (MakedGrid[X + 1, Y].GetComponent<BtnPos_ForOnly>().HaveStone == STONEOX.O)
                {
                    TheAvailableMovePos.Add(MakedGrid[X + 2, Y]);
                    TheAvailableDestroyPos.Add(MakedGrid[X + 1, Y]);
                    Count++;
                    Debug.Log("아래에 해당사항 있음");
                }
            }
        }
    }

    private void FindAvailablePosLeft(GameObject MG, int X, int Y)
    {
        if (Y > 1)
        {
            if (MakedGrid[X, Y - 2].GetComponent<BtnPos_ForOnly>().HaveStone == STONEOX.X)
            {
                if (MakedGrid[X, Y - 1].GetComponent<BtnPos_ForOnly>().HaveStone == STONEOX.O)
                {
                    TheAvailableMovePos.Add(MakedGrid[X, Y - 2]);
                    TheAvailableDestroyPos.Add(MakedGrid[X, Y - 1]);
                    Count++;
                    Debug.Log("좌에 해당사항 있음");
                }
            }
        }
    }

}