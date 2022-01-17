using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int itemTotal; 
    public int currentItemConut;
    public int stage;
    public Text totalCountText;
    public Text currentCountText;

    // Start is called before the first frame update
    void Awake()
    {
        totalCountText.text = "/ " + itemTotal;
    }

    // Update is called once per frame
    public void GetItem(int itemCount)
    {
        currentCountText.text =  itemCount.ToString();
    }
    public void ExitGame(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
            
    }

    public void RetryGame(){
         #if UNITY_EDITOR
            Application.LoadLevel(Application.loadedLevel);
        #else
            Application.LoadLevel(Application.loadedLevel);
        #endif
    }
}
