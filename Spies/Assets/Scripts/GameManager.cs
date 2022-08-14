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


    // Stuff for calculating post combat outcome
    private int[] playerOrder;
    private int[] damageProtect;

    private bool goodLuckActive = false;

    private int diceRollLeft;
    private int diceRollRight;

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

        // playerOrder[0] will be on the left and start first always
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
    public void CardSelection(Card cardSelected)
    {

        Debug.Log(cardSelected.cardName);

        if(cardSelected.inBigCardPos)
        {
            // take as card is used

            // process the card being used
            CardUsed(cardSelected.cardName);

            // remove the card from inventory

            // players[j].availableCardSlots[i] = false;
            // reorganize inventory (ie move remaining cards to center with for loop

            // add card back into main deck            


        }
        else
        {
            // shifts a card up from player hand to big card view

            if (gamestate == "mainmap")
            {
                // iterate through player inventory and move down any cards currently in big card pos)
                foreach (Card inventoryCard in players[mainPlayerTurn - 1].inventoryDeck)
                {
                    if (inventoryCard.inBigCardPos)
                    {
                        inventoryCard.inBigCardPos = false;
                        inventoryCard.transform.position = playerInventories[mainPlayerTurn - 1].cardSlots[inventoryCard.posInHand].position;
                        inventoryCard.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                        break;
                    }
                }

                // move selected card to bigcard pos
                cardSelected.transform.position = playerInventories[mainPlayerTurn - 1].bigCardSlot.position;
                cardSelected.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                // update card inBigCardPos to true
                cardSelected.inBigCardPos = true;
            }
            else
            {
                // check which player it is to update and if it is left or right
                if (System.Array.IndexOf(playerOrder, combatPlayerTurn) == 0)
                {
                    // left
                    // iterate through player inventory and move down any cards currently in big card pos)
                    foreach (Card inventoryCard in players[mainPlayerTurn - 1].inventoryDeck)
                    {
                        if (inventoryCard.inBigCardPos)
                        {
                            inventoryCard.inBigCardPos = false;
                            inventoryCard.transform.position = combatInventoryLeft.cardSlots[inventoryCard.posInHand].position;
                            inventoryCard.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                            break;
                        }
                    }

                    // move selected card to bigcard pos
                    cardSelected.transform.position = combatInventoryLeft.bigCardSlot.position;
                    cardSelected.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                    // update card inBigCardPos to true
                    cardSelected.inBigCardPos = true;
                } else
                {
                    // left
                    // iterate through player inventory and move down any cards currently in big card pos)
                    foreach (Card inventoryCard in players[mainPlayerTurn - 1].inventoryDeck)
                    {
                        if (inventoryCard.inBigCardPos)
                        {
                            inventoryCard.inBigCardPos = false;
                            inventoryCard.transform.position = combatInventoryRight.cardSlots[inventoryCard.posInHand].position;
                            inventoryCard.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                            break;
                        }
                    }

                    // move selected card to bigcard pos
                    cardSelected.transform.position = combatInventoryRight.bigCardSlot.position;
                    cardSelected.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                    // update card inBigCardPos to true
                    cardSelected.inBigCardPos = true;
                }

            }

        }
    }

    // TODO

    public void CardUsed(string cardName)
    {
        bool invalidCard = true;


        // check for defense cards
        switch (cardName)
        {
            case "ArmourCard":
                invalidCard = false;
                break;

            case "ShieldCard":
                invalidCard = false;
                break;

            case "DodgeCard":
                invalidCard = false;
                break;

            case "GoodLuckCard":
                invalidCard = false;
                break;

            default:
                break;
        }

        // check for trap cards [FRANCENE HERE] call the trap placement functions
        switch (cardName)
        {
            case "FartBombCard":
                invalidCard = false;
                break;

            case "MouseTrapCard":
                invalidCard = false;
                break;

            case "NetTrapCard":
                invalidCard = false;
                break;

            case "ZapTrapCard":
                invalidCard = false;
                break;

            default:
                break;
        }

        // check for bonus cards
        switch (cardName)
        {

            case "SpeedBoostCard":
                invalidCard = false;
                break;

            case "AppleCard":
                invalidCard = false;
                break;

            case "HealthPotionCard":
                invalidCard = false;
                break;

            case "HeartyCakeCard":
                invalidCard = false;
                break;

            default:
                break;
        }

        // check for attack cards
        switch (cardName)
        {
            case "RoundhouseKickCard":
                invalidCard = false;
                break;

            case "DaggerSlashCard":
                invalidCard = false;
                break;

            case "SlapCard":
                invalidCard = false;
                break;

            case "SuckerPunchCard":
                invalidCard = false;
                break;

            default:
                break;
        }


        if (invalidCard)
        {
            Debug.Log("This card cannot be used here!");

            // play some sound effect
        }
    }


    /////////////// DICE HANDLING
    public void DiceRollSelect(string diceLocation)
    {
        if(diceLocation == "mainmap")
        {
            MovementDiceRoll();
        } else
        {
            CombatDiceRoll(diceLocation);
        }
    }

    // TODO
    public void MovementDiceRoll()
    {
        // generate random number form 1 to 6
        diceRollNum = Random.Range(1, 7);

        diceRollText.text = diceRollNum.ToString();

        // call character movement function for player <playerturn>
        // FRANCENE HERE
    }

    public void CombatDiceRoll(string dicelocation)
    {
        // for max base damage of 10
        diceRollNum = Random.Range(1, 11);

        if (dicelocation == "diceleft")
        {
            diceRollLeft = diceRollNum;
            baseDamageLeft.text = "+ " + diceRollNum + " Damge";

        } else
        {
            diceRollRight = diceRollNum;
            baseDamageRight.text = "+ " + diceRollNum + " Damge";

        }
    }



}
