using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerPoker7;

public class GameManagerPoker7 : MonoBehaviour
{
    public Text mainText;
    public Text betsText;
    public Text cashText;

    public PlayerPoker7 playerScript;
    public PlayerPoker7 dealerScript;

    public GameObject hideDealerCards;
    public GameObject hideFirstCard;
    public GameObject hideSecondCard;
    public GameObject hideThirdCard;
    public GameObject hideFourthCard;
    public GameObject hideFifthCard;

    public GameObject removeButtons;
    public GameObject originalHandPlayer;
    public GameObject originalHandDealer;

    int pot = 0;
    int removeCounter = 0;

    bool roundOver = false;
    bool finishedBetting = false;

    List<int> removeList = new List<int>();

    public Button doneFirstBetting;
    public Button doneSecondBetting;
    public Button doneThirdBetting;
    public Button doneFourthBetting;
    public Button doneFifthBetting;
    public Button dealButton;
    public Button betButton;
    public Button confirmRemoveButton;

    public Button chooseToRemove1;
    public Button chooseToRemove2;
    public Button chooseToRemove3;
    public Button chooseToRemove4;
    public Button chooseToRemove5;
    public Button chooseToRemove6;
    public Button chooseToRemove7;


    private void Start()
    {
        chooseToRemove1.onClick.AddListener(() => ChooseRemove1());
        chooseToRemove2.onClick.AddListener(() => ChooseRemove2());
        chooseToRemove3.onClick.AddListener(() => ChooseRemove3());
        chooseToRemove4.onClick.AddListener(() => ChooseRemove4());
        chooseToRemove5.onClick.AddListener(() => ChooseRemove5());
        chooseToRemove6.onClick.AddListener(() => ChooseRemove6());
        chooseToRemove7.onClick.AddListener(() => ChooseRemove7());

        confirmRemoveButton.onClick.AddListener(() => ConfirmRemove());


        dealButton.onClick.AddListener(() => DealClicked());
        doneFirstBetting.onClick.AddListener(() => FirstRoundBetting());
        doneSecondBetting.onClick.AddListener(() => SecondRoundBetting());
        doneThirdBetting.onClick.AddListener(() => ThirdRoundBetting());
        doneFourthBetting.onClick.AddListener(() => FourthRoundBetting());
        doneFifthBetting.onClick.AddListener(() => FifthRoundBetting());
        betButton.onClick.AddListener(() => BetClicked());
    }

    private void Update()
    {
        if(removeCounter >= 2)
        {
            removeButtons.gameObject.SetActive(false);
        }
    }

    public void ConfirmRemove()
    {
        originalHandPlayer.gameObject.SetActive(false);
        originalHandDealer.gameObject.SetActive(false);
        playerScript.Remove(removeList);
        dealerScript.Remove(removeList);
        confirmRemoveButton.gameObject.SetActive(false);
        playerScript.SortHands(playerScript.GetHand());
        dealerScript.SortHands(dealerScript.GetHand());
        EvaluateBothHands();
    }

    public void ChooseRemove1()
    {
        removeList.Add(0);
        chooseToRemove1.gameObject.SetActive(false);
        removeCounter++;
    }

    public void ChooseRemove2()
    {
        removeList.Add(1);
        chooseToRemove2.gameObject.SetActive(false);
        removeCounter++;
    }
    public void ChooseRemove3()
    {
        removeList.Add(2);
        chooseToRemove3.gameObject.SetActive(false);
        removeCounter++;
    }
    public void ChooseRemove4()
    {
        removeList.Add(3);
        chooseToRemove4.gameObject.SetActive(false);
        removeCounter++;
    }
    public void ChooseRemove5()
    {
        removeList.Add(4);
        chooseToRemove5.gameObject.SetActive(false);
        removeCounter++;
    }
    public void ChooseRemove6()
    {
        removeList.Add(5);
        chooseToRemove6.gameObject.SetActive(false);
        removeCounter++;
    }
    public void ChooseRemove7()
    {
        removeList.Add(6);
        chooseToRemove7.gameObject.SetActive(false);
        removeCounter++;
    }

    public void FirstRoundBetting()
    {
        hideFirstCard.gameObject.SetActive(false);
        doneFirstBetting.gameObject.SetActive(false);
        doneSecondBetting.gameObject.SetActive(true);
    }
    public void SecondRoundBetting()
    {
        hideSecondCard.gameObject.SetActive(false);
        doneSecondBetting.gameObject.SetActive(false);
        doneThirdBetting.gameObject.SetActive(true);
    }

    public void ThirdRoundBetting()
    {
        hideThirdCard.gameObject.SetActive(false);
        doneThirdBetting.gameObject.SetActive(false);
        doneFourthBetting.gameObject.SetActive(true);
    }

    public void FourthRoundBetting()
    {
        hideFourthCard.gameObject.SetActive(false);
        doneFourthBetting.gameObject.SetActive(false);
        doneFifthBetting.gameObject.SetActive(true);
    }

    public void FifthRoundBetting()
    {
        hideFifthCard.gameObject.SetActive(false);
        doneFifthBetting.gameObject.SetActive(false);
        removeButtons.SetActive(true);
        confirmRemoveButton.gameObject.SetActive(true);
        chooseToRemove1.gameObject.SetActive(true);
        chooseToRemove2.gameObject.SetActive(true);
        chooseToRemove3.gameObject.SetActive(true);
        chooseToRemove4.gameObject.SetActive(true);
        chooseToRemove5.gameObject.SetActive(true);
        chooseToRemove6.gameObject.SetActive(true);
        chooseToRemove7.gameObject.SetActive(true);
    }

    private void DealClicked()
    {
        removeCounter = 0;

        originalHandPlayer.gameObject.SetActive(true);
        originalHandDealer.gameObject.SetActive(true);

        hideDealerCards.gameObject.SetActive(true);
        hideFirstCard.gameObject.SetActive(true);
        hideSecondCard.gameObject.SetActive(true);
        hideThirdCard.gameObject.SetActive(true);
        hideFourthCard.gameObject.SetActive(true);
        hideFifthCard.gameObject.SetActive(true);

        dealButton.gameObject.SetActive(false);
        removeList.Clear();
        mainText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckPoker5>().Shuffle();
        playerScript.StartingHands();
        dealerScript.StartingHands();


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
        PokerHand playerHand = playerScript.EvaluateHand(playerScript.GetHand());
        PokerHand dealerHand = dealerScript.EvaluateHand(dealerScript.GetHand());
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

        if (roundOver)
        {
            mainText.gameObject.SetActive(true);
            dealButton.gameObject.SetActive(true);
        }
    }
}
