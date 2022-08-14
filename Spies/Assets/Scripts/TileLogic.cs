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

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("mouse enter" + gameObject.name);
        var tempColor = hover.color;
        tempColor.a = 1f;
        hover.color = tempColor;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("mouse exit" + gameObject.name);
        var tempColor = hover.color;
        tempColor.a = 0.0f;
        hover.color = tempColor;

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("mouse click" + gameObject.name);

        Transform tile = this.transform;
        Instantiate(trapCard, tile);
        //trapCard.transform.parent = tile;
    }

    void Start()
    {
        hover = this.GetComponent<Image>();
        var tempColor = hover.color;
        tempColor.a = 0.0f;
        hover.color = tempColor;
        hover.enabled = true;
    }
}
