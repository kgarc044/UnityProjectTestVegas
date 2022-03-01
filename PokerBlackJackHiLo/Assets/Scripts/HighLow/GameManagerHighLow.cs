using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerHighLow : MonoBehaviour
{
    public Text mainText;
    public Text betsText;
    public Text cashText;

    public Button dealButton;
    public Button lowerButton;
    public Button higherButton;
    public Button betButton;

    public PlayerHighLow playerScript;

    public GameObject hideCard; 

    private string userGuess = "";

    int pot = 0;

    void Start()
    {
        dealButton.onClick.AddListener(() => DealClicked());
        lowerButton.onClick.AddListener(() => LowerClicked());
        higherButton.onClick.AddListener(() => HigherClicked());
        betButton.onClick.AddListener(() => BetClicked());
    }

    private void DealClicked()
    {
        dealButton.gameObject.SetActive(false);
        mainText.gameObject.SetActive(false);
        userGuess = "";
        GameObject.Find("Deck").GetComponent<DeckHighLow>().Shuffle();
        hideCard.gameObject.SetActive(true);
        playerScript.StartingCard();
        pot = 40;
        betsText.text = "Bets: $" + pot.ToString();
        playerScript.AdjustMoney(-20);
        cashText.text = "$" + playerScript.GetMoney().ToString();
        lowerButton.gameObject.SetActive(true);
        higherButton.gameObject.SetActive(true);
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

    private void LowerClicked()
    {
        userGuess = "Lower";
        lowerButton.gameObject.SetActive(false);
        higherButton.gameObject.SetActive(false);
        CheckIfWon(playerScript.EvaluateGuess(userGuess));
    }

    private void HigherClicked()
    {
        userGuess = "Higher";
        lowerButton.gameObject.SetActive(false);
        higherButton.gameObject.SetActive(false);
        CheckIfWon(playerScript.EvaluateGuess(userGuess));
    }

    public void CheckIfWon(bool correct)
    {
        hideCard.gameObject.SetActive(false);
        if (correct)
        {
            Debug.Log("Player guessed correctly");
            mainText.text = "You win!";
            playerScript.AdjustMoney(pot);
            mainText.gameObject.SetActive(true);
            dealButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Player guessed wrong");
            mainText.text = "You lose!";
            mainText.gameObject.SetActive(true);
            dealButton.gameObject.SetActive(true);
        }
    }
}
