using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int health = 100;
    public StatusBarScript playerhealth;
    public bool[] availableCardSlots = new bool[] { true, true, true, true, true };

    public List<Card> inventoryDeck = new List<Card>();

    private void Start()
    {
        availableCardSlots = new bool[] { true, true, true, true, true };
    }

    public void UpdateHealth(int newHealth)
    {
        // any modifiers on the health (eg cannot lower than 0)


        // update the number
        health = newHealth;

        // update main health bar
        playerhealth.SetHealth(newHealth);

        // the combat health bar will read the health at the end of computing a turn

        
    }





}
