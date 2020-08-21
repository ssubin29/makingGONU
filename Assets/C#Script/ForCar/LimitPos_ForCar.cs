using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class LimitPos_ForCar : MonoBehaviour
{
    public GameObject[] POSS;
    public GameObject[,] MakedGrid;
    public GameObject AvailableDPos;

    public List<GameObject> TurnPOSS;
    public List<GameObject> ifTurnAvailablePOSS;
    public List<int> ifTurnAvailablePOSSX;
    public List<int> ifTurnAvailablePOSSY;

    public Vector3[] Vector3ForRightUp;
    public Vector3[] Vector3ForRightDown;
    public Vector3[] Vector3ForLeftUp;
    public Vector3[] Vector3ForLeftDown;

    public int PossCount=0;

    public ManageStone_ForCar MSInstance;

    // Start is called before the first frame update
    void Start()
    {
        POSS = GameObject.FindGameObjectsWithTag("POS_FORCAR");
        for (int i = 0; i < POSS.Length; i++)
        {
            if (POSS[i].GetComponent<BtnPos_ForCar>().TurnPointOX==STONEOX.O)
            {
                TurnPOSS.Add(POSS[i]);
            }
        }
        PossCount = 0;
        MakedGrid = new GameObject[5, 5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                MakedGrid[i, j] = POSS[PossCount];
                PossCount++;
            }
        }
    }

    public void ShowAvailablePos()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (MakedGrid[i, j] == MSInstance.MoveBtnPos)
                {
                    Debug.Log("이동가능한 POS를 확인합니다");
                    CheckAvailablePos(i, j);
                    break;
                }
            }
        }
    }

    public void CheckAvailablePos(int X,int Y) // 이동가능한 위치를 찾는 함수 실행
    {
        OffNotAvailablePos();

        if (X>=1)
        {
            FindAvailablePos( X-1 , Y);
        }
        if (X<4)
        {
            FindAvailablePos(X+1 , Y);
        }
        if (Y>=1)
        {
            FindAvailablePos(X, Y-1);
        }
        if (Y<4)
        {
            FindAvailablePos(X , Y+1);
        }
        CanvasGroup CG = MakedGrid[X, Y].GetComponent<BtnPos_ForCar>().MyCanvasGroup;
        MSInstance.CanvasGroupOn(CG);
    }

    public void OffNotAvailablePos()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                MSInstance.CanvasGroupOff(MakedGrid[i, j].GetComponent<BtnPos_ForCar>().MyCanvasGroup);
            }
        }
    }

    public void FindAvailablePos(int X, int Y)
    {
        //만약 돌이 없다면 갈 수 없으므로 돌이 있는 경우에만 해당되도록 함
        if (MakedGrid[X,Y].GetComponent<BtnPos_ForCar>().HaveWhatStone==BtnColor.XXX)
        {
            CanvasGroup CG = MakedGrid[X, Y].GetComponent<BtnPos_ForCar>().MyCanvasGroup;
            MSInstance.CanvasGroupOn(CG);
        }        
    }

    public void ShowAvailablePosForTurnBTN()
    {
        ifTurnAvailablePOSS.Clear();
        ifTurnAvailablePOSSX.Clear();
        ifTurnAvailablePOSSY.Clear();
        OffNotAvailablePos();
        ShowAvailablePos(); //도는 것을 포기할 경우는?

        if (MSInstance.MoveBtnPos==MakedGrid[0,1])
        {
            Debug.Log("[0,1]위치에서 [1,0]으로 이동할 수 있습니다");
            CheckAvailablePosFixedX(1, 0);//왼
        }
        else if (MSInstance.MoveBtnPos == MakedGrid[0, 3])
        {
            Debug.Log("[0,3]위치에서 [1,4]으로 이동할 수 있습니다");
            CheckAvailablePosFixedX(1, 4);//오
        }
        else if (MSInstance.MoveBtnPos == MakedGrid[1,0])
        {
            Debug.Log("[1,0]위치에서 [0,1]으로 이동할 수 있습니다");
            CheckAvailablePosFixedY(0,1);//위
        }
        else if (MSInstance.MoveBtnPos == MakedGrid[1,4])
        {
            Debug.Log("[1,4]위치에서 [0,3]으로 이동할 수 있습니다");
            CheckAvailablePosFixedY(0,3);//위
        }
        else if (MSInstance.MoveBtnPos == MakedGrid[3,0])
        {
            Debug.Log("[3,0]위치에서 [4,1]으로 이동할 수 있습니다");
            CheckAvailablePosFixedY(4,1);//아래
        }
        else if (MSInstance.MoveBtnPos == MakedGrid[3,4])
        {
            Debug.Log("[3,4]위치에서 [4,3]으로 이동할 수 있습니다");
            CheckAvailablePosFixedY(4,3);//아래
        }
        else if (MSInstance.MoveBtnPos == MakedGrid[4,1])
        {
            Debug.Log("[4,1]위치에서 [3,0]으로 이동할 수 있습니다");
            CheckAvailablePosFixedX(3,0);//왼
        }
        else //(MSInstance.MoveBtnPos == MakedGrid[4,3])
        {
            Debug.Log("[4,3]위치에서 [3,4]으로 이동할 수 있습니다");
            CheckAvailablePosFixedX(3,4);//오
        }
    }

    private void HelpWait()
    {
        Debug.Log("이동합니다");
    }

    public  void CallHowTurnMove(int X,int Y)
    {
        string stringX = X.ToString();
        string stringY = Y.ToString();
        Invoke("Move" + stringX + "_" + stringY + "to", 0.0001f);
    }

    public void CheckAvailablePosFixedX(int X, int Y) //가로로 무한 이동
    {
        //현재 누구의 차례인지 파악
        BtnColor BC;
        BtnColor anotherBC;
        if (MSInstance.WhoseTurn == true)
        {
            BC = BtnColor.black;
            anotherBC= BtnColor.white;
        }
        else
        {
            BC = BtnColor.white;
            anotherBC = BtnColor.black;
        }

        //Y의 위치에 따라 완쪽부터 탐색할지 오른쪽부터 탐색할지 결정함
        if (Y == 0)
        {
            for (int i = 0; i < 5; i++) //왼쪽부터 01234
            {
                if (MakedGrid[X,i].GetComponent<BtnPos_ForCar>().HaveWhatStone != BC)
                {
                    CanvasGroup CG = MakedGrid[X, i].GetComponent<BtnPos_ForCar>().MyCanvasGroup;
                    MSInstance.CanvasGroupOn(CG);
                    ifTurnAvailablePOSS.Add(MakedGrid[X, i]);
                    ifTurnAvailablePOSSX.Add(X);
                    ifTurnAvailablePOSSY.Add(i);
                    if (MakedGrid[X, i].GetComponent<BtnPos_ForCar>().HaveWhatStone==anotherBC)
                    {
                        AvailableDPos = MakedGrid[X, i];
                        break;
                    }
                }
                else //MakedGrid[X, i].GetComponent<BtnPos_ForCar>().HaveWhatStone == BC)
                {
                    Debug.Log("가로이동경로의 같은 편 돌이 있습니다 이동을 제한합니다.");
                    break;
                }
            }
        }
        else
        {
            for (int i = 4; i >= 0; i--) //오른쪽부터 43210
            {
                if (MakedGrid[X, i].GetComponent<BtnPos_ForCar>().HaveWhatStone != BC)
                {
                    CanvasGroup CG = MakedGrid[X, i].GetComponent<BtnPos_ForCar>().MyCanvasGroup;
                    MSInstance.CanvasGroupOn(CG);
                    ifTurnAvailablePOSS.Add(MakedGrid[X, i]);
                    ifTurnAvailablePOSSX.Add(X);
                    ifTurnAvailablePOSSY.Add(i);
                    if (MakedGrid[X, i].GetComponent<BtnPos_ForCar>().HaveWhatStone == anotherBC)
                    {
                        AvailableDPos = MakedGrid[X, i];
                        break;
                    }
                }
                else //MakedGrid[X, i].GetComponent<BtnPos_ForCar>().HaveWhatStone == BC
                {
                    Debug.Log("가로이동경로의 같은 편 돌이 있습니다 이동을 제한합니다.");
                    break;
                }
            }
        }
    }

    public void CheckAvailablePosFixedY(int X, int Y) //세로로 무한 이동
    {
        //현재 누구의 차례인지 파악
        BtnColor BC;
        BtnColor anotherBC;
        if (MSInstance.WhoseTurn == true)
        {
            BC = BtnColor.black;
            anotherBC = BtnColor.white;
        }
        else
        {
            BC = BtnColor.white;
            anotherBC = BtnColor.black;
        }
        
        //X의 위치에 따라 왼쪽부터 탐색할지 오른쪽부터 탐색할지 결정함
        if (X==0)
        {
            for (int i = 0; i < 5; i++) //위부터 01234
            {
                if (MakedGrid[i, Y].GetComponent<BtnPos_ForCar>().HaveWhatStone != BC)
                {
                    CanvasGroup CG = MakedGrid[i, Y].GetComponent<BtnPos_ForCar>().MyCanvasGroup;
                    MSInstance.CanvasGroupOn(CG);
                    ifTurnAvailablePOSS.Add(MakedGrid[i, Y]);
                    ifTurnAvailablePOSSX.Add(i);
                    ifTurnAvailablePOSSY.Add(Y);
                    if (MakedGrid[i, Y].GetComponent<BtnPos_ForCar>().HaveWhatStone == anotherBC)
                    {
                        AvailableDPos = MakedGrid[i, Y];
                        break;
                    }
                }
                else //MakedGrid[i, Y].GetComponent<BtnPos_ForCar>().HaveWhatStone == BC
                {
                    Debug.Log("세로이동경로의 같은 편 돌이 있습니다 이동을 제한합니다.");
                    break;
                }
            }
        }
        else
        {
            for (int i = 4; i >= 0; i--) //아래쪽부터 43210
            {
                if (MakedGrid[i, Y].GetComponent<BtnPos_ForCar>().HaveWhatStone != BC)
                {
                    CanvasGroup CG = MakedGrid[i, Y].GetComponent<BtnPos_ForCar>().MyCanvasGroup;
                    MSInstance.CanvasGroupOn(CG);
                    ifTurnAvailablePOSS.Add(MakedGrid[i, Y]);
                    ifTurnAvailablePOSSX.Add(i);
                    ifTurnAvailablePOSSY.Add(Y);
                    if (MakedGrid[i, Y].GetComponent<BtnPos_ForCar>().HaveWhatStone == anotherBC)
                    {
                        AvailableDPos = MakedGrid[i, Y];
                        break;
                    }
                }
                else //MakedGrid[i, Y].GetComponent<BtnPos_ForCar>().HaveWhatStone == BC
                {
                    Debug.Log("세로이동경로의 같은 편 돌이 있습니다 이동을 제한합니다.");
                    break;
                }
            }
        }
    }

    public void UPtoDown(GameObject GO,Vector3[] V3)
    {
        for (int i = 0; i < 6; i++) //012345
        {
            GO.transform.localPosition = V3[i];
        }
    }

    public void DowntoUp(GameObject GO, Vector3[] V3)
    {
        for (int i = 5; i >=0; i--) //543210
        {
            GO.transform.localPosition = V3[i];
        }
    }

    //바퀴를 도는 애니메이션을 위해 Vector2의 집합들을 이용할거임
    //그것을 위한 함수들의 집합

    //오른쪽 위
    private void Move1_4to()
    {
        DowntoUp(MSInstance.MoveBTN, Vector3ForRightUp);
    }
    private void Move0_3to()
    {
        UPtoDown(MSInstance.MoveBTN, Vector3ForRightUp);
    }

    //오른쪽 아래
    private void Move3_4to()
    {
        UPtoDown(MSInstance.MoveBTN, Vector3ForRightDown);
    }
    private void Move4_3to()
    {
        DowntoUp(MSInstance.MoveBTN, Vector3ForRightDown);
    }

    //왼쪽 위
    private void Move1_0to()
    {
        DowntoUp(MSInstance.MoveBTN, Vector3ForLeftUp);
    }
    private void Move0_1to()
    {
        UPtoDown(MSInstance.MoveBTN, Vector3ForLeftUp);
    }

    //왼쪽 아래
    private void Move3_0to()
    {
        UPtoDown(MSInstance.MoveBTN, Vector3ForLeftDown);
    }
    private void Move4_1to()
    {
        DowntoUp(MSInstance.MoveBTN, Vector3ForLeftDown);
    }
}
