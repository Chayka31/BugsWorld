using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

[Serializable]
public class ListStatss
{
    public int countups;
    public int lvl;
    public bool firstTry;
    public int score;
    public List<Stat> Stats = new List<Stat>();
    public bool press_start_button;

    public ListStatss() 
    {
        countups = 0;
        lvl = 1;
        firstTry = true;
        score = 0;
        int[] ints = new int[10]; 
        ints[0] = 5;
        ints[1] = 14;
        ints[2] = 25;
        ints[3] = 40;
        ints[4] = 60;
        ints[5] = 76;
        ints[6] = 85;
        ints[7] = 92;
        ints[8] = 99;
        ints[9] = 115;
        float[] floats = new float[10];
        floats[0] = 10;
        floats[1] = 18;
        floats[2] = 25;
        floats[3] = 38;
        floats[4] = 50;
        floats[5] = 70;
        floats[6] = 90;
        floats[7] = 120;
        floats[8] = 135;
        floats[9] = 150;
        Stats.Add(new Stat("Урон", 0, ints , floats));
        int[] intss = new int[10];
        intss[0] = 7;
        intss[1] = 15;
        intss[2] = 20;
        intss[3] = 26;
        intss[4] = 32;
        intss[5] = 40;
        intss[6] = 45;
        intss[7] = 50;
        intss[8] = 60;
        intss[9] = 80;
        float[] floatss = new float[10];
        floatss[0] = 9;
        floatss[1] = 10;
        floatss[2] = 11;
        floatss[3] = 12;
        floatss[4] = 14;
        floatss[5] = 17;
        floatss[6] = 19;
        floatss[7] = 22;
        floatss[8] = 25;
        floatss[9] = 29;
        Stats.Add(new Stat("Скорость", 0, intss, floatss));
        int[] intsss = new int[10];
        intsss[0] = 1;
        intsss[1] = 4;
        intsss[2] = 7;
        intsss[3] = 10;
        intsss[4] = 17;
        intsss[5] = 19;
        intsss[6] = 25;
        intsss[7] = 27;
        intsss[8] = 30;
        intsss[9] = 31;
        float[] floatsss = new float[10];
        floatsss[0] = 300;
        floatsss[1] = 400;
        floatsss[2] = 450;
        floatsss[3] = 500;
        floatsss[4] = 600;
        floatsss[5] = 666;
        floatsss[6] = 800;
        floatsss[7] = 900;
        floatsss[8] = 1000;
        floatsss[9] = 1111;
        Stats.Add(new Stat("Скорость поворота", 0, intsss, floatsss));
        int[] intssss = new int[10];
        intssss[0] = 3;
        intssss[1] = 6;
        intssss[2] = 10;
        intssss[3] = 17;
        intssss[4] = 22;
        intssss[5] = 26;
        intssss[6] = 30;
        intssss[7] = 36;
        intssss[8] = 40;
        intssss[9] = 50;
        float[] floatssss = new float[10];
        floatssss[0] = 50;
        floatssss[1] = 65;
        floatssss[2] = 80;
        floatssss[3] = 100;
        floatssss[4] = 125;
        floatssss[5] = 155;
        floatssss[6] = 178;
        floatssss[7] = 190;
        floatssss[8] = 250;
        floatssss[9] = 300;
        Stats.Add(new Stat("Здоровье", 0, intssss, floatssss));
        int[] intsssss = new int[1];
        intsssss[0] = 1000;
        float[] floatsssss = new float[1];
        floatsssss[0] = 0;
        Stats.Add(new Stat("???", 0, intsssss, floatsssss));
    }
}

public class Progress : MonoBehaviour
{
    public ListStatss ls;

    public static Progress Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Debug.Log("Instance == null");
        }
        else
        {
            Destroy(gameObject);
        }

        if(YandexGame.SDKEnabled == true) 
        {
            LoadSaveCloud();
        }
    }

    public void LoadSaveCloud() 
    {
        ls = YandexGame.savesData.ls_save;
    }

    public void ResetAllprogress() 
    {
        ls = new ListStatss();
        MySave();
        SceneManager.LoadScene(0);
    }

    private void OnEnable() => YandexGame.GetDataEvent += LoadSaveCloud;
    private void OnDisable() => YandexGame.GetDataEvent -= LoadSaveCloud;
    public void MySave() 
    {
        YandexGame.savesData.ls_save = ls;
        YandexGame.SaveProgress();
    }
}
