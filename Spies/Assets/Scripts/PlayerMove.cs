using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int currentTile = 1;

    // Update is called once per frame
    void Update()
    {
    }

    public void Move(int steps)
    {
        Debug.Log($"{this.transform.name} move {steps}");
        for (int i = 0; i < steps; i++)
        {
            currentTile++;
            if (currentTile > 100)
            {
                currentTile = 100;
                Transform player = this.transform;
                GameObject destination = GameObject.Find($"Tower/100");
                player.SetParent(destination.gameObject.transform);
            }
            /*  else if (currentTile - steps < 1) { // when they're on the way back down
            Transform player = this.transform;
            GameObject destination = GameObject.Find($"Tower/100");
            player.parent = destination.gameObject.transform;
        } */
            else
            {
                Transform player = this.transform;
                GameObject destination = GameObject.Find($"Tower/{currentTile}");
                foreach (Transform transform in destination.transform)
                {
                    if (transform.CompareTag("Trap"))
                    {
                        //deal some damage or sth
                        Debug.Log($"You hit a trap at {currentTile}");
                    }
                }
                player.SetParent(destination.gameObject.transform);
            }
        }
    }
}
