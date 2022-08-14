using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollClick : MonoBehaviour
{
    public string diceLocation;

    private void OnMouseDown()
    {
        FindObjectOfType<GameManager>().DiceRollSelect(diceLocation);
    }
}
