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
    public GameObject[] playerAvatars;
    
    public TextMesh diceRollText;

    public int diceRollNum = 1;

    public Text mainPlayerTurnText;

    private bool speedBoostActive = false;
    private bool mainEndTurn = false;


    [Header("Combat Overlay")]
    public int combatPlayerTurn = 0;

    public GameObject combatOverlay;

    public PlayerInventory combatInventoryLeft;
    public PlayerInventory combatInventoryRight;

    public Text baseDamageLeft;
    public Text baseDamageRight;
    public Text TotalDamageLeft;
    public Text TotalDamageRight;
    public Text playerTurnText;

    public StatusBarScript statusBarLeft;
    public StatusBarScript statusBarRight;


    // Stuff for calculating post combat outcome
    private int[] playerOrder;

    private float[] damageProtect;
    private int[] damageDealt;

    private bool diceRolled = false;
    private bool combatCardSelected = false;
    private bool endTurn = false;

    private bool goodLuckActive = false;


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

    ////// MAIN GAME HANDLING + GAMESTATE UPDATES

    public Card DrawCard()
    {
        Card randCard = deck[Random.Range(0, deck.Count)];
        deck.Remove(randCard);
        return randCard;
    }

    // TODO 
    public void UpdateMainTurn()
    {
        // hide previous player card 
        foreach (Card inventoryCard in players[mainPlayerTurn].inventoryDeck)
        {
            inventoryCard.gameObject.SetActive(false);
        }

        mainPlayerTurn++;
        if (mainPlayerTurn > 4)
        {
            mainPlayerTurn = 1;
        }

        // update player text
        mainPlayerTurnText.text = "Player " + mainPlayerTurn + "'s turn";

        // show next player 
        foreach (Card inventoryCard in players[mainPlayerTurn].inventoryDeck)
        {
            inventoryCard.gameObject.SetActive(true);
        }

        // reset any mainmap variables for the turn
        speedBoostActive = false;

        // the health bar visibility toggle (KIV)
    }

    // TODO function for when a player passes a card station 
    public void PickNewCard()
    {

    }

    /////// COMBAT HANDLING

    public void EnterCombatSetup(int playernumleft, int playernumright)
    {
        gamestate = "combat";

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
        mainMap.SetActive(false);

        // combatoverlay setactive true
        combatOverlay.SetActive(true);

        foreach (Card inventoryCard in players[playerOrder[0] - 1].inventoryDeck)
        {
            // cards set active
            inventoryCard.gameObject.SetActive(true);

            // move into place and scale
            inventoryCard.transform.position = combatInventoryLeft.cardSlots[inventoryCard.posInHand].position;
            inventoryCard.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        }

        foreach (Card inventoryCard in players[playerOrder[1] - 1].inventoryDeck)
        {
            // cards set active
            inventoryCard.gameObject.SetActive(true);

            // move into place and scale
            inventoryCard.transform.position = combatInventoryLeft.cardSlots[inventoryCard.posInHand].position;
            inventoryCard.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        }

        // update player health
        statusBarLeft.SetHealth(players[playerOrder[0]].health);
        statusBarLeft.SetPlayerNum(playerOrder[0]);

        // update opponents health
        statusBarRight.SetHealth(players[playerOrder[1]].health);
        statusBarRight.SetPlayerNum(playerOrder[1]);

        // set base damage
        baseDamageLeft.text = "Click Dice for base damage";
        baseDamageRight.text = "Click Dice for base damage";

        // update player turn text
        playerTurnText.text = "Player " + playerOrder[0] + "'s Turn";


        // TODO
        // Clear combat setup variables
        combatCardSelected = false;
        diceRolled = false;
        endTurn = false;

        damageProtect = new float[] { 0f, 0f };
        damageDealt = new int[] { 0, 0 };
        goodLuckActive = false;

        combatPlayerTurn = 0;

        TotalDamageLeft.text = "Total Attack:\n" + damageDealt[0].ToString();
        TotalDamageRight.text = "Total Attack:\n" + damageDealt[1].ToString();
    }

    // TODO to update combat turn status and combat selection over etc
    public void CombatStatusUpdate()
    {
        // check if the dice has been rolled


    }

    // TODO
    public void ProcessCombatResult()
    {
        // check if anyone died, if totem is passed etc

    }

    // TODO 
    public void ExitCombat()
    {
        gamestate = "mainmap";

        // hide non main turn player's cards individually with setactive false
        foreach(int playernum in playerOrder)
        {
            foreach (Card inventoryCard in players[playernum - 1].inventoryDeck)
            {
                // move into place and scale
                inventoryCard.transform.position = playerInventories[0].cardSlots[inventoryCard.posInHand].position;
                inventoryCard.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                // cards set active
                if (playernum != mainPlayerTurn)
                {
                    inventoryCard.gameObject.SetActive(false);

                }
            }
        }

        // mainmap setactive true
        mainMap.SetActive(true);

        // combatoverlay setactive false
        combatOverlay.SetActive(false);

        // call map manager function to continue map traversal
        // TODO [FRANCENE HERE]
    }


    /////// CARD HANDLING

    public void CardSelection(Card cardSelected)
    {

        Debug.Log(cardSelected.cardName);

        if(cardSelected.inBigCardPos)
        {
            if (!mainEndTurn && !combatCardSelected)
            {
                // process the card being used
                CardUsed(cardSelected.cardName);

                // add card back into main deck
                deck.Add(cardSelected);

                if (gamestate == "mainmap")
                {
                    // remove the card from inventory
                    players[mainPlayerTurn - 1].availableCardSlots[cardSelected.posInHand] = false;

                    players[mainPlayerTurn - 1].inventoryDeck.Remove(cardSelected);

                    // reorganize inventory (ie move remaining cards to center with for loop
                    int i = 0;
                    foreach (Card inventoryCard in players[mainPlayerTurn - 1].inventoryDeck)
                    {
                        // move into place and scale
                        inventoryCard.transform.position = playerInventories[0].cardSlots[i].position;
                        inventoryCard.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        i++;
                    }
                }
                else
                {
                    TotalDamageLeft.text = "Total Attack:\n" + damageDealt[0].ToString();
                    TotalDamageRight.text = "Total Attack:\n" + damageDealt[1].ToString();

                    // remove the card from inventory
                    players[playerOrder[combatPlayerTurn] - 1].availableCardSlots[cardSelected.posInHand] = false;

                    players[playerOrder[combatPlayerTurn] - 1].inventoryDeck.Remove(cardSelected);

                    // reorganize inventory (ie move remaining cards to center with for loop
                    int i = 0;
                    foreach (Card inventoryCard in players[playerOrder[combatPlayerTurn] - 1].inventoryDeck)
                    {
                        // move into place and scale
                        inventoryCard.transform.position = playerInventories[0].cardSlots[i].position;
                        inventoryCard.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        inventoryCard.posInHand = i;
                        i++;
                    }
                    players[playerOrder[combatPlayerTurn] - 1].inventoryDeck.Add(cardSelected);
                }

            }

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
                if (combatPlayerTurn == 0)
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
                    // right
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

        if (gamestate == "mainmap")
        {
            // TODO
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
                    speedBoostActive = true;
                    break;

                case "AppleCard":
                    invalidCard = false;
                    players[mainPlayerTurn - 1].health += 10;
                    if (players[mainPlayerTurn - 1].health > 100)
                    {
                        players[mainPlayerTurn - 1].health = 100;

                    }
                    break;

                case "HealthPotionCard":
                    invalidCard = false;
                    break; players[mainPlayerTurn - 1].health += 30;
                    if (players[mainPlayerTurn - 1].health > 100)
                    {
                        players[mainPlayerTurn - 1].health = 100;

                    }

                case "HeartyCakeCard":
                    invalidCard = false;
                    break; players[mainPlayerTurn - 1].health += 50;
                    if (players[mainPlayerTurn - 1].health > 100)
                    {
                        players[mainPlayerTurn - 1].health = 100;

                    }

                default:
                    break;
            }
        } else
        {
            if (!combatCardSelected)
            {
                // check for defense cards
                switch (cardName)
                {
                    case "ArmourCard":
                        damageProtect[combatPlayerTurn] = 0.8f;
                        combatCardSelected = true;
                        invalidCard = false;
                        break;

                    case "ShieldCard":
                        damageProtect[combatPlayerTurn] = 0.6f;
                        combatCardSelected = true;
                        invalidCard = false;
                        break;

                    case "DodgeCard":
                        damageProtect[combatPlayerTurn] = 0.6f;
                        combatCardSelected = true;
                        invalidCard = false;
                        break;

                    case "GoodLuckCard":
                        goodLuckActive = true;
                        combatCardSelected = true;
                        invalidCard = false;
                        break;

                    default:
                        break;
                }



                // check for attack cards
                switch (cardName)
                {
                    case "RoundhouseKickCard":
                        damageDealt[combatPlayerTurn] += 35;
                        combatCardSelected = true;
                        invalidCard = false;
                        break;

                    case "DaggerSlashCard":
                        damageDealt[combatPlayerTurn] += 45;
                        combatCardSelected = true;
                        invalidCard = false;
                        break;

                    case "SlapCard":
                        damageDealt[combatPlayerTurn] += 10;
                        combatCardSelected = true;
                        invalidCard = false;
                        break;

                    case "SuckerPunchCard":
                        damageDealt[combatPlayerTurn] += 25;
                        combatCardSelected = true;
                        invalidCard = false;
                        break;

                    default:
                        break;
                }
            }
            
        }
        

        if (invalidCard)
        {
            Debug.Log("This card cannot be used here!");

            // play some sound effect to indicate it is invalid & 
        } else
        {
            // TODO (KIV)
            // play some sound effect to indicate it is been selected successfully
            // or add like sparkles or something in unity
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
            if (!diceRolled)
            {
                CombatDiceRoll(diceLocation);
            }
        }
    }

    public void MovementDiceRoll()
    {
        mainEndTurn = true;

        // generate random number form 1 to 6
        diceRollNum = Random.Range(1, 7);

        diceRollText.text = diceRollNum.ToString();

        if (speedBoostActive)
        {
            speedBoostActive = false;
            diceRollNum *= 2;
        }

        // call character movement function for player <playerturn>
        GameObject playerObject = playerAvatars[mainPlayerTurn - 1].gameObject;
        PlayerMove player = playerObject.GetComponent<PlayerMove>();
        player.Move(diceRollNum);

        UpdateMainTurn();
    }

    public void CombatDiceRoll(string dicelocation)
    {
        // for max base damage of 10
        diceRollNum = Random.Range(1, 11);

        if (!diceRolled)
        {
            if (dicelocation == "diceleft")
            {
                damageDealt[0] += diceRollNum;
                baseDamageLeft.text = "+ " + diceRollNum + " Attack";
                TotalDamageLeft.text = "Total Attack:\n" + damageDealt[0].ToString();

            } else
            {
                damageDealt[1] += diceRollNum;
                baseDamageRight.text = "+ " + diceRollNum + " Attack";
                TotalDamageRight.text = "Total Attack:\n" + damageDealt[1].ToString();

            }
        }
        
        
        diceRolled = true;
    }



}
