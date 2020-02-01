using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAngleController : MonoBehaviour
{
    public GameObject parentRoom;

    void Update()
    {
        parentRoom.transform.rotation = transform.rotation;    
    }
}
