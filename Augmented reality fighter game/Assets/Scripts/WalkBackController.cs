using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class WalkBackController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public void OnPointerDown(PointerEventData data)
    {
        FighterController.mvBack = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        FighterController.mvBack = false;
    }

}
