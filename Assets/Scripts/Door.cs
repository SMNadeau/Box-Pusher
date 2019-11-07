using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Door class. Handles logic for switch/door combo.
 * Each door is assigned a switch. This is done in the LevelBuilder Build() method.
 * Currently Update() checks for changes in logic, which would indicate a box has 
 * been moved to/from a switch. 
 * When a door is opened/closed Update() switches the sprite of the Door to reflect
 * its current condition.
 * 
 * 
 */
public class Door : MonoBehaviour
{

    public static bool player_finished = true;
    Switch m_Switch;
    SpriteRenderer m_SpriteRenderer;
    Sprite m_Open;
    bool m_JustOpened, m_IsClosed;
    int frameCount = 0;
    Sprite[] doorAnim = new Sprite[5];

    // Start is called before the first frame update
    public void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Open = Resources.Load<Sprite>("OpenDoor");
        doorAnim[0] = Resources.Load<Sprite>("Door0");
        doorAnim[1] = Resources.Load<Sprite>("Door1");
        doorAnim[2] = Resources.Load<Sprite>("Door2");
        doorAnim[3] = Resources.Load<Sprite>("Door3");
        doorAnim[4] = Resources.Load<Sprite>("Door4");
        m_JustOpened = false;
        m_IsClosed = true;
    }

    /** Assigns a Switch GameObject to the Door GameObject calling this method
     *  (Used in LevelBuilder)
     */
    public void AssignSwitch(Switch sw)
    {
        m_Switch = sw;
    }

    /** Returns whether or not a Switch has been assigned to the Door
     *  (Used in LevelBuilder to make sure only one Switch is assigned to a Door)
     */
    public bool HasSwitch()
    {
        return m_Switch != null;
    }

    /** returns True when there is a Player or Box occupying the Switch assigned to the Door */
    public bool IsOpen()
    {
        return m_Switch.BoxOnSwitch() || m_Switch.PlayerOnSwitch();
    }

    /** Checks whether there is a player or box on the same tile as a door. */
    bool IsOccupied()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (var box in boxes)
        {
            if (transform.position.x == box.transform.position.x && transform.position.y == box.transform.position.y)
            {
                return true;
            }
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (transform.position.x == player.transform.position.x && transform.position.y == player.transform.position.y)
            return true;
        return false;
    }

    /**
     *
     * Opens/Closes door based on boolean values. IsOpen and IsClosed both being true indicates the player or a box
     * have been moved over the switch, and the door is opened. When they are both false then there is nothing on the
     * switch and nothing on the door tile(when it is occupied).
     * 
     */

    void Update()
    {
        if (GameManager.NotMoving())
        {
            if (IsOpen() && m_IsClosed)
            {
                if (frameCount < 5)
                {
                    m_SpriteRenderer.sprite = doorAnim[frameCount];
                    frameCount++;
                }
                else
                {
                    m_SpriteRenderer.sprite = m_Open;
                    m_IsClosed = false;
                }
            }
            else if (!(IsOpen()) && !(m_IsClosed) && !(IsOccupied()))
            {
                if (frameCount > 4)
                    frameCount = 4;
                if (frameCount > 0)
                {
                    m_SpriteRenderer.sprite = doorAnim[frameCount];
                    frameCount--;
                }
                else
                {
                    m_SpriteRenderer.sprite = doorAnim[frameCount];
                    m_IsClosed = true;
                }
            }
        }
        if (m_Switch.PlayerOnSwitch())
        {
            if (frameCount < 5)
            {
                m_SpriteRenderer.sprite = doorAnim[frameCount];
                frameCount++;
            }
            else
            {
                m_SpriteRenderer.sprite = m_Open;
                m_IsClosed = false;
            }
        }
    }
}
