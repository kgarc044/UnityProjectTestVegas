using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckScript : MonoBehaviour
{
    public Sprite[] cardSprites;
    int[] cardValues = new int[53];
    string[] suitType = new string[53];
    int currentIndex = 0;
    public bool isPoker;

    void Start()
    {
        GetCardValues();
        GetSuitTypes();
    }

    // Update is called once per frame
    void GetCardValues()
    {
        int num = 0;
        if (isPoker)
        {
            for (int i = 0; i < cardSprites.Length; i++)
            {
                num = i;
                num %= 13;
                if(num == 0)
                {
                    num = 13;
                }
                cardValues[i] = num++;
            }
        }
        else
        {
            for (int i = 0; i < cardSprites.Length; i++)
            {
                num = i;
                num %= 13;
                if (num > 10 || num == 0)
                {
                    num = 10;
                }
                cardValues[i] = num++;
            }
        }
        
    }

    void GetSuitTypes()
    {
        for (int i = 0; i < cardSprites.Length; i++)
        {

            if (cardSprites[i].name.Contains("Clubs"))
            {
                suitType[i] = "Clubs";
            }
            else if (cardSprites[i].name.Contains("Hearts"))
            {
                suitType[i] = "Hearts";
            }
            else if (cardSprites[i].name.Contains("Spades"))
            {
                suitType[i] = "Spades";
            }
            else if (cardSprites[i].name.Contains("Diamonds"))
            {
                suitType[i] = "Diamonds";
            }
        }
    }

    public void Shuffle()
    {
        System.Random random = new System.Random();

        for (int i = cardSprites.Length - 1; i > 0; --i)
        {
            int j = random.Next(i, cardSprites.Length);
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[j];
            cardSprites[j] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[j];
            cardValues[j] = value;

            string suit = suitType[i];
            suitType[i] = suitType[j];
            suitType[j] = suit;
        }
        currentIndex = 1;
        
    }
    public int DealCard(CardScript cardScript)
    {

        cardScript.SetSprite(cardSprites[currentIndex]);
        cardScript.SetValue(cardValues[currentIndex]);
        cardScript.SetSuit(suitType[currentIndex]);
        currentIndex++;
        
        return cardScript.GetValueOfCard();
    }

    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }
}
