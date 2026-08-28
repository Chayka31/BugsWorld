using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] Screens;
    public TextMeshProUGUI txtscore;
    public TextMeshProUGUI txtlvl;
    public Sprite[] spriteslvl;
    public Image imgjuk;
    public AudioSource audioSource;

    public List<Stat_visual> st_vis = new List<Stat_visual>();

    public bool tank;

    public AudioSource[] audioSources;
    public AudioSource bugsrap;

    public TextMeshProUGUI tmpro;
    void OnApplicationFocus(bool hasFocus)
    {
        audioSources = FindObjectsOfType<AudioSource>();
        if (!hasFocus)
        {
            foreach (AudioSource a in audioSources)
            {
                if (a != null)
                {
                    a.Pause();
                }
            }
        }
        else
        {
            foreach (AudioSource a in audioSources)
            {
                if (a != null)
                {
                    if (Progress.Instance.ls.press_start_button) 
                    {
                        a.UnPause();
                    }
                    else 
                    {
                        a.Pause();
                    }

                }
            }
        }
    }


    void Start()
    {
        if(Progress.Instance.ls != null) 
        {
            if (!Progress.Instance.ls.firstTry)
            {
                ButtonClick(1);
                SetGraphic();
            }
            if(Progress.Instance.ls.press_start_button == true) 
            {
                bugsrap.Play();
            }
        }
    }

    public void SetGraphicBtn() 
    {
        Progress.Instance.ls = new ListStatss();
        Progress.Instance.ls.press_start_button = true;
        bugsrap.Play();
        SetGraphic();
    }


    // Update is called once per frame
    void Update()
    {
        if (Progress.Instance.ls != null) 
        {
            txtscore.text = "—чЄт: " + Progress.Instance.ls.score.ToString();
            txtlvl.text = "”ровень: " + Progress.Instance.ls.lvl.ToString();
            if (Progress.Instance.ls.lvl != 999)
            {
                imgjuk.sprite = spriteslvl[Progress.Instance.ls.lvl - 1];
            }
            else
            {
                imgjuk.sprite = spriteslvl[5];
            }
        }
    }

    public void ButtonClick(int c) 
    {
        for (int i = 0; i < Screens.Length; i++)
        {
            Screens[i].SetActive(false);
        }
        Screens[c].SetActive(true);

    }

    public void BuyStatUp(int i) 
    {
        if (Progress.Instance.ls.score >= Progress.Instance.ls.Stats[i].Costs[Progress.Instance.ls.Stats[i].current_zna4enie]) 
        {
            if (Progress.Instance.ls.Stats[i].current_zna4enie < st_vis[i].imgs.Length - 1) 
            {
                audioSource.Play();
                Progress.Instance.ls.countups++;
                Progress.Instance.ls.score -= Progress.Instance.ls.Stats[i].Costs[Progress.Instance.ls.Stats[i].current_zna4enie];
                Progress.Instance.ls.Stats[i].current_zna4enie++;
                SetGraphic();
                CheckUps();
                Progress.Instance.MySave();
            }
        }
    }
    public void CheckUps() 
    {
        switch(Progress.Instance.ls.countups) 
        {
            case 10: Progress.Instance.ls.lvl++; break;
            case 20: Progress.Instance.ls.lvl++; break;
            case 27: Progress.Instance.ls.lvl++; break;
            case 35: Progress.Instance.ls.lvl++; break;
            case 40: Progress.Instance.ls.lvl++; break;
        }
    }

    public void ResetScritpableObject()
    {
        foreach (Stat st in Progress.Instance.ls.Stats)
        {
            st.current_zna4enie = 0;
        }
        Progress.Instance.ls.score = 0;
        Progress.Instance.ls.countups = 0;
        Progress.Instance.ls.lvl = 1;
        Progress.Instance.ls.press_start_button = false;
    }
    public void SetGraphic() 
    {
        for (int i = 0; i < Progress.Instance.ls.Stats.Count; i++)
        {
            for (int j = 0; j < Progress.Instance.ls.Stats[i].current_zna4enie + 1; j++)
            {
                st_vis[i].imgs[j].color = Color.black;
            }
            st_vis[i].cost.text = Progress.Instance.ls.Stats[i].Costs[Progress.Instance.ls.Stats[i].current_zna4enie].ToString();
            if (Progress.Instance.ls.Stats[i].zna4enias[Progress.Instance.ls.Stats[i].current_zna4enie] != 0) 
            {
                st_vis[i].zna4enie.text = Progress.Instance.ls.Stats[i].name + " " + Progress.Instance.ls.Stats[i].zna4enias[Progress.Instance.ls.Stats[i].current_zna4enie].ToString();
            }
            else 
            {
                st_vis[i].zna4enie.text = "???";
            }
        }
    }

    public void ResetProgressInMainMenu() 
    {
        Progress.Instance.ResetAllprogress();
    }
    public void Play() 
    {
        Progress.Instance.ls.firstTry = false;
        this.gameObject.SetActive(false);
        Stats_class.lvl = Progress.Instance.ls.lvl;
        Stats_class.damage = Progress.Instance.ls.Stats[0].zna4enias[Progress.Instance.ls.Stats[0].current_zna4enie];
        Stats_class.speed = Progress.Instance.ls.Stats[1].zna4enias[Progress.Instance.ls.Stats[1].current_zna4enie];
        Stats_class.rotationspeed = Progress.Instance.ls.Stats[2].zna4enias[Progress.Instance.ls.Stats[2].current_zna4enie];
        Stats_class.health = Progress.Instance.ls.Stats[3].zna4enias[Progress.Instance.ls.Stats[3].current_zna4enie];

        Progress.Instance.MySave();

        SceneManager.LoadScene(1);
    }

    public void BuyTank() 
    {
        if (Progress.Instance.ls.score >= 1000)
        {
            if (!tank)
            {
                Progress.Instance.ls.score -= 1000;
                tank = true;
                Progress.Instance.ls.lvl = 999;
            }
        }
    }
}


[Serializable]

public class Stat_visual 
{
    public string name;

    public Image[] imgs;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI zna4enie;
}
[Serializable]
public class Stat
{
    public string name;
    public int current_zna4enie;
    public int[] Costs;
    public float[] zna4enias;

    public Stat(string _name, int _current_zna4enie, int[] _costs, float[] _zna4enias) 
    {
        name = _name;
        current_zna4enie = _current_zna4enie;
        Costs = _costs;
        zna4enias = _zna4enias;
    }
}
