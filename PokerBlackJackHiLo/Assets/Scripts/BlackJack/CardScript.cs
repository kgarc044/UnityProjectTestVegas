using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
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

    public string GetTypeOfSuit()
    {
        return suit;
    }

    public void SetSuit(string suitName)
    {
        suit = suitName;
    }

    public void SetSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    public string GetSpriteName()
    {
        return GetComponent<SpriteRenderer>().sprite.name;
    }

    public void ResetCard()
    {
        Sprite back = GameObject.Find("Deck").GetComponent<DeckScript>().GetCardBack();
        gameObject.GetComponent<SpriteRenderer>().sprite = back;
        value = 0;
    }
}
