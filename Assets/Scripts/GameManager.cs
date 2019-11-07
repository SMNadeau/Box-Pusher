using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LevelBuilder m_LevelBuilder;
    public GameObject m_NextButton;
    private static bool m_ReadyForInput;
    private Player[] m_Player;
    private bool next_level = false;
    public static bool canMove = true;

    private void Start()
    {
        m_NextButton.SetActive(false);
        ResetScene();
    }

    Vector2 temp = new Vector2(0, 0);
    public int frame = 0;
    

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            PreviousLevel();
        }
        if (canMove)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveInput.Normalize();

            if (frame == 0)
            {
                temp = moveInput;
                Door.player_finished = false;
            }
            
            if (temp.sqrMagnitude > 0.5)
            {
                if (m_ReadyForInput)
                {
                    //performs movement action for each player object that exists
                    for (int j = 0; j < m_Player.Length; j++)
                    {
                        m_Player[j].Move(temp);
                    }
                    System.Threading.Thread.Sleep(30);
                    frame++;

                    //player moves 1/4 of a tile every frame for 4 frames
                    if (frame == 4)
                    {
                        Door.player_finished = true;
                        m_ReadyForInput = false;
                        temp = new Vector2(0, 0);
                        m_NextButton.SetActive(IsLevelComplete());
                    }
                }
                else
                {
                    m_ReadyForInput = true;
                    frame = 0;
                }
            }
            else
            {
                m_ReadyForInput = true;
                frame = 0;
            }
        } 

    }

    public static bool NotMoving()
    {
        return m_ReadyForInput;
    }

    public void PreviousLevel()
    {
        m_LevelBuilder.PreviousLevel();

        StartCoroutine(ResetSceneASync());
    }

    public void NextLevel()
        {
            m_NextButton.SetActive(false);
            m_LevelBuilder.NextLevel();
            
           StartCoroutine(ResetSceneASync());
        }

        public void ResetScene()
        {
            StartCoroutine(ResetSceneASync());
        }

    //checks if each type of box is on a circle, if so, the level is complete
    bool IsLevelComplete()
    {
        Box[] boxes = FindObjectsOfType<Box>();
        SlideBox[] slides = FindObjectsOfType<SlideBox>();
        StickyBox[] sticks = FindObjectsOfType<StickyBox>();
        foreach (var box in boxes)
        {
            if (!box.m_OnCircle && !box.m_OnHole) return false;
        }
        foreach (var slide in slides)
        {
            if (!slide.m_OnCircle) return false;
        }
        foreach (var stick in sticks)
        {
            if (!stick.m_OnCircle) return false;
        }

        return true;
    }

    IEnumerator ResetSceneASync()
    {
        if (SceneManager.sceneCount > 1)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("LevelScene");
            while (!asyncUnload.isDone)
            {
                yield return null;
                Debug.Log("Unloading...");
            }
            Debug.Log("Unload Done");
            Resources.UnloadUnusedAssets();
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
            Debug.Log("Loading...");
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));
        m_LevelBuilder.Build();
        m_Player = FindObjectsOfType<Player>();
        Debug.Log("Level loaded");
    }
}
