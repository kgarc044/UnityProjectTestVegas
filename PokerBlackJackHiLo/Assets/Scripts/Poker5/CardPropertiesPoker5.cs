using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPropertiesPoker5 : MonoBehaviour
{
    public int value = 0;
    public string suit = "";

    public int GetValueOfCard()
    {
        return value;
    }

    public void SetValue(int newValue)
    {
        value = newValue;
    }

    public string GetSuitOfCard()
    {
        return suit;
    }

    public void SetSuit(string newSuit)
    {
        suit = newSuit;
    }

    public void SetSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }


}
