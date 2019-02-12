using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class BillBoardController : MonoBehaviour
    {

        // move the billboard 
        public void MoveBillboard()
        {
            // calculating a postion that is 1 unity up 
            Vector3 newPosition = transform.position;
            newPosition.y += 1;

            // moving the billboard to that new position 
            transform.position = newPosition;
        }
    }

}

