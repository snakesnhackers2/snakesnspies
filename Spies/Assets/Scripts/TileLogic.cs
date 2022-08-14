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
    public int defense;
    public int hpBoost;
    int numOfPlayers = 0;
    public bool hasTotem = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        var tempColor = hover.color;
        tempColor.a = 1f;
        hover.color = tempColor;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        var tempColor = hover.color;
        tempColor.a = 0.0f;
        hover.color = tempColor;

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Transform tile = this.transform;
        Instantiate(trapCard, tile);
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
            //AMELIA START COMBAT
            Debug.Log("fight");
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
            }
        }

        return num;
    }
}
