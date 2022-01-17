using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int itemCount;
    public GameManager manager;
    public AudioSource audioS; 
    public Animator anim;
    void Awake(){
        itemCount =0;
    }

    private void OnTriggerEnter(Collider other){
        if (other.tag =="item") {
            itemCount ++;
            other.gameObject.SetActive(false);
            manager.GetItem(itemCount);
            audioS.Play();
        } else if (other.tag =="finish"){
            if(itemCount == manager.itemTotal){
                manager.stage++;
                SceneManager.LoadScene(manager.stage);
            }
           
                SceneManager.LoadScene(manager.stage);
        
        }
        else if (other.tag =="bomb"){
            other.gameObject.SetActive(false);
        }
        else if (other.tag =="bigSphere"){
            anim.SetBool("isStart",true);
        }
    }
  

    // Update is called once per frame
    void Update()
    {
        
    }
}
