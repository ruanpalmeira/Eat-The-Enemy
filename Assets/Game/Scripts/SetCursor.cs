using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SetCursor : MonoBehaviour {
 
    void Start () {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        Cursor.SetCursor(rend.sprite.texture, Vector2.zero, CursorMode.Auto);
    }
 
}