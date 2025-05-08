using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class game : MonoBehaviour
{
    public Sprite[] allimg;
    List<int> firstlist = new List<int>();
    List<int> lasttlist = new List<int>();
    public GameObject prefab, downholder;
    int singlelist = 12, pairlist = 0;
    int firststore = -1, secondstore = -1;
    bool firstguess = false, secondguess = false;
    GameObject a, b;
    int vol = 0, cnt = 0, time = 20;
    new AudioSource audio;
    public AudioClip clip1;
    public GameObject panels, panellos;
    public Text l;
    GridLayoutGroup g;
    int levelno = 1;
    int[,] gridsize = { { 150, 150 }, { 150, 150 }, { 150, 150 },{ 140,140},{140,140 },{ 120,120},{ 120,120},{ 120,120},{ 120,120} };
    
    int[] gridbox = { 12,14,16,18,20,22,24,26,28,30};
    int[] cntt = { 6,7,8,9,10,11,12,13,14,15};
    int select;
    public GameObject tex;
    public Text levelbox;
    int maxlevel;
    void Start()
    {
        maxlevel = PlayerPrefs.GetInt("maxlevel",0);
        select = PlayerPrefs.GetInt("select",0);
        levelno = PlayerPrefs.GetInt("levelno",1);
        levelbox.text = "Levelno "+levelno;
        g=downholder.GetComponent<GridLayoutGroup>();
        int cellsizex = gridsize[levelno - 1, 0];
        int celsizeY=gridsize[levelno - 1, 1];
        g.cellSize = new Vector2(celsizeY, celsizeY);
        g.spacing = new Vector2(10, 10);
        tex.SetActive(false);

        if (select==0)
        {
            StartCoroutine(timeline());
            tex.SetActive(true);
        }
        audio = GetComponent<AudioSource>();
        vol = PlayerPrefs.GetInt("vol",0);
        if(vol%2==0)
        {
            audio.clip = clip1;
            audio.Play();
        }
        else
        {
            audio.volume=0;
        }
        singlelist = gridbox[levelno - 1];
        pairlist = singlelist / 2;
        for (int i = 0; i < pairlist; i++)
        {
            int r = Random.Range(0, allimg.Length);
            while (firstlist.Contains(r))
            {
                r = Random.Range(0, allimg.Length);
            }
            firstlist.Add(r);
            print(" " + r);
        }
        lasttlist.AddRange(firstlist);
        lasttlist.AddRange(firstlist);

        shufflelists();
        generatebtn();
    }
    
    void shufflelists()
    {
        for (int i = 0; i < lasttlist.Count; i++)
        {
            int r = Random.Range(0, lasttlist.Count);
            int temp = lasttlist[r];
            lasttlist[r] = lasttlist[i];
            lasttlist[i] = temp;
        }
    }
    
    void generatebtn()
    {
        for (int i = 0; i < lasttlist.Count; i++)
        {
            GameObject g1 = Instantiate(prefab, downholder.transform);
            int imgno = lasttlist[i];
            StartCoroutine(closeimg(g1,imgno));
            StartCoroutine(notclick(g1, imgno));
        }
    }
    IEnumerator closeimg(GameObject g1, int imgno)
    {
        g1.GetComponent<Image>().sprite = allimg[imgno];
        yield return new WaitForSeconds(3f);
        g1.GetComponent<Image>().sprite = default;
    }
    IEnumerator notclick(GameObject g1, int imgno)
    {
        yield return new WaitForSeconds(3f);
        g1.GetComponent<Button>().onClick.AddListener(() => onclickbtn(g1, imgno));

    }
   
    void onclickbtn(GameObject g1, int imgno)
    {
        audio.clip = clip1;
        audio.Play();
        g1.GetComponent<Image>().sprite = allimg[imgno];
        print(imgno);
        if (!firstguess)
        {
            firstguess = true;
            firststore = imgno;
            a = g1;
            a.GetComponent<Button>().interactable = false;
        }
        else if (!secondguess)
        {
            secondguess = true;
            secondstore = imgno;
            b = g1;
            b.GetComponent<Button>().interactable = false;

            if (firststore == secondstore)
            {
                print("PICTURE MATCH");
                firstguess = false;
                secondguess = false;
                firststore = -1;
                secondstore = -1;
                cnt++;
                print("cnt ="+cnt);
                time += 2;
                if (cnt == cntt[levelno-1])
                {
                    print("picture complete");
                    PlayerPrefs.SetInt("levelno", levelno + 1);
                    if(levelno>maxlevel)
                    {
                        PlayerPrefs.SetInt("maxlevel",levelno);
                    }
                    levelbox.text = "Levelno " + levelno;
                    panels.SetActive(true);
                }
            }
            else
            {
                print("PICTURE is'nt match");
                firstguess = false;
                secondguess = false;
                firststore = -1;
                secondstore = -1;
                StartCoroutine(btntime(a, b));
                a.GetComponent<Button>().interactable = true;
                b.GetComponent<Button>().interactable = true;

            }

        }
           

    }
    IEnumerator btntime(GameObject a1, GameObject b1)
    {
        yield return new WaitForSeconds(0.5f);
        a1.GetComponent<Image>().sprite = default;
        b1.GetComponent<Image>().sprite = default;
    }
    public void btnchangescene()
    {
        SceneManager.LoadScene("home");
    }
    public void btnpanel()
    {
        SceneManager.LoadScene("play");
        panels.SetActive(false);
    }
    IEnumerator timeline()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);

            if (cnt != cntt[levelno-1])
            {
                time = time - 1;
                l.text = "TIME " + time;
                if (time == 0)
                {
                    panellos.SetActive(true);
                }
            }
            else
            {
                time = time - 0;
            }
            
        }
           
    }
    public void btnpanellos()
    {
        SceneManager.LoadScene("play");
    }
    void Update()
    {

    }
}
/*first image open //imgno=1
        * store in an array
        * second image open//imgno=2
        * store in array
        * then compare first two image
        * if(img is true)
        * {
        *      imgunlock
        * }
        * else
        * {
        *      imglock
        * }
        * then loop is continue and pick image 3 and 4//
        */