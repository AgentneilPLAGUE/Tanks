using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public HighScores m_HighScores;

    public GameObject[] tanks;

    public Text m_MessageText;
    public Text m_TimerText;

    public GameObject m_HighScorePanel;
    public Text m_HighScoresText;

    public Button m_NewGameButton;
    public Button m_HighScoresButton;
    public Button m_ExitButton;

    public Camera TankCamera;
    public Camera DeathCamera;

    private float gameTime = 0;
    public float GameTime
    {
        get
        {
            return gameTime;
        }
    }

    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };

    private GameState gameState;
    public GameState State
    {
        get
        {
            return gameState;
        }
    }

    private void Awake()
    {
        gameState = GameState.Start;
    }

    // Use this for initialization
    void Start()
    {
        TankCamera.enabled = false;
        DeathCamera.enabled = true;

        foreach (GameObject tank in tanks)
        {
            if (tank.tag != "Player")

            tank.SetActive(false);
        }
        m_TimerText.gameObject.SetActive(false);
        m_MessageText.text = "Press ENTER to Start";

        m_HighScorePanel.gameObject.SetActive(false);
        m_NewGameButton.gameObject.SetActive(true);
        m_HighScoresButton.gameObject.SetActive(true);
    }

    public void OnNewGame()
    {
         //To Do: Reset the whole game back to fresh ** Done **
        
         SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reloads current scene, from google. ;) replaced new game button, because of my third person camera
        
         // Obsolete in this instance. I think..

         /* m_NewGameButton.gameObject.SetActive(false);
          m_HighScoresButton.gameObject.SetActive(false);
          m_HighScorePanel.SetActive(false);

          gameTime = 0; gameState = GameState.Playing; m_TimerText.gameObject.SetActive(true);
          m_MessageText.text = "";

          for (int i = 0; i < tanks.Length; i++)
          {
              tanks[i].SetActive(true);
          }*/
    }
    public void OnHighScores()
    {
        m_MessageText.text = "";

        m_HighScoresButton.gameObject.SetActive(false);
        m_HighScorePanel.SetActive(true);

        string text = "";
        for (int i = 0; i < m_HighScores.scores.Length; i++)
        {
            int seconds = m_HighScores.scores[i];
            text += string.Format("{0:D2}:{1:D2}\n", (seconds / 60), (seconds % 60));
        }
        m_HighScoresText.text = text;
    }

    public void OnClickExit()
    {
        // My quit button. for Sam.
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                //  Debug.Log("Start");

                foreach (GameObject tank in tanks)
                {
                    tank.SetActive(false);
                }

                // to do: disable scripts until playing, tank aim, tank movement and bullet  ** Done (Nailed it)**

                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    TankCamera.enabled = true;
                    DeathCamera.enabled = false;

                    m_TimerText.gameObject.SetActive(true);
                    m_MessageText.text = "";
                    gameState = GameState.Playing;

                    foreach (GameObject tank in tanks)
                    {
                        tank.SetActive(true);
                    }
                    m_MessageText.gameObject.SetActive(false);
                    m_TimerText.gameObject.SetActive(true);
                    m_HighScoresButton.gameObject.SetActive(false);
                    m_NewGameButton.gameObject.SetActive(false);
                    m_TimerText.text = "";
                }
                break;

            case GameState.Playing:

                bool isGameOver = false;

                TankCamera.enabled = true;
                DeathCamera.enabled = false;

                gameTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(gameTime);
                m_TimerText.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));

                if (OneTankLeft() == true)
                {
                    isGameOver = true;
                }
                else if (IsPlayerDead() == true)
                {
                    isGameOver = true;
                }

                if (isGameOver == true)
                {
                    gameState = GameState.GameOver;
                    m_TimerText.gameObject.SetActive(false);

                    m_NewGameButton.gameObject.SetActive(true);
                    m_HighScoresButton.gameObject.SetActive(true);

                    if (IsPlayerDead() == false)
                    {
                        TankCamera.enabled = false;
                        DeathCamera.enabled = true;

                        m_MessageText.text = "WINNER!";
                        m_MessageText.gameObject.SetActive(true);

                        m_HighScores.AddScore(Mathf.RoundToInt(GameTime));
                        m_HighScores.SaveScoresToFile();

                        //Causing problems. need to sort out *update* Fixed by Sam! Thanks!
                        foreach (GameObject tank in tanks)
                        {
                            tank.SetActive(false);
                        }
                    }
                    else
                    {
                        TankCamera.enabled = false;
                        DeathCamera.enabled = true;

                        m_MessageText.text = "TRY AGAIN";
                        m_MessageText.gameObject.SetActive(true);

                        foreach (GameObject tank in tanks)
                        {
                            tank.SetActive(false);
                        }
                    }

                }

                break;

            case GameState.GameOver:

                // Not needed

               /* if (Input.GetKeyUp(KeyCode.Return) == true)
                {

                    foreach (GameObject tank in tanks)
                    {
                        tank.SetActive(true);
                    }
                    gameTime = 0;
                    gameState = GameState.Playing;
                    m_MessageText.gameObject.SetActive(false);
                }*/


                break;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private bool OneTankLeft()
    {
        int numberOfTanks = 0;
        foreach (GameObject tank in tanks)
        {
            if (tank.activeSelf == true)
            {
                numberOfTanks++;
            }
        }

        return (numberOfTanks <= 1);
    }

    private bool IsPlayerDead()
    {
        foreach (GameObject tank in tanks)
        {
            if (tank.activeSelf == false)
            {
                if (tank.tag == "Player")
                {
                    return true;
                }
            }
        }
        return false;
    }
    
}