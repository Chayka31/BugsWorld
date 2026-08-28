using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class Manager : MonoBehaviour
{
    public int Score;
    public TextMeshProUGUI txtscore;
    public TextMeshProUGUI txtscoreonramka;
    public bool scoreisdoubled;
    public GameObject player;
    public GameObject TableExit;
    public GameObject buttonDouble;
    public bool marker;
    public GameObject cameras;
    public AudioSource audios;
    public AudioSource[] audioSources;

    void Start()
    {
         scoreisdoubled = false;
         player = GameObject.FindGameObjectWithTag("Player");
         audioSources = FindObjectsOfType<AudioSource>();
    }

    void OnApplicationFocus(bool hasFocus)
    {
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
                    a.UnPause();
                }
            }
        }
    }

    public void showAdd() 
    {
        YandexGame.RewVideoShow(0);
    }
    public void DoubleScore() 
    {
        if (!scoreisdoubled) 
        {
            Score *= 2;
            scoreisdoubled = true;
            buttonDouble.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        txtscore.text = "Ñ÷¸ò: " + Score;
        txtscoreonramka.text = "Âàø ñ÷¸ò: " + Score;
        if(player == null) 
        {
            if (marker == false)
            {
                GameOver();
                marker = true;
            }
        }
    }

    public void GameOver()
    {
        TableExit.GetComponent<Animator>().Play("ramka_start", 0);
        cameras.GetComponent<AudioSource>().mute = true;
        cameras.AddComponent<AudioListener>();
        audios.Play();
        juk_spawner[] sp = FindObjectsOfType<juk_spawner>();
        foreach (juk_spawner item in sp)
        {
            item.max_count_in_zone = 0;
        }
    }
    public void ToMainMenu() 
    {
       
        Progress.Instance.ls.score += Score;
        Progress.Instance.MySave();
        SceneManager.LoadScene(0);


    }
}
