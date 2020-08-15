using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePanel_ForOnly : MonoBehaviour
{
    public CanvasGroup FirstPanel_FORONLY;
    public CanvasGroup SecondPanel_FORONLY;
    public CanvasGroup ThirdPanel_FORONLY;

    public CanvasGroup ThirdPanel_Win;
    public CanvasGroup ThirdPanel_Lose;

    public Text WinText;
    public Text LoseText;

    public Sprite MyImage;

    public ManageStone_ForOnly MSInstance;

    // Start is called before the first frame update
    void Start()
    {
        MainisFirst();
    }

    public void ClickColorBtn()
    {
        for (int i = 0; i < MSInstance.BTNS.Length; i++)
        {
            MSInstance.BTNS[i].GetComponent<Image>().sprite = MyImage;
        }
        MainisSecond();
    }

    public void ShowTheWinPanel(int Stone,int Turn)
    {
        MainisThird();
        CanvasGroupOn(ThirdPanel_Win);
        CanvasGroupOff(ThirdPanel_Lose);
        WinText.text = Turn.ToString() + "번 만에\n성공하셨습니다\n축하드립니다!!";
    }

    public void ShowTheLosePanel(int Stone, int Turn)
    {
        MainisThird();
        CanvasGroupOn(ThirdPanel_Lose);
        CanvasGroupOff(ThirdPanel_Win);
        int LastStone = 32 - Stone;
        LoseText.text = "더이상 이동할 수 없습니다\n" + "돌을 움직인 횟수 : " + Turn.ToString() +
            "\n움직인 돌의 개수 : " + LastStone.ToString() +
            "\n남은 돌의 개수 : " + Stone.ToString()+"\n수고하셨습니다";
    }

    private void MainisFirst()
    {
        CanvasGroupOn(FirstPanel_FORONLY);
        CanvasGroupOff(SecondPanel_FORONLY);
        CanvasGroupOff(ThirdPanel_FORONLY);
    }

    private void MainisSecond()
    {
        CanvasGroupOff(FirstPanel_FORONLY);
        CanvasGroupOn(SecondPanel_FORONLY);
        CanvasGroupOff(ThirdPanel_FORONLY);
    }
    private void MainisThird()
    {
        CanvasGroupOff(FirstPanel_FORONLY);
        CanvasGroupOff(SecondPanel_FORONLY);
        CanvasGroupOn(ThirdPanel_FORONLY);
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
}
