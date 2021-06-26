using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour{

    public int points = 0;
    public int choice;
    public int castleLife = 10;
    public int wavenumber = 1;
    public float speed = 5f;
    public Text pointsText;
    public GameObject snake_Prefab, mouse_Prefab;
    public GameObject[] powerUpsPrefabs;
    public Transform snakeParent, mousesParent, powerUpsParent;
    public GameObject tile1, tile2, tile3, tile4;
    public float rateToSpawn = 5f;
    public bool canSpawn = true;
    public bool isPaused = true;
    public bool movetile1 = false, movetile2 = false, movetile3 = false, movetile4 = false;
    public PauseScript pauseScript;
    public SoundManager soundManager;
    public Vector3[] verticalSpawnLocations = {new Vector3(-3,6,0), new Vector3(0,6,0), new Vector3(3,6,0)};
    public Vector3[] horizontalSpawnLocations = {new Vector3(-10,-1,0), new Vector3(-10,1,0), new Vector3(-10,3,0)};
    
    void Start(){
        pointsText.text = "0";
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        StartCoroutine(SpawnAnimal());
        StartCoroutine(SpawnPowerUp());
    }

    void Update(){
        pointsText.text = points.ToString();
        if(movetile1){
            var targetPosition = new Vector2(tile1.transform.position.x, -9.0f);
            tile1.transform.position = Vector2.MoveTowards(tile1.transform.position, targetPosition, speed*Time.deltaTime);
        }
        if(movetile2){
            var targetPosition2 = new Vector2(tile2.transform.position.x, -9.0f);
            tile2.transform.position = Vector2.MoveTowards(tile2.transform.position, targetPosition2, speed*Time.deltaTime);
        }
        if(movetile3){
            var targetPosition3 = new Vector2(tile3.transform.position.x, -9.0f);
            tile3.transform.position = Vector2.MoveTowards(tile3.transform.position, targetPosition3, speed*Time.deltaTime);
        }
        if(movetile4){
            var targetPosition4 = new Vector2(tile4.transform.position.x, -9.0f);
            tile4.transform.position = Vector2.MoveTowards(tile4.transform.position, targetPosition4, speed*Time.deltaTime);
        }
    }

    public void AddPoints(int amount){
        points += amount;
        if(points == 5||points == 15||points == 30||points == 60){
            wavenumber++;
            switch (wavenumber){
                case 2:
                    movetile1 = true;
                    break;
                case 3:
                    movetile2 = true;
                    break;
                case 4:
                    movetile3 = true;
                    break;
                case 5:
                    movetile4 = true;
                    break;

                default:
                    break;
            }
        }
        if(points > 100){
            pauseScript.Pause("win");
        }
    }

    public void RemovePoints(int amount){
        points -= amount;
    }

    public void RemoveLife(int amount){
        castleLife -= amount;
        if(castleLife <= 0){
            pauseScript.Pause("gameover");
            soundManager.PlaySound("lost");
        }
    }

    public void AnimalToSpawn(string animal){
        
        switch (animal){
            case "snake":
                var verticalPosition = Random.Range(0, wavenumber);
                if(verticalPosition > verticalSpawnLocations.Length){
                    verticalPosition = verticalSpawnLocations.Length-1;
                }
                var snake = Instantiate(snake_Prefab, verticalSpawnLocations[verticalPosition], transform.rotation);
                snake.transform.SetParent(snakeParent);
                break;

            case "mouse":
                var horizontalPosition = Random.Range(0, horizontalSpawnLocations.Length);
                var mouse = Instantiate(mouse_Prefab, horizontalSpawnLocations[horizontalPosition], transform.rotation);
                mouse.transform.SetParent(mousesParent);
                break;

            default:
                break;
        }
    }

    IEnumerator SpawnAnimal(){
        while(canSpawn){
            switch(wavenumber){
                case 1:
                    AnimalToSpawn("mouse");
                    yield return new WaitForSeconds(rateToSpawn);
                    AnimalToSpawn("snake");
                    break;
                case 2:
                    
                    AnimalToSpawn("mouse");
                    yield return new WaitForSeconds(rateToSpawn-1);
                     AnimalToSpawn("snake");
                    break;
                case 3:
                    
                    AnimalToSpawn("mouse");
                    yield return new WaitForSeconds(rateToSpawn-2);
                     AnimalToSpawn("snake");
                    break;
                case 4:
                    
                    AnimalToSpawn("mouse");
                    yield return new WaitForSeconds(rateToSpawn-3);
                     AnimalToSpawn("snake");
                    break;
                case 5:
                    
                    AnimalToSpawn("mouse");
                    yield return new WaitForSeconds(rateToSpawn-4);
                    AnimalToSpawn("snake");
                    break;

                default:
                    Debug.LogError("wave unvalid");
                    break;
            }
        }
    }

    IEnumerator SpawnPowerUp(){
        while(canSpawn){
            choice = Random.Range(0, powerUpsPrefabs.Length);
                var horizontalPos = Random.Range(-8, 8);
                var verticalPos = Random.Range(-2, 4);
                var powerup = Instantiate(powerUpsPrefabs[choice], new Vector3(horizontalPos, verticalPos, 1), transform.rotation);
                powerup.transform.SetParent(powerUpsParent);
            switch(wavenumber){
                case 1:
                    yield return new WaitForSeconds(rateToSpawn*(3.0f));
                    break;
                case 2:
                    yield return new WaitForSeconds(rateToSpawn*(2.5f));
                    break;
                case 3:
                    yield return new WaitForSeconds(rateToSpawn*(2.0f));
                    break;
                case 4:
                    yield return new WaitForSeconds(rateToSpawn*(1.5f));
                    break;
                case 5:
                    yield return new WaitForSeconds(rateToSpawn*(1.0f));
                    break;
                default:
                    Debug.LogError("wave unvalid");
                    break;
            }
        }
    }

    public void slowAudio(){
        GetComponent<AudioSource>().pitch = 0.8f;
    }

    public void speedAudio(){
        GetComponent<AudioSource>().pitch = 1.0f;
    }
    public void slowTime(){
        StartCoroutine(Slow());
    }

    IEnumerator Slow(){
        slowAudio();
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(3);
        Time.timeScale = 1.0f;
        speedAudio();
    }
}