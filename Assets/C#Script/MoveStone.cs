using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveStone : MonoBehaviour
{
    public Text BlackT;
    public Text WhiteT;

    public RectTransform WhichToMove;

    public Vector3 currentPos;
    public Vector3 WhereToGo;

    public CanvasGroup PosGroup;
    public CanvasGroup WhiteGroup;
    public CanvasGroup BlackGroup;
    public CanvasGroup WinGroup;

    public bool Aselected; // Aselected는 이동할 버튼 선택 유무 Bselected는 이동시킬 위치선택 유무
    public bool Bselected; // A는 이동시키려는 버튼의 위치, B는 이동할 위치
    public bool WhoseTurn; // true면 BlackStart false면 WhiteStart


    // Start is called before the first frame update
    void Start()
    {
        Aselected = false;
        Bselected = false;
        WhoseTurn = true;
        CheckBW();
    }

    public void BlackStart()
    {     
        BlackT.enabled = true;
        BlackGroup.interactable = true;
        BlackGroup.blocksRaycasts = true;

        WhiteT.enabled = false;
        WhiteGroup.interactable = false;
        WhiteGroup.blocksRaycasts = false;

        PosGroup.alpha = 0;
        PosGroup.interactable = false;
        PosGroup.blocksRaycasts = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown("1") == true)
        {
            WinGroup.alpha = 1;
            WinGroup.blocksRaycasts = true;
            WinGroup.interactable = true;
        }
    }

    public void WhiteStart()
    {
        BlackGroup.interactable = false;
        BlackGroup.blocksRaycasts = false;
        BlackT.enabled = false;

        WhiteGroup.interactable = true;
        WhiteGroup.blocksRaycasts = true;
        WhiteT.enabled = true;

        PosGroup.alpha = 0;
        PosGroup.interactable = false;
        PosGroup.blocksRaycasts = false;
    }

    public void CheckBW()
    {
        if (WhoseTurn==true)
        {
            BlackStart();
        }

        else
        {
            WhiteStart();
        }

    }

    public void ChangeTurn()
    {
        Debug.Log("차례를 바꿉니다");
        WhoseTurn = !WhoseTurn;
        CheckBW();
    }

}
