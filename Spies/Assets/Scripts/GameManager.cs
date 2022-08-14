using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    [Header("Card management")]
    public List<Card> deck = new List<Card>();

    [Header("Main Map")]
    public int playerTurn = 1;

    public GameObject mainMap;

    public PlayerStats player1;
    public PlayerStats player2;
    public PlayerStats player3;
    public PlayerStats player4;

    public TextMesh diceRollText;

    [Header("Combat Overlay")]
    public int combatPlayerTurn = 1;

    public GameObject combatOverlay;

    public Text baseDamageLeft;
    public Text baseDamageRight;

    public StatusBarScript statusBarLeft;
    public StatusBarScript statusBarRight;






}
