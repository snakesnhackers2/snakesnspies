using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int currentTile = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Move(int steps) {
        if (currentTile + steps > 100) {
            Transform player = this.transform;
            GameObject destination = GameObject.Find($"Tower/100");
            player.parent = destination.gameObject.transform;
        }/*  else if (currentTile - steps < 1) {
            Transform player = this.transform;
            GameObject destination = GameObject.Find($"Tower/100");
            player.parent = destination.gameObject.transform;
        } */

    }
}
