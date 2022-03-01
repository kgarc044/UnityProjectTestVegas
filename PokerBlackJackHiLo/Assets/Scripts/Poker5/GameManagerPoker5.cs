using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerPoker5;

public class GameManagerPoker5 : MonoBehaviour
{
    public Text mainText;
    public Text betsText;
    public Text cashText;

    public PlayerPoker5 playerScript;
    public PlayerPoker5 dealerScript;

    public GameObject redrawUI;
    public GameObject hideDealerCards;

    int pot = 0;

    bool roundOver = false;
    bool finishedBetting = false;

    List<int> redrawList = new List<int>();

    public Button redraw1;
    public Button redraw2;
    public Button redraw3;
    public Button redraw4;
    public Button redraw5;
    public Button confirmRedrawButton;
    public Button doneFirstBetting;
    public Button doneSecondBetting;
    public Button dealButton;
    public Button betButton;


    private void Start()
    {
        dealButton.onClick.AddListener(() => DealClicked());
        redraw1.onClick.AddListener(() => RedrawFirstCard());
        redraw2.onClick.AddListener(() => RedrawSecondCard());
        redraw3.onClick.AddListener(() => RedrawThirdCard());
        redraw4.onClick.AddListener(() => RedrawFourthCard());
        redraw5.onClick.AddListener(() => RedrawFifthCard());
        doneFirstBetting.onClick.AddListener(() => FirstRoundBetting());
        doneSecondBetting.onClick.AddListener(() => SecondRoundBetting());
        confirmRedrawButton.onClick.AddListener(() => ConfirmRedraw());
        betButton.onClick.AddListener(() => BetClicked());
    }

    public void FirstRoundBetting()
    {
        doneFirstBetting.gameObject.SetActive(false);
        betButton.gameObject.SetActive(false);
        redraw1.gameObject.SetActive(true);
        redraw2.gameObject.SetActive(true);
        redraw3.gameObject.SetActive(true);
        redraw4.gameObject.SetActive(true);
        redraw5.gameObject.SetActive(true);
        redrawUI.SetActive(true);
    }
    public void SecondRoundBetting()
    {
        doneSecondBetting.gameObject.SetActive(false);
        betButton.gameObject.SetActive(false);
        EvaluateBothHands();
    }

    public void ConfirmRedraw()
    {
        redrawUI.SetActive(false);
        playerScript.RedrawHand(redrawList);
        playerScript.SortHands();
        dealerScript.SortHands();
        doneSecondBetting.gameObject.SetActive(true);
        betButton.gameObject.SetActive(true);
    }
    
    public void RedrawFirstCard()
    {
        redrawList.Add(0);
        redraw1.gameObject.SetActive(false);
    }
    public void RedrawSecondCard()
    {
        redrawList.Add(1);
        redraw2.gameObject.SetActive(false);
    }
    public void RedrawThirdCard()
    {
        redrawList.Add(2);
        redraw3.gameObject.SetActive(false);
    }
    public void RedrawFourthCard()
    {
        redrawList.Add(3);
        redraw4.gameObject.SetActive(false);
    }
    public void RedrawFifthCard()
    {
        redrawList.Add(4);
        redraw5.gameObject.SetActive(false);
    }

    private void DealClicked()
    {
        hideDealerCards.gameObject.SetActive(true);
        dealButton.gameObject.SetActive(false);
        redrawList.Clear();
        mainText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckPoker5>().Shuffle();
        playerScript.StartingHands();
        dealerScript.StartingHands();

        playerScript.SortHands();
        dealerScript.SortHands();

        pot = 40;
        betsText.text = "Bets: $" + pot.ToString();
        playerScript.AdjustMoney(-20);
        cashText.text = "$" + playerScript.GetMoney().ToString();

        doneFirstBetting.gameObject.SetActive(true);
        betButton.gameObject.SetActive(true);

    }

    private void BetClicked()
    {
        Text newBet = betButton.GetComponentInChildren(typeof(Text)) as Text;
        int intBet = int.Parse(newBet.text.ToString().Remove(0, 1));
        playerScript.AdjustMoney(-intBet);
        cashText.text = "$" + playerScript.GetMoney().ToString();
        pot += (intBet * 2);
        betsText.text = "Bets: $" + pot.ToString();
    }

    public void EvaluateBothHands()
    {
        PokerHand playerHand = playerScript.EvaluateHand();
        PokerHand dealerHand = dealerScript.EvaluateHand();
        roundOver = true;

        Debug.Log("You have " + playerHand + " and a high " + playerHand + " of " + playerScript.highCard);
        Debug.Log("Dealer has " + dealerHand + " and a high " + dealerHand + " of " + dealerScript.highCard);

        hideDealerCards.gameObject.SetActive(false);

        if (playerHand > dealerHand)
        {
            mainText.text = "You win!";
            playerScript.AdjustMoney(pot);
        }
        else if (playerHand < dealerHand)
        {
            mainText.text = "Dealer wins!";
        }
        else if (playerHand == dealerHand)
        {
            if (playerScript.highCard > dealerScript.highCard)
            {
                mainText.text = "You win!";
                playerScript.AdjustMoney(pot);
            }
            else if (playerScript.highCard < dealerScript.highCard)
            {
                mainText.text = "Dealer wins!";
            }
            else if (playerScript.highCard == dealerScript.highCard)
            {
                if (playerScript.secondHighCard > dealerScript.secondHighCard)
                {
                    mainText.text = "You win!";
                    playerScript.AdjustMoney(pot);
                }
                else if (playerScript.secondHighCard < dealerScript.secondHighCard)
                {
                    mainText.text = "Dealer wins!";
                }
                else
                {
                    mainText.text = "Draw! Split the pot.";
                    playerScript.AdjustMoney(pot / 2);
                }
            }
            else
            {
                mainText.text = "Draw! Split the pot.";
                playerScript.AdjustMoney(pot / 2);
            }
        }

        if(roundOver)
        {
            mainText.gameObject.SetActive(true);
            dealButton.gameObject.SetActive(true);
        }
    }
}
