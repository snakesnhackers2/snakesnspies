using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool hasTotem = false;
    public int currentTile = 1;
    GameObject destination;

    // Update is called once per frame
    void Update()
    {
    }

    public void Move(int steps)
    {
        Transform player = this.transform;
        for (int i = 0; i < steps; i++)
        {
            currentTile++;
            if (currentTile > 100)
            {
                currentTile = 100;
                destination = GameObject.Find($"Tower/100");
                player.SetParent(destination.gameObject.transform);

                TileLogic end = destination.GetComponent<TileLogic>();
                if (end.hasTotem)
                {
                    hasTotem = true; // set player as owner
                    end.hasTotem = false; // remove totem from tile
                    Destroy(GameObject.Find("Tower/100/TotemTile"));
                }
            }
            /*  else if (currentTile - steps < 1) { // when they're on the way back down
            Transform player = this.transform;
            GameObject destination = GameObject.Find($"Tower/100");
            player.parent = destination.gameObject.transform;
        } */
            else
            {
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

        // if destination has a vent, take the route to the other end
        Vent vent = destination.GetComponent<Vent>();
        if (vent)
        {
            int otherEnd = vent.otherEnd;
            currentTile = otherEnd;
            GameObject opening = GameObject.Find($"Tower/{otherEnd}");
            player.SetParent(opening.gameObject.transform);
        }
    }
}
