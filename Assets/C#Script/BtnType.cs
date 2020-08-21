using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnType : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defaultScale;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    public CanvasGroup gameGroup;
    public CanvasGroup helpGroup;
    public Text onoffText;
    new public AudioSource audio;//

    private void Start()
    {
        buttonScale = this.transform;
        defaultScale = buttonScale.localScale;
    }

    bool isSound;


    public void OnBtnClick()
    {
        switch (currentType)
        {
            case (BTNType.GameStart):
                Debug.Log("새 게임");
                CanvasGroupOff(mainGroup);
                CanvasGroupOn(gameGroup);
                break;
            
            case (BTNType.Quit):
                Debug.Log("끝내기");
                Application.Quit();
                break;
            case (BTNType.Option):
                Debug.Log("설정에 들어갑니다.");
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                break;
            case (BTNType.Back):
                Debug.Log("뒤로가기");
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                break;
            case (BTNType.Help):
                Debug.Log("도움을 받습니다.");
                break;
            case (BTNType.Explain):
                CanvasGroupOff(mainGroup);
                CanvasGroupOn(helpGroup);
                break;
            case (BTNType.helpX):
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(helpGroup);
                break;
            case (BTNType.gameselectX):
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(gameGroup);
                break;
            case (BTNType.returnnn):
                SceneManager.LoadScene("Start");
                break;

        }
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void Hobak3()
    {
        SceneManager.LoadScene("Hobak3");
    }

    public void Hobak4()
    {
        SceneManager.LoadScene("Hobak4");
    }

    public void Only()
    {
        SceneManager.LoadScene("Only");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 0.9f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }

}
