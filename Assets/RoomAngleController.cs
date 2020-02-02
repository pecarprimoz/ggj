using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAngleController : MonoBehaviour
{
    public GameObject parentRoom;
    //public Rigidbody cubeBody;

    void Update()
    {
        GetComponent<Rigidbody>().AddExplosionForce(0.2f, transform.localPosition, 0.2f);
        // cubeBody.AddForceAtPosition(Vector3.forward, transform.localPosition, ForceMode.Force);
        //parentRoom.transform.rotation = Quaternion.Euler(Vector3.Lerp(parentRoom.transform.rotation.eulerAngles, transform.rotation.eulerAngles, Time.deltaTime));
    }
}
