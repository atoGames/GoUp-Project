using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float m_Speed = 1.5f;
    [SerializeField]
    private float m_MoveUp = 3f;

    public bool m_isStartClick = false;

    public Vector3 m_StratPosition;

    //  Follow the Finger
    private Vector3 m_Direction;

    private Vector2 m_ClampPlayer;

    public Rigidbody2D m_Rigidbody2D;

    public AudioSource[] m_Eff;


    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

    }
    void Update()
    {

        // Player can Only play the game if the situation changes to PLAY
        if (Game_Manager.Instance.mGame == GameMode.PLAY_MODE)
        {
            keyBordInput(Input.mousePosition);
            touchInput();

            // Clamp Player
            ClampPlayer();
            if (m_isStartClick)
            {
                // Do not make physics affect the player
                m_Rigidbody2D.isKinematic = true;

                // Move Player
                transform.Translate(new Vector2(m_Direction.x * m_Speed , m_MoveUp) * Time.deltaTime);
            }
            else
                // Let the Player fall --- 
                m_Rigidbody2D.isKinematic = false;
        }
    }

    private void ClampPlayer()
    {

        // Take The Size of the Player  &  Divided by 4  --- ( 1 / 4 ) = 0.25f
        var localScalePlayer = transform.localScale.x / 4;
        // Take The Screen View From The Camera --- Only Width 
        var ClampPlayerX = Camera.main.orthographicSize * Screen.width / Screen.height;

        // Clamp Width --- X
        m_ClampPlayer = new Vector2(Mathf.Clamp(transform.position.x, -ClampPlayerX + localScalePlayer, ClampPlayerX - localScalePlayer), 0f);
        // Leave The Value Of Y  --- The Player Moves Up ,  Does Not stop Him!!!
        m_ClampPlayer.y = transform.position.y;

        // Apply The Changes
        transform.position = m_ClampPlayer;

    }

    void touchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            // if The Player Clicks On The Screen
            if (t.phase == TouchPhase.Began)
            {
                // Take The site First Click On The Screen
                m_StratPosition = Camera.main.ScreenToWorldPoint(t.position);
                m_isStartClick = true;
            }
            // If The Player Moved a Finger From The Last Location On The Screen
            // and still pressed
            if (t.phase == TouchPhase.Moved && m_isStartClick)
                // Do This : Track Where it is Moving From The Last Point 
                m_Direction = Camera.main.ScreenToWorldPoint(t.position) - m_StratPosition;
            // If The Player lifted a finger from The Screen
            if (t.phase == TouchPhase.Ended)
                m_isStartClick = false;
        }
    }

    void keyBordInput(Vector3 posScreen)
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_StratPosition = Camera.main.ScreenToWorldPoint(posScreen);
            m_isStartClick = true;
        }
        if (Input.GetMouseButton(0) && m_isStartClick)
            m_Direction = Camera.main.ScreenToWorldPoint(posScreen) - m_StratPosition;

        if (Input.GetMouseButtonUp(0))
            m_isStartClick = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            Debug.Log("Fruit");
            Game_Manager.Instance.m_Fruit += 1;

            if(!Game_Manager.Instance.isAudioOff)
               m_Eff[1].Play();

            Destroy(other.gameObject);
        }
        if (other.CompareTag("Score"))
        {
            Debug.Log("Score");
            Game_Manager.Instance.m_Score += 1;

            if (!Game_Manager.Instance.isAudioOff)
                m_Eff[0].Play();

            Destroy(other.gameObject);

        }
        if (other.CompareTag("Block"))
        {
            Debug.Log("Block");
            m_isStartClick = false;

            if (!Game_Manager.Instance.isAudioOff)
                m_Eff[2].Play();
        }
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy");
            Destroy(other.gameObject);
        }
        if (other.CompareTag("EndLevel"))
        {
            Debug.Log("EndLevel");
            Game_Manager.Instance.NextLevel += -11;
            transform.position = Game_Manager.Instance.m_StartPoint.position;
            m_isStartClick = false;

            m_Rigidbody2D.isKinematic = false;

           FindObjectOfType<Camera_Follow>().transform.position = Game_Manager.Instance.ReCamera;
        }

        if (other.CompareTag("Die"))
            Die();

    }



    public void Die()
    {
        Game_Manager.Instance.uiManager.m_UI_PanelDie.SetActive(true);
    }
}