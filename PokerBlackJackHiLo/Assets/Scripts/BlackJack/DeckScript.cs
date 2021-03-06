using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public Sprite[] cardSprites;
    int[] cardValues = new int[53];
    int currentIndex = 0;
    public bool isPoker;

    void Start()
    {
        GetCardValues();
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
    public int DealCard(CardScript cardScript)
    {

        cardScript.SetSprite(cardSprites[currentIndex]);
        cardScript.SetValue(cardValues[currentIndex]);
        currentIndex++;
        return cardScript.GetValueOfCard();
    }

    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }
}
