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
        if (Input.GetKeyDown(KeyCode.W))
        {
            System.Random random = new System.Random();
            int steps = random.Next(1, 6);
            Move(steps);
        }
    }

    public void Move(int steps)
    {
        Debug.Log($"{this.transform.name} move {steps}");
        if (currentTile + steps > 100)
        {
            currentTile = 100;
            Transform player = this.transform;
            GameObject destination = GameObject.Find($"Tower/100");
            player.SetParent(destination.gameObject.transform);
        }/*  else if (currentTile - steps < 1) { // when they're on the way back down
            Transform player = this.transform;
            GameObject destination = GameObject.Find($"Tower/100");
            player.parent = destination.gameObject.transform;
        } */
        else {
            currentTile += steps;
            Transform player = this.transform;
            GameObject destination = GameObject.Find($"Tower/{currentTile}");
            player.SetParent(destination.gameObject.transform);
        }
    }
}
