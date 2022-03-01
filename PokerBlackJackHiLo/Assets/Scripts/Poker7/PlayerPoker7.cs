using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPoker7 : MonoBehaviour
{
    public GameObject[] hand;
    public GameObject[] temp;
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
        for (cardIndex = 0; cardIndex < hand.Length; cardIndex++)
        {
            deckScript.SetHand(hand[cardIndex].GetComponent<CardPropertiesPoker5>());
        }
    }

    public void Remove(List<int> list)
    {
        int tempCounter = 0;

        for (int i = 0; i < 7; i++)
        {
            if (i == list[0] || i == list[1])
            {
                continue;
            }
            else
            {
                temp[tempCounter].GetComponent<CardPropertiesPoker5>().SetValue(hand[i].GetComponent<CardPropertiesPoker5>().GetValueOfCard());
                temp[tempCounter].GetComponent<CardPropertiesPoker5>().SetSuit(hand[i].GetComponent<CardPropertiesPoker5>().GetSuitOfCard());
                temp[tempCounter].GetComponent<CardPropertiesPoker5>().SetSprite(hand[i].GetComponent<SpriteRenderer>().sprite);
                tempCounter++;
            }

        }
    }

    public GameObject[] GetHand()
    {
        return temp;
    }

    public void SortHands(GameObject[] hand)
    {
        int min = 0;
        for (int i = 0; i < hand.Length; i++)
        {
            for (int j = i + 1; j < hand.Length; j++)
            {
                if (hand[i].GetComponent<CardPropertiesPoker5>().GetValueOfCard() > hand[j].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
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

    
    

    public PokerHand EvaluateHand(GameObject[] hand)
    {
        heartsSum = 0;
        diamondSum = 0;
        clubSum = 0;
        spadesSum = 0;

        GetNumberOfSuit();
        if (FourOfKind())
        {
            return PokerHand.FourKind;
        }
        else if (FullHouse())
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
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].GetComponent<CardPropertiesPoker5>().GetSuitOfCard() == "Clubs")
            {
                clubSum++;
            }
            else if (temp[i].GetComponent<CardPropertiesPoker5>().GetSuitOfCard() == "Diamonds")
            {
                diamondSum++;
            }
            else if (temp[i].GetComponent<CardPropertiesPoker5>().GetSuitOfCard() == "Hearts")
            {
                heartsSum++;
            }
            else if (temp[i].GetComponent<CardPropertiesPoker5>().GetSuitOfCard() == "Spades")
            {
                spadesSum++;
            }
        }
    }

    private bool FourOfKind()
    {
        //Check if FIRST 4 cards are a FOUR OF A KIND
        if (temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }

        //Check if LAST 4 cards are a FOUR OF A KIND
        else if (temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = temp[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }

        return false;
    }

    private bool FullHouse()
    {
        //if FIRST 3 cards are a THREE OF A KIND and the LAST 2 cards is a PAIR
        if (temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            secondHighCard = temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        //if FIRST 2 cards are a PAIR and the LAST 3 are a THREE OF A KIND
        else if (temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            secondHighCard = temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        return false;
    }

    private bool Flush()
    {
        if (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5)
        {
            highCard = temp[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        return false;
    }

    private bool Straight()
    {
        if (temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() + 1 == temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() + 1 == temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() + 1 == temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() + 1 == temp[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = temp[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }


        return false;
    }

    private bool ThreeOfAKind()
    {
        if (temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        else if (temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        else if (temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = temp[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        return false;
    }

    private bool TwoPair()
    {
        if (temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            secondHighCard = temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }

        else if (temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            secondHighCard = temp[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }

        else if (temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() &&
            temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            secondHighCard = temp[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
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
        if (temp[0].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        else if (temp[1].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        else if (temp[2].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
            return true;
        }
        else if (temp[3].GetComponent<CardPropertiesPoker5>().GetValueOfCard() == temp[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard())
        {
            highCard = temp[4].GetComponent<CardPropertiesPoker5>().GetValueOfCard();
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
