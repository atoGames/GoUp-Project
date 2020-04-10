using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    public UIElement uiElement;
    private Game_Manager game_Manager;

    public GameObject m_goLevels;
    public GameObject m_UI_btns;
    public GameObject m_UI_PanelSetting;
    public GameObject m_UI_PanelDie;

    [SerializeField , Header("Animator Panel Setting Open ")]
    private Animator anim_PanelSettingOpen;
    public bool isPanelSettingOpen;
    [SerializeField , Header("Animator UpUILeftSatrt  ")]
    private Animator anim_UpUILeftSatrt;

    private const float AmountChange = 11f;


    void Start()
    {
        isPanelSettingOpen = true;
        game_Manager = FindObjectOfType<Game_Manager>();
        uiElement = FindObjectOfType<UIElement>();
    }

    void Update()
    {
        if (game_Manager.mGame == GameMode.PLAY_MODE)
        {
            m_UI_btns.SetActive(false);

            uiElement.m_txtScore.text = "" + game_Manager.m_Score.ToString();
            uiElement.m_txtFruit.text = "" + game_Manager.m_Fruit.ToString();
        }
        else
            m_UI_btns.SetActive(true);

    }

    public void btnPlay()
    {
        if (game_Manager.mGame == GameMode.MAIN_MENU)
            game_Manager.mGame = GameMode.PLAY_MODE;

        anim_UpUILeftSatrt.SetBool("UpUILeftStart", true);
    }
    public void btnNextLevels()
    {
        game_Manager.NextLevel -= AmountChange;
        game_Manager.m_CourntIndexLevel -= 1;
    }
    public void btnPrevLevels()
    {
        game_Manager.NextLevel += AmountChange;
        game_Manager.m_CourntIndexLevel += 1;
    }
    public void btn_Setting()
    {
        if (isPanelSettingOpen)
        {
            anim_PanelSettingOpen.SetBool("Panel Setting Open", true);
            isPanelSettingOpen = false;
        }
        else
        {
            anim_PanelSettingOpen.SetBool("Panel Setting Open", false);
            isPanelSettingOpen = true;
        }

    }
    public void btn_PlayAgain()
    {
        game_Manager.player.transform.position = game_Manager.m_StartPoint.position;
        game_Manager.player.m_isStartClick = false;
        game_Manager.player.m_Rigidbody2D.isKinematic = false;

        FindObjectOfType<Camera_Follow>().transform.position = game_Manager.ReCamera;

        if (game_Manager.mGame == GameMode.PLAY_MODE)
            game_Manager.mGame = GameMode.MAIN_MENU;

        m_UI_PanelDie.SetActive(false);

    }


    public void btn_AudioOff(bool t)
    {
        game_Manager.isAudioOff = t;

        if (game_Manager.isAudioOff)
            uiElement.ImageAudio.GetComponent<Image>().sprite = uiElement.SpriteAudioOff;
        else
            uiElement.ImageAudio.GetComponent<Image>().sprite = uiElement.SpriteAudioOn;

    }
    public void btn_MusicOff(bool t)
    {
        game_Manager.isMusicOff = t;

        if (game_Manager.isMusicOff)
            uiElement.ImageMusic.GetComponent<Image>().sprite = uiElement.SpriteMusicOff;
        else
            uiElement.ImageMusic.GetComponent<Image>().sprite = uiElement.SpriteMusicOn;
    }

    public void btnQuit() => Application.Quit();
    // game_Manager.SaveMe(); }


}
