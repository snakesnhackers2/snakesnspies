using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{

    [Header("GameState")]
    public string gamestate = "mainmap";

    [Header("Card management")]
    public List<Card> deck = new List<Card>();

    [Header("Main Map")]
    public int mainPlayerTurn = 1;

    public GameObject mainMap;

    public PlayerStats[] players;

    public PlayerInventory[] playerInventories;

    public GameObject[] playerObjects; 
    
    public TextMesh diceRollText;

    public int diceRollNum = 1;

    [Header("Combat Overlay")]
    public int combatPlayerTurn = 1;

    public GameObject combatOverlay;

    public PlayerInventory combatInventoryLeft;
    public PlayerInventory combatInventoryRight;

    public Text baseDamageLeft;
    public Text baseDamageRight;
    public Text playerTurnText;

    public StatusBarScript statusBarLeft;
    public StatusBarScript statusBarRight;

    private int[] playerOrder;


    private void Start()
    {
        
        // init draw 3 cards for all 4 players
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                Card newCard = DrawCard();
                players[j].inventoryDeck.Add(newCard);
                newCard.transform.position = playerInventories[j].cardSlots[i].position;
                newCard.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                players[j].availableCardSlots[i] = false;
                newCard.posInHand = i;
            }
        }

        // hide the non turn players cards
        for (int i = 1; i < 4; i++)
        {
            foreach(Card inventoryCard in players[i].inventoryDeck)
            {
                inventoryCard.gameObject.SetActive(false);
            }
        }
    }

    public Card DrawCard()
    {
        Card randCard = deck[Random.Range(0, deck.Count)];
        deck.Remove(randCard);
        return randCard;
    }

    // TODO 
    public void UpdateMainTurn()
    {
        mainPlayerTurn++;
        if (mainPlayerTurn > 4)
        {
            mainPlayerTurn = 1;
        }

        // hide previous player card 

        //
    }

    // TODO
    public void EnterCombatSetup(int playernumleft, int playernumright)
    {
        // randomize player turn between the 
        int randomizer = Random.Range(0, 2);
        playerOrder = new int[2];

        if (randomizer == 0)
        {
            playerOrder[0] = playernumleft;
            playerOrder[1] = playernumright;
        } else
        {
            playerOrder[1] = playernumleft;
            playerOrder[0] = playernumright;
        }

        // mainmap setactive false

        // combatoverlay setactive true

        // opponents cards set active

        // move players cards into place

        // move opponents cards into place

        // update player health

        // update opponents health

        // reset base health text left

        // reset base health text right

        // update player turn text

    }

    // TODO to update combat turn status and combat selection over etc
    public void CombatStatusUpdate()
    {

    }

    // TODO
    public void ProcessCombatResult()
    {
        // check if anyone died, if totem is passed etc
    }

    // TODO 
    public void ExitCombat()
    {
        // hide non main turn player's cards individually with setactive false

        // mainmap setactive true

        // combatoverlay setactive false

        // move players cards back into place

        // call map manager function to continue map traversal
    }


    // TODO
    public void MovementDiceRoll()
    {
        // generate random number form 1 to 6
        diceRollNum = Random.Range(1, 7);

        diceRollText.text = diceRollNum.ToString();

        // call character movement function for player <playerturn>
    }

    // TODO
    public void CardSelection(Card cardSelected)
    {

        Debug.Log(cardSelected.cardName);

        if(cardSelected.inBigCardPos)
        {
            // take as card is used


        } else
        {
            // shifts a card up from player hand to big card view

            if (gamestate == "mainmap")
            {

                // iterate through player inventory and move down any cards currently in big card pos)

                // move selected card to bigcard pos

                // update card inBigCardPos to true
            }
            else
            {
                // check which player it is to update and if it is left or right

                // iterate through player inventory and move down any cards currently in big card pos)

                // move selected card to bigcard pos

                // update card inBigCardPos to true
            }

        }


    }

    public void DiceRollSelect(string diceLocation)
    {

    }


}
