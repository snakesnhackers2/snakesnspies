using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarScript : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Text healthValue;
    public Text statusBarTitle;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
        healthValue.text = health + " / 100";

    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        healthValue.text = health + " / 100";

    }

    public void SetPlayerNum (int playerNum)
    {
        statusBarTitle.text = "Player " + playerNum + "'s Health";
    }
}
