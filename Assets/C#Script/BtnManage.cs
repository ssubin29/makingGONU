using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnManage : MonoBehaviour
{
    public BtnNumber ThisBtn;
    public RectTransform WhichTo;

    public Vector3 APos;

    public MoveStone moveSInstance;
    public PosManage btnMInstance;

    public void MovePos()
    {
        if (moveSInstance.Aselected == false && moveSInstance.Bselected == false)
        {
            moveSInstance.Aselected = true;
        }
    }

}
