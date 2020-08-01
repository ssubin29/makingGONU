using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BtnManage : MonoBehaviour,IPointerClickHandler
{
    public Button BTN;
    public RectTransform pos;
    public RectTransform whereTogo;
    private BtnNumber currentNumber;
    public TeamColor currentColor;
    public Vector3 a;
    public Vector3 b;
    bool isSelectedbyA=false;
    bool isSelectedbyB = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Down()
    {
        if (isSelectedbyA==false)
        {
            isSelectedbyA =! isSelectedbyA;
            a = pos.localPosition;
        }
        else // isSelected==true
        {
            if (a == pos.localPosition) 
            {
                print("같습니다");
                isSelectedbyA = !isSelectedbyA;
            }
            else
            {
                print("좌표가 다릅니다");
                isSelectedbyA = !isSelectedbyA;
            }
        }

    }


    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
