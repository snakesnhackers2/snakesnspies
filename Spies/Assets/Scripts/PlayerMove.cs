using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int currentTile = 1;
    GameObject destination;

    // Update is called once per frame
    void Update()
    {
    }

    public void Move(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            currentTile++;
            if (currentTile > 100)
            {
                currentTile = 100;
                Transform player = this.transform;
                destination = GameObject.Find($"Tower/100");
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
                destination = GameObject.Find($"Tower/{currentTile}");
                foreach (Transform transform in destination.transform)
                {
                    if (transform.CompareTag("Trap"))
                    {
                        //deal some damage or sth
                        // TODO: ADD TRAP LOGIC
                        Debug.Log($"You hit a trap at {currentTile}");
                    }
                    else if (transform.CompareTag("DrawCard"))
                    {
                        // TODO: LOGIC TO DRAW CARD HERE
                    }


                    TileLogic tile = destination.GetComponent<TileLogic>();
                    if (tile.getNumOfPlayers() == 2)
                    {

                    }
                }
                player.SetParent(destination.gameObject.transform);
            }



            /*  waitTime = 2;
                        //Reset counter
                        float counter = 0f;
                        while (counter < waitTime)
                        {
                            //Increment Timer until counter >= waitTime
                            counter += Time.deltaTime;
                            Debug.Log("We have waited for: " + counter + " seconds");
                            //Check if we want to quit this function
                            //Wait for a frame so that Unity doesn't freeze
                        } */
        }
    }
}
