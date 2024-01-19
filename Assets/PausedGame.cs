using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedGame : MonoBehaviour
{
    [SerializeField] GameObject pausedGameMenu;
    public GameObject gameEndMenu;
    private PlayerController playerController;
    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public void StopGame()
    {
        pausedGameMenu.SetActive(true); //zobrazí UI pausedGame
        Time.timeScale = 0f; //nastaví èas na 0
        playerController.isPaused = true; //zastaví støílení
    }
    public void ResumeGame()
    {
        pausedGameMenu.SetActive(false); //schová UI pausedGame
        Time.timeScale = 1f; //nastaví èas na 1
        playerController.isPaused = false; //povolí støílení
    }
    public void ReturnToMainMenu()
    {
        pausedGameMenu.SetActive(false); //schová UI pausedGame
        Time.timeScale = 1f; //nastaví èas na 1
        playerController.isPaused = false;//povolí støílení
        SceneManager.LoadScene("MainMenu"); //naète novou scénu
    }
    public void Retry()
    {
        gameEndMenu.SetActive(false); //schová UI
        Time.timeScale = 1f;//nastaví èas na 1
        playerController.isPaused = false;//povolí støílení
        SceneManager.LoadScene("Level1"); //naète scénu
    }
}
