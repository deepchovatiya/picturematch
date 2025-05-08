using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class levels : MonoBehaviour
{
    public Button []allbtn;
    int levelno = 1,maxlevel=0;
    public Sprite tick;
    void Start()
    {
        levelno = PlayerPrefs.GetInt("levelno",1);
        maxlevel = PlayerPrefs.GetInt("maxlevel",0);
        for(int i=0;i<=maxlevel;i++)
        {
            allbtn[i].interactable = true;
            if(i<maxlevel)
            {
                allbtn[i].GetComponent<Image>().sprite=tick;
            }
        }
    }

    void Update()
    {
        
    }
    public void levelnos(int levelno)
    {
        PlayerPrefs.SetInt("levelno",levelno);
        SceneManager.LoadScene("play");
    }
    public void btnback()
    {
        SceneManager.LoadScene("home");
    }
}
