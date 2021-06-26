using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour{


    public static bool GameIsPaused = false;
    public bool gameStarting;
    public Animator uianimator;
    public GameObject pauseMenuUI;
    public GameObject MenuUI;

    private void Start() {
        uianimator = GetComponent<Animator>();
        Time.timeScale = 1;
        
        if(SceneManager.GetActiveScene().name == "Game"){
            gameStarting = false;
            uianimator.SetInteger("slider", 3);
            MenuUI.SetActive(false);
            //StartCoroutine(LoadGamer());
        }else{
            pauseMenuUI.GetComponent<CanvasGroup>().alpha = 0;
            MenuUI.SetActive(true);
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused){
                Resume();
            }else{
                Pause("pause");
            }
        }
    }

    public void Resume(){
        uianimator.SetInteger("slider", 3);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void Pause(string estado){
        switch (estado){
            case "pause":
                pauseMenuUI.GetComponent<CanvasGroup>().alpha = 1;
                pauseMenuUI.GetComponentInChildren<Text>().fontSize = 50;
                pauseMenuUI.transform.GetChild(1).gameObject.SetActive(true);
                pauseMenuUI.transform.GetChild(2).gameObject.SetActive(false);
                pauseMenuUI.transform.GetChild(3).gameObject.SetActive(true);
                uianimator.SetInteger("slider", 1);
                Time.timeScale = 0;
                GameIsPaused = true;
                break;
            case "win":
                pauseMenuUI.GetComponent<CanvasGroup>().alpha = 1;
                pauseMenuUI.GetComponentInChildren<Text>().fontSize = 50;
                pauseMenuUI.GetComponentInChildren<Text>().text = "You Won"; 
                uianimator.SetInteger("slider", 1);
                Time.timeScale = 0;
                GameIsPaused = true;
                
                break;
            case "gameover":
                pauseMenuUI.GetComponent<CanvasGroup>().alpha = 1;
                pauseMenuUI.GetComponentInChildren<Text>().fontSize = 50;
                pauseMenuUI.GetComponentInChildren<Text>().text = "GAME OVER"; 
                uianimator.SetInteger("slider", 1);
                Time.timeScale = 0;
                GameIsPaused = true;
                break;
            case "info":
                pauseMenuUI.GetComponent<CanvasGroup>().alpha = 1;
                pauseMenuUI.GetComponentInChildren<Text>().fontSize = 0;
                pauseMenuUI.GetComponentInChildren<Text>().text = "Help the mice defend their castle against the invading snakes!"; 
                pauseMenuUI.transform.GetChild(1).gameObject.SetActive(false);
                pauseMenuUI.transform.GetChild(2).gameObject.SetActive(true);
                pauseMenuUI.transform.GetChild(3).gameObject.SetActive(false);
                uianimator.SetInteger("slider", 1);
                Time.timeScale = 0;
                GameIsPaused = true;
                break;
            default:
                break;
        }
        
    }

    public void LoadMenu(){
        uianimator.SetInteger("slider", 3);
        pauseMenuUI.GetComponent<CanvasGroup>().alpha = 1;
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
        gameStarting = false;
    }

    public void LoadGame(){
        StartCoroutine(LoadLevel());
    }

    public void QuitGame(){
        Application.Quit();
    }
    
    IEnumerator LoadLevel(){
        pauseMenuUI.GetComponent<CanvasGroup>().alpha = 0;
        uianimator.SetInteger("slider", 1);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    IEnumerator LoadGamer(){
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;
    }
}
