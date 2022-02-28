using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerHighLow : MonoBehaviour
{
    public Button dealButton;
    public Button lowerButton;
    public Button higherButton;
    public PlayerHighLow playerScript;

    public GameObject hideCard; 

    private string userGuess = "";

    void Start()
    {
        dealButton.onClick.AddListener(() => DealClicked());
        lowerButton.onClick.AddListener(() => LowerClicked());
        higherButton.onClick.AddListener(() => HigherClicked());
    }

    private void DealClicked()
    {
        userGuess = "";
        GameObject.Find("Deck").GetComponent<DeckHighLow>().Shuffle();
        hideCard.gameObject.SetActive(true);
        playerScript.StartingCard();

        lowerButton.gameObject.SetActive(true);
        higherButton.gameObject.SetActive(true);
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
        }
        else
        {
            Debug.Log("Player guessed wrong");
        }
    }
}
