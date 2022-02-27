using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoker5 : MonoBehaviour
{
    public GameObject[] hand;
    public CardPropertiesPoker5 cardScript;
    public DeckPoker5 deckScript;
    int cardIndex = 0;

    public void StartingHands()
    {
        GetCards();
    }

    public void GetCards()
    {
        for(cardIndex = 0; cardIndex < hand.Length; cardIndex++)
        {
            deckScript.SetHand(hand[cardIndex].GetComponent<CardPropertiesPoker5>());
        }
    }

    public void RedrawHand(List<int> list) 
    {
        foreach (var v in list)
        {
            deckScript.SetHand(hand[v].GetComponent<CardPropertiesPoker5>());
        }
        
    }

    public void SortHands()
    {
        int min = 0;
        for (int i = 0; i < hand.Length; i++)
        {
            for (int j = i+1; j < hand.Length; j++)
            {
                if(hand[i].GetComponent<CardPropertiesPoker5>().GetValueOfCard() > hand[j].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
                {
                    min = j;
                }
                if(min != i)
                {
                    GameObject temp = hand[i];
                    hand[i] = hand[j];
                    hand[j] = temp;
                }
            }
        }
    }
}
