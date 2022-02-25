using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckPoker5 : MonoBehaviour
{
    public Sprite[] cardSprites;
    int[] cardValues = new int[52];
    string[] cardSuit = new string[52];
    int currentIndex = 0;

    void GetCardValues()
    {
        int num = 0;
        for (int i = 0; i < cardSprites.Length; i++)
        {
            num = i;
            num %= 13;
            if (num == 0)
            {
                num = 13;
            }
            cardValues[i] = num++;
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
        }

        currentIndex = 1;

    }
    public void SetDeck(CardPropertiesPoker5 cardScript)
    {
        cardScript.SetSprite(cardSprites[currentIndex]);
        cardScript.SetValue(cardValues[currentIndex]);
        cardScript.SetSuit(cardSuit[currentIndex]);
        currentIndex++;
    }

}
