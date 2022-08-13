using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroll : MonoBehaviour
{
    public Transform cam;
    public float relativeMove = .3f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y -= Input.mouseScrollDelta.y * relativeMove;
        transform.position = pos;
        //transform.position = new Vector2(cam.position.x , cam.position.y * relativeMove);   
    }
}
