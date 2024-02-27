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
        pausedGameMenu.SetActive(true); //zobraz� UI pausedGame
        Time.timeScale = 0f; //nastav� �as na 0
        playerController.isPaused = true; //zastav� st��len�
    }
    public void ResumeGame()
    {
        pausedGameMenu.SetActive(false); //schov� UI pausedGame
        Time.timeScale = 1f; //nastav� �as na 1
        playerController.isPaused = false; //povol� st��len�
    }
    public void ReturnToMainMenu()
    {
        pausedGameMenu.SetActive(false); //schov� UI pausedGame
        Time.timeScale = 1f; //nastav� �as na 1
        playerController.isPaused = false;//povol� st��len�
        SceneManager.LoadScene("MainMenu"); //na�te novou sc�nu
    }
    public void Retry()
    {
        gameEndMenu.SetActive(false); //schov� UI
        Time.timeScale = 1f;//nastav� �as na 1
        playerController.isPaused = false;//povol� st��len�
        SceneManager.LoadScene("Level1"); //na�te sc�nu
    }
}
