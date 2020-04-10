using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum GameMode
{
    PLAY_MODE,
    MAIN_MENU

}
public class Game_Manager : MonoBehaviour
{

    public static Game_Manager Instance;
    [HideInInspector]
    public UIManager uiManager;
    [HideInInspector]
    public Player player;


    public GameMode mGame = GameMode.MAIN_MENU;

    [HideInInspector]
    public int m_Score;
    [HideInInspector]
    public int m_Fruit;
    public int m_IndexLevel = 0;
    public int m_CourntIndexLevel = 0;
    public int m_PosLastLevel = 11;

    public AudioSource m_Mousic;

    public bool isAudioOff = false;
    public bool isMusicOff = false;

    [HideInInspector]
    public float NextLevel;
    public float m_SpeedLevel = 6f;

    public Transform m_StartPoint;

    [HideInInspector]
    public Vector3 ReCamera; // = new Vector3(0, -1, -10);


    void Awake()
    {
        ReCamera = new Vector3(0, -0.6f, -10);
        Instance = this;
        uiManager = GetComponent<UIManager>();
        player = FindObjectOfType<Player>();

        m_Mousic.Play();

      //  LoadMe();
    }

    void Update()
    {

        NextLevel = Mathf.Clamp(NextLevel, -m_PosLastLevel, 0);
        LevelSelect();

        m_Mousic.mute = isMusicOff;


    }
    private void LevelSelect()
    {
        var posLevels = uiManager.m_goLevels.transform.position;
        posLevels.x = Mathf.Lerp(posLevels.x, NextLevel, m_SpeedLevel * Time.deltaTime);
        posLevels.y = 0;
        posLevels.z = 0;

        uiManager.m_goLevels.transform.position = posLevels;
    }

    //Call This if Player Quit
    public void SaveMe()
    {
        PlayerPrefs.SetInt("_Score",  m_Score);
        PlayerPrefs.SetInt("_Fruit", m_Fruit);
        PlayerPrefs.SetInt("_IndexLevel", m_IndexLevel);
       
    }
        // 
    public void LoadMe()
    {
        m_Score = PlayerPrefs.GetInt("_Score");
        m_Fruit = PlayerPrefs.GetInt("_Fruit");

        m_IndexLevel = PlayerPrefs.GetInt("_IndexLevel");

    }
}
