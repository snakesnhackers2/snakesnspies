using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string cardName;
    public int posInHand = 0;
    public bool inBigCardPos = false;    
    public string type;
    public int damage;
    public float defense; //percentage
    public int hpBoost;
    public int rounds; //number of rounds before getting to play again

    private void OnMouseDown()
    {
        FindObjectOfType<GameManager>().CardSelection(this);
        inBigCardPos = true;
    }

    private void OnMouseUp() {
        inBigCardPos = false;
    }
}
