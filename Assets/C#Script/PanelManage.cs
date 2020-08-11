using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManage : MonoBehaviour
{
    public CanvasGroup FirstPanel;
    public CanvasGroup SecondPanel;
    public CanvasGroup ThirdPanel;

    public CanvasGroup ThirdBWin;
    public CanvasGroup ThirdWWin;

    public MoveStone MSInstance;

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

    public void BlackStoneClick()
    {
        MSInstance.WhoseTurn = true;
        MSInstance.CheckBW();
        CanvasGroupOff(FirstPanel);
        CanvasGroupOn(SecondPanel);
    }
    public void WhiteStoneClick()
    {
        MSInstance.WhoseTurn = false;
        MSInstance.CheckBW();
        CanvasGroupOff(FirstPanel);
        CanvasGroupOn(SecondPanel);
    }

    // Start is called before the first frame update
    void Start()
    {
        MSInstance.WhoseTurn = true;
        CanvasGroupOn(FirstPanel);
        CanvasGroupOff(SecondPanel);
        CanvasGroupOff(ThirdPanel);
    }

    // Update is called once per frame
    public void Ending()
    {
        CanvasGroupOff(FirstPanel);
        CanvasGroupOff(SecondPanel);
        CanvasGroupOn(ThirdPanel);
        if (MSInstance.WhoseTurn==true)
        {
            CanvasGroupOn(ThirdWWin);
            CanvasGroupOff(ThirdBWin);
        }
        else
        {
            CanvasGroupOn(ThirdBWin);
            CanvasGroupOff(ThirdWWin);
        }
    }
}
