using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveStone : MonoBehaviour
{

    public RectTransform WhichToMove;
    public Vector3 currentPos;
    public Vector3 WhereToGo;

    public CanvasGroup PosGroup;
    public CanvasGroup WhiteGroup;
    public CanvasGroup BlackGroup;

    public bool Aselected; //Aelected는 이동할 버튼 선택 유무 Bselected는 이동시킬 위치선택 유무
    public bool Bselected; //A는 이동시키려는 버튼의 위치, B는 이동할 위치
    public bool BlackTurn;
    public bool WhiteTurn;


    // Start is called before the first frame update
    void Start()
    {
        Aselected = false;
        Bselected = false;
    }
}
