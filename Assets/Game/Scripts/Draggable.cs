using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour{

    public bool isAnimal = false;
    public bool isPowerUp = false;
    public bool canDrag = true;
    public bool isDragged = false;
    private Vector3 mouseDragStartPos;
    private Vector3 spriteDragStartPos;
    private Movement movement;

    private void Start(){
        movement = GetComponent<Movement>();
    }

    private void OnMouseDown(){
        if(canDrag){
            isDragged = true;
            mouseDragStartPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spriteDragStartPos = transform.localPosition;
        }
    }

    private void OnMouseDrag(){
        if(canDrag){
            if(isDragged){
                transform.localPosition = spriteDragStartPos + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPos);
            }
        }
    }

    private void OnMouseUp(){
        if(canDrag){
            isDragged = false;
            if(isAnimal){
                transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
                if(movement.verticalMovement){
                    movement.targetPosition.x = transform.position.x;
                }else{
                    movement.targetPosition.y = transform.position.y;
                }
            }
        }
    }
}