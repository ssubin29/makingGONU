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
    public Text onoffText;
    new public AudioSource audio;//

    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }

    bool isSound;


    public void OnBtnClick()
    {
        switch (currentType)
        {
            case (BTNType.GameStart):
                Debug.Log("새 게임");
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
            case (BTNType.Sound):
                Debug.Log("사운드를 조작합니다.");
                if (isSound)
                {
                    isSound = !isSound;
                    Debug.Log("소리끄기");
                    audio.volume = 1f;
                    onoffText.text = "소리끄기";
                }
                else
                {
                    isSound = !isSound;
                    Debug.Log("소리켜기");
                    audio.volume = 0;
                    onoffText.text = "소리켜기";
                }
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


    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 0.9f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }

}
