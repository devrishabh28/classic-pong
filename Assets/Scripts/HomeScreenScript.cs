using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenScript : MonoBehaviour
{

    [SerializeField] private GameData gameData;

    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void singlePlayer()
    {
        gameData.AI = true;
    }

    public void setMouse()
    {
        gameData.mouse = true;
    }

    public void setKeyboard()
    {
        gameData.mouse = false;
    }

    public void multiPlayer()
    {
        gameData.AI = false;
        gameData.mouse = false;
    }

    public void quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
