using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{

    public bool canMove = true;
    public bool isShaking = false;
    public bool randomSpeed = false;
    public bool verticalMovement = true;
    public Vector2 targetPosition;
    public float speed = 0.5f;
    public Animator animator;
    public GameObject shake;
    public SoundManager soundManager;
    public GameController gameController;

    void Start(){
        animator = GetComponent<Animator>();
        shake = GameObject.Find("Tilemap_castle");
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        if(verticalMovement){
            targetPosition = new Vector2(transform.position.x, -3.5f);
            animator.SetBool("Down", true);
            speed = 0.5f;
        }else{
            targetPosition = new Vector2(10 , transform.position.y);
            animator.SetBool("Right", true);
            speed = 0.75f;
        }
        if(randomSpeed){
            speed = Random.Range(0.5f, 1.3f);
        }
    }

    void Update(){
        if(canMove){
            if((Vector2)transform.position != targetPosition){
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }else{
                if(!isShaking){
                    isShaking = true;
                    StartCoroutine(shakeCastle());
                }
                Destroy(this.gameObject, 0.5f);
                
            }
        }
    }

    public void IncreaseSpeed(float increment){
        speed += increment;
    }

    public void AllowMovement(bool permission){
        canMove = permission;
    }

    
    IEnumerator shakeCastle(){
        if(GetComponent<Animal>().ID == "snake"){
            shake.GetComponent<Animator>().SetTrigger("Shake");
            soundManager.PlaySound("shake");
            gameController.RemoveLife(1);
            yield return new WaitForSeconds(0.5f);
        isShaking = false;
        }
        
    }
}