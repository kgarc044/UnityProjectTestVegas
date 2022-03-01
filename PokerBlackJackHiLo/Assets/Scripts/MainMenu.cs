using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayHighLow()
    {
        SceneManager.LoadScene("HighLow");
    }

    public void PlayBlackJack()
    {
        SceneManager.LoadScene("BlackJack");
    }

    public void PlayPoker5()
    {
        SceneManager.LoadScene("Poker5");
    }

    public void PlayPoker7()
    {
        SceneManager.LoadScene("Poker7");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }


}
