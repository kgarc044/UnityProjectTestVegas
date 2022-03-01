using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHighLow : MonoBehaviour
{
    public Sprite[] cardSprites;
    int[] cardValues = new int[52];
    string[] cardSuit = new string[52];
    int currentIndex = 0;

    private void Start()
    {
        GetCardValues();
        GetSuitTypes();
    }

    void GetCardValues()
    {
        int num = 0;
        for (int i = 0; i < cardSprites.Length; i++)
        {
            num = i;
            num %= 13;
            if (num == 0 && (i == 0 || i == 13 || i == 26 || i == 39))
            {
                num = 0;
            }
            else if (num == 0)
            {
                num = 13;
            }
            num = num + 1;
            cardValues[i] = num;
        }
    }

    public void Shuffle()
    {
        System.Random random = new System.Random();

        for (int i = cardSprites.Length - 1; i >= 0; --i)
        {
            int j = random.Next(i, cardSprites.Length);
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[j];
            cardSprites[j] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[j];
            cardValues[j] = value;

            string suit = cardSuit[i];
            cardSuit[i] = cardSuit[j];
            cardSuit[j] = suit;
        }

        currentIndex = 0;

    }

    void GetSuitTypes()
    {
        for (int i = 0; i < cardSprites.Length; i++)
        {

            if (cardSprites[i].name.Contains("Clubs"))
            {
                cardSuit[i] = "Clubs";
            }
            else if (cardSprites[i].name.Contains("Hearts"))
            {
                cardSuit[i] = "Hearts";
            }
            else if (cardSprites[i].name.Contains("Spades"))
            {
                cardSuit[i] = "Spades";
            }
            else if (cardSprites[i].name.Contains("Diamonds"))
            {
                cardSuit[i] = "Diamonds";
            }
        }
    }

    public void SetHand(CardPropertiesPoker5 cardScript)
    {
        cardScript.SetSprite(cardSprites[currentIndex]);
        cardScript.SetValue(cardValues[currentIndex]);
        cardScript.SetSuit(cardSuit[currentIndex]);
        currentIndex++;
    }

}
