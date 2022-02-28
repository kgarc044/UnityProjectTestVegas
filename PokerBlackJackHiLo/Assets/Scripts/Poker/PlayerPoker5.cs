using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoker5 : MonoBehaviour
{
    public GameObject[] hand;
    public CardPropertiesPoker5 cardScript;
    public DeckPoker5 deckScript;
    int cardIndex = 0;

    private int heartsSum = 0;
    private int diamondSum = 0;
    private int clubSum = 0;
    private int spadesSum = 0;
    private int money = 1000;

    public int highCard;
    public int secondHighCard;

    public enum PokerHand
    {
        HighCard,
        OnePair,
        TwoPairs,
        ThreeKind,
        Straight,
        Flush,
        FullHouse,
        FourKind
    }
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

    public GameObject[] GetHand()
    {
        return hand;
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
                    int temp = hand[i].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
                    hand[i].GetComponent<CardPropertiesPoker5>().SetValue(hand[j].GetComponent<CardPropertiesPoker5>().GetValueOfCard());
                    hand[j].GetComponent<CardPropertiesPoker5>().SetValue(temp);

                    string tempString = hand[i].GetComponent<CardPropertiesPoker5>().GetSuitOfCard();
                    hand[i].GetComponent<CardPropertiesPoker5>().SetSuit(hand[j].GetComponent<CardPropertiesPoker5>().GetSuitOfCard());
                    hand[j].GetComponent<CardPropertiesPoker5>().SetSuit(tempString);

                    Sprite tempSprite = hand[i].GetComponent<SpriteRenderer>().sprite;
                    hand[i].GetComponent<CardPropertiesPoker5>().SetSprite(hand[j].GetComponent<SpriteRenderer>().sprite);
                    hand[j].GetComponent<CardPropertiesPoker5>().SetSprite(tempSprite);

                }
            }
        }
    }


    public PokerHand EvaluateHand()
    {
        heartsSum = 0;
        diamondSum = 0;
        clubSum = 0;
        spadesSum = 0;

        GetNumberOfSuit();
        if(FourOfKind())
        {
            return PokerHand.FourKind;
        }
        else if(FullHouse())
        {
            return PokerHand.FullHouse;
        }
        else if (Flush())
        {
            return PokerHand.Flush;
        }
        else if (Straight())
        {
            return PokerHand.Straight;
        }
        else if (ThreeOfAKind())
        {
            return PokerHand.ThreeKind;
        }
        else if (TwoPair())
        {
            return PokerHand.TwoPairs;
        }
        else if (OnePair())
        {
            return PokerHand.OnePair;
        }

        highCard = hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
        return PokerHand.HighCard;
    }

    public void GetNumberOfSuit()
    {
        for(int i = 0; i < hand.Length; i++)
        {
            if(hand[i].GetComponent<CardPropertiesPoker5>().GetSuitOfCard() == "Clubs")
            {
                clubSum++;
            }
            else if(hand[i].GetComponent<CardPropertiesPoker5>().GetSuitOfCard() == "Diamonds")
            {
                diamondSum++;
            }
            else if (hand[i].GetComponent<CardPropertiesPoker5>().GetSuitOfCard() == "Hearts")
            {
                heartsSum++;
            }
            else if (hand[i].GetComponent<CardPropertiesPoker5>().GetSuitOfCard() == "Spades")
            {
                spadesSum++;
            }
        }
    }
    
    private bool FourOfKind()
    {
        //Check if FIRST 4 cards are a FOUR OF A KIND
        if(hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }

        //Check if LAST 4 cards are a FOUR OF A KIND
        else if (hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }

        return false;
    }

    private bool FullHouse()
    {
        //if FIRST 3 cards are a THREE OF A KIND and the LAST 2 cards is a PAIR
        if (hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            secondHighCard = hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        //if FIRST 2 cards are a PAIR and the LAST 3 are a THREE OF A KIND
        else if (hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            secondHighCard = hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        return false;
    }

    private bool Flush()
    {
        if(heartsSum == 5 || diamondSum == 5 ||clubSum == 5 || spadesSum == 5)
        {
            highCard = hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
                return true;
        }
        return false;
    }

    private bool Straight()
    {
        if(hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() + 1 == hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() + 1 == hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() + 1 == hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() + 1 == hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }

        return false;
    }

    private bool ThreeOfAKind()
    {
        if (hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        else if (hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        else if (hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        return false;
    }

    private bool TwoPair()
    {
        if (hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            secondHighCard = hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }

        else if (hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            secondHighCard = hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }

        else if (hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            secondHighCard = hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        return false;
    }

    private bool OnePair()
    {
        //if 1,2
        //if 2,3
        //if 3,4
        //if 4,5
        if (hand[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        else if (hand[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        else if (hand[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        else if (hand[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = hand[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        return false;
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
