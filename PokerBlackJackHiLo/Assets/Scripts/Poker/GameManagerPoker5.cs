using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerPoker5;

public class GameManagerPoker5 : MonoBehaviour
{
    public PlayerPoker5 playerScript;
    public PlayerPoker5 dealerScript;

    public GameObject redrawUI;

    List<int> redrawList = new List<int>();

    public Button redraw1;
    public Button redraw2;
    public Button redraw3;
    public Button redraw4;
    public Button redraw5;
    public Button confirmRedrawButton;
    public Button dealButton;
    public Button sortTestButton;

    
    private void Start()
    {
        dealButton.onClick.AddListener(() => DealClicked());
        redraw1.onClick.AddListener(() => RedrawFirstCard());
        redraw2.onClick.AddListener(() => RedrawSecondCard());
        redraw3.onClick.AddListener(() => RedrawThirdCard());
        redraw4.onClick.AddListener(() => RedrawFourthCard());
        redraw5.onClick.AddListener(() => RedrawFifthCard());
        confirmRedrawButton.onClick.AddListener(() => ConfirmRedraw());
        sortTestButton.onClick.AddListener(() => SortHand());
    }

    public void SortHand()
    {
        playerScript.SortHands();
        dealerScript.SortHands();
    }
    public void ConfirmRedraw()
    {
        redrawUI.SetActive(false);
        playerScript.RedrawHand(redrawList);

        EvaluateBothHands();
    }

    public void EvaluateBothHands()
    {
        PokerHand playerHand = playerScript.EvaluateHand();
        PokerHand dealerHand = dealerScript.EvaluateHand();

        Debug.Log("You have " + playerHand + " and a high card of " + playerScript.highCard);
        Debug.Log("Dealer has " + dealerHand + " and a high card of " + dealerScript.highCard);

        if (playerHand > dealerHand)
        {
            Debug.Log("Player wins!");
        }
        else if(playerHand<dealerHand)
        {
            Debug.Log("Dealer wins!");
        }
        else if (playerHand == dealerHand)
        {
            if(playerScript.highCard > dealerScript.highCard)
            {
                Debug.Log("Player wins!");
            }
            else if (playerScript.highCard < dealerScript.highCard)
            {
                Debug.Log("Dealer wins!");
            }
            else if (playerScript.highCard == dealerScript.highCard)
            {
                if (playerScript.secondHighCard > dealerScript.secondHighCard)
                {
                    Debug.Log("Player wins!");
                }
                else if (playerScript.secondHighCard < dealerScript.secondHighCard)
                {
                    Debug.Log("Dealer wins!");
                }
                else
                {
                    Debug.Log("Draw! Split the pot.");
                }
            }
            else
            {
                Debug.Log("Draw! Split the pot.");
            }
        }
        
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
        redrawList.Clear();
        GameObject.Find("Deck").GetComponent<DeckPoker5>().Shuffle();
        playerScript.StartingHands();
        dealerScript.StartingHands();

        redraw1.gameObject.SetActive(true);
        redraw2.gameObject.SetActive(true);
        redraw3.gameObject.SetActive(true);
        redraw4.gameObject.SetActive(true);
        redraw5.gameObject.SetActive(true);
        redrawUI.SetActive(true);
    }
}
