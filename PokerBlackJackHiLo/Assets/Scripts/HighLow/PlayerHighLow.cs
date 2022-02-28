using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHighLow : MonoBehaviour
{
    public GameObject[] hand;
    public CardPropertiesPoker5 cardScript;
    public DeckHighLow deckScript;
    int cardIndex = 0;

    public bool cardIsLower = false;

    public void StartingCard()
    {
        for (cardIndex = 0; cardIndex < hand.Length; cardIndex++)
        {
            deckScript.SetHand(hand[cardIndex].GetComponent<CardPropertiesPoker5>());
        }
    }

    public bool EvaluateGuess(string userGuess)
    {
        cardIsLower = IsInitialCardLower();
        return cardIsLower;
    }

    public bool IsInitialCardLower()
    {
        if (hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() < hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard()) {
            return true;
        }
        else
        {
            return false;
        }
    }
}
