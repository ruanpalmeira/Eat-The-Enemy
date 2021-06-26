using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{

    public AudioClip mouseHurt, hit, boots,shake,playerDamage, playerJump, playerLost, playerWin, extraLife, goodApple;
    public AudioClip[] hits, walk, crunch, hisses;
    public bool lost = false;

    public void PlaySound(string som){
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.SetParent(this.transform);
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

        switch(som){
            case "mouse":
                audioSource.PlayOneShot(mouseHurt);
                break;
            case "hit":
                var hit = Random.Range(0,4);
                audioSource.PlayOneShot(hits[hit]);
                break;
            case "lost":
                if(!lost){
                    audioSource.PlayOneShot(playerLost);
                    lost = true;
                }else{
                     Destroy(soundGameObject);
                }
                break;
            case "walk":
                var step = Random.Range(0,2);
                audioSource.PlayOneShot(walk[step]);
                break;
            case "snake":
                var hit2 = Random.Range(0,5);
                audioSource.PlayOneShot(hisses[hit2]);
                break;
            case "boots":
                audioSource.PlayOneShot(boots);
                break;
            case "shake":
                audioSource.PlayOneShot(shake);
                break;
            case "apple":
                var bite = Random.Range(0,6);
                audioSource.PlayOneShot(crunch[bite]);
                break;
            default:
                Debug.LogError("Sound not found!");
                break;
        }
        
        Destroy(soundGameObject, 2.0f);
        
    }
}
