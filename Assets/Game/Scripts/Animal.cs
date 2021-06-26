using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour{

    public string ID;
    public int life = 2;
    public GameController gameController;
    public Movement movement;
    public GameObject smoke_Prefab;
    public float rateToSpawnSmoke = 0.5f;
    public bool fighting = false;
    public SoundManager soundManager;
    public Draggable draggable;

    private void Start(){
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        movement = GetComponent<Movement>();
        draggable = GetComponent<Draggable>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        
        if(other.tag == "Animal"){
            //Debug.Log(this.ID + " X " + other.GetComponent<Animal>().ID);
            if(!fighting){
                if((this.draggable.isDragged == false && other.GetComponent<Draggable>().isDragged == false)){
                    Debug.Log(this.draggable.isDragged );
                    switch (other.GetComponent<Animal>().ID){
                        case "snake":
                                if(this.ID == "mouse"){
                                    movement.AllowMovement(false);
                                    fighting = true;
                                    StartCoroutine(spawnSmoke());
                                    //gameController.AddPoints(1);
                                    //Destroy(this.gameObject);
                                }
                            break;
                            
                        case "mouse":
                                if(this.ID == "snake"){
                                    movement.AllowMovement(false);
                                    fighting = true;
                                    StartCoroutine(spawnSmoke());
                                    //gameController.AddPoints(1);
                                    //Destroy(this.gameObject);
                                }
                            break;
                        
                        default:
                            break;
                    }
                }
            }
            
        }
        if(other.tag == "PowerUp"){
            switch (other.GetComponent<PowerUp>().ID){
                case "boots":
                    if(!other.GetComponent<Draggable>().isDragged){
                        soundManager.PlaySound("boots");
                        movement.IncreaseSpeed(other.GetComponent<PowerUp>().SpeedBoots());
                        Destroy(other.gameObject);
                    }
                    
                    break;
                case "apple":
                    if(!other.GetComponent<Draggable>().isDragged){
                        soundManager.PlaySound("apple");
                        transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
                        life += 3;
                        //movement.IncreaseSpeed(other.GetComponent<PowerUp>().SpeedBoots());
                        Destroy(other.gameObject);
                    }
                    
                    break;

                case "book":
                    if(!other.GetComponent<Draggable>().isDragged){
                        //soundManager.PlaySound("book");
                        gameController.slowTime();

                        //movement.IncreaseSpeed(other.GetComponent<PowerUp>().SpeedBoots());
                        Destroy(other.gameObject);
                    }
                    
                    break;
                default:
                    break;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Animal"){
            //Debug.Log("nada");
            this.fighting = false;
            movement.AllowMovement(true);
        }
        
    }

    IEnumerator spawnSmoke(){
        while (fighting){
            removeLife();
            Destroy(Instantiate(smoke_Prefab, transform.position, transform.rotation), 0.5f);
            yield return new WaitForSeconds(rateToSpawnSmoke);
        }
    }

    public void removeLife(){
        //Debug.Log("Aqui");
        
        if(this.life > 1){
            soundManager.PlaySound("hit");
            //Debug.Log("Aqui2");
            this.life--;
        }else{
            soundManager.PlaySound(ID);
            this.fighting = false;
            if(ID == "snake"){
                gameController.AddPoints(1);
                
            }
            Destroy(this.gameObject);
        }
    }

    private void walk(){
        soundManager.PlaySound("walk");
    }
}