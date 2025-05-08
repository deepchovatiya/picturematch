using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class homepage : MonoBehaviour
{
    int vol = 0;
    public GameObject a;
    public Sprite mute,unmute;
    public Slider voi;
    new AudioSource audio;
    public GameObject pnal;
    void Start()
    {
        pnal.SetActive(false);
        audio=GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("music",1);
    }
    public void btnchnagevolume()
    {
        voi.value=audio.volume;
        PlayerPrefs.SetFloat("music",audio.volume);
    }
    void Update()
    {
        
    }
    public void btntimeline(int select)
    {
        PlayerPrefs.SetInt("select",select);
        SceneManager.LoadScene("level");
    }
    
    public void btnsound()
    {
        vol++;
        if(vol%2==0)
        {
            PlayerPrefs.SetInt("vol",vol);
            print("sound "+vol);
            a.GetComponent<Image>().sprite = unmute;
        }
        else if(vol%2==1) 
        {
            PlayerPrefs.SetInt("vol",vol);
            print("mute"+vol);
            a.GetComponent<Image>().sprite = mute;

        }
    }
    public void onp()
    {
        pnal.SetActive(true);
    }
    public void offp()
    {
        pnal.SetActive(false);
    }
}
