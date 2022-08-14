using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string cardName;
    public int posInHand = 0;
    public bool inBigCardPos = false;

    private void OnMouseDown()
    {
        FindObjectOfType<GameManager>().CardSelection(this);
    }

}
