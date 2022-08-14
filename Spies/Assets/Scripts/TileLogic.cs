using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TileLogic : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    Image hover;
    public GameObject trapCard;
    public int damage;
    public int rounds;
    int numOfPlayers = 0;
    public bool hasTotem = false;
    public GameManager gameManager;
    List<int> players = new List<int>();

    public bool combatInitiated = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
/*         if (gameManager.trapCardSelected == true)
        {
            var tempColor = hover.color;
            tempColor.a = 1f;
            hover.color = tempColor;
        } */
    }


    public void OnPointerExit(PointerEventData eventData)
    {
/*         var tempColor = hover.color;
        tempColor.a = 0.0f;
        hover.color = tempColor;
 */
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        /*  if (gameManager.trapCardSelected == true && gameManager.selectedCard != null)
        {
            Transform tile = this.transform;
            Instantiate(trapCard, tile);
            damage = gameManager.selectedCard.damage;
            rounds = gameManager.selectedCard.rounds;
        }
                */
    }

    void Start()
    {
        hover = this.GetComponent<Image>();
        var tempColor = hover.color;
        tempColor.a = 0.0f;
        hover.color = tempColor;
        hover.enabled = true;
        numOfPlayers = 0;
    }

    void Update()
    {
        numOfPlayers = getNumOfPlayers();

        if (numOfPlayers == 2)
        {

            if (!combatInitiated)
            {
                FindObjectOfType<GameManager>().EnterCombatSetup(players[0], players[1]);
                combatInitiated = true;
            }
        }
    }

    public int getNumOfPlayers()
    {
        int num = 0;
        GameObject tile = this.gameObject;
        foreach (Transform transform in tile.transform)
        {
            if (transform.CompareTag("Player"))
            {
                num++;
                int playernum = int.Parse(transform.name.Replace("PlayerCharacter", ""));
                players.Add(playernum);
            }
        }

        return num;
    }
}
