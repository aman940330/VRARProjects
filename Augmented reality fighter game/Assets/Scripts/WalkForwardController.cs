using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WalkForwardController : MonoBehaviour, IPointerDownHandler,IPointerUpHandler {

    public void OnPointerDown(PointerEventData data)
    {
        FighterController.mvFWD = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        FighterController.mvFWD = false;
    }

}

