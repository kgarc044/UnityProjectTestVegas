using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHighLow : MonoBehaviour
{
    public GameObject[] hand;
    public CardPropertiesPoker5 cardScript;
    public DeckHighLow deckScript;
    int cardIndex = 0;
    private int money = 1000;
    string correctAnswer;

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
        int initial = hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
        int guessCard = hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard();

        if(initial < guessCard)
        {
            correctAnswer = "Higher";
        }
        else
        {
            correctAnswer = "Lower";
        }

        if (correctAnswer == userGuess)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void AdjustMoney(int amount)
    {
        money += amount;
    }

    public int GetMoney()
    {
        return money;
    }
}
