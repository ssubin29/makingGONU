using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePanel_ForCar : MonoBehaviour
{
    public CanvasGroup FirstPanel_FORCAR;
    public CanvasGroup SecondPanel_FORCAR;
    public CanvasGroup ThirdPanel_FORCAR;

    public CanvasGroup ThirdBWin;
    public CanvasGroup ThirdWWin;

    public ManageStone_ForCar MSInstance;

    private void Start()
    {
        //MainisFirst();
    }

    public void CallThird()
    {
        MainisThird();
    }

    public void ClickBtninFirstPanel()
    {
        MainisSecond();
    }

    private void MainisFirst()
    {
        CanvasGroupOn(FirstPanel_FORCAR);
        CanvasGroupOff(SecondPanel_FORCAR);
        CanvasGroupOff(ThirdPanel_FORCAR);
    }

    private void MainisSecond()
    {
        CanvasGroupOff(FirstPanel_FORCAR);
        CanvasGroupOn(SecondPanel_FORCAR);
        CanvasGroupOff(ThirdPanel_FORCAR);
    }
    private void MainisThird()
    {
        CanvasGroupOff(FirstPanel_FORCAR);
        CanvasGroupOff(SecondPanel_FORCAR);
        if (MSInstance.WhoseTurn == true)
        {
            CanvasGroupOn(ThirdBWin);
            CanvasGroupOff(ThirdWWin);
        }
        else
        {
            CanvasGroupOn(ThirdWWin);
            CanvasGroupOff(ThirdBWin);
        }
        CanvasGroupOn(ThirdPanel_FORCAR);
    }

    private void CanvasGroupOn(CanvasGroup cg)
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

}
