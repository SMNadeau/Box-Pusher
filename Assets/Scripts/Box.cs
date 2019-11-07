using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    public bool m_OnCircle;
    public bool m_OnHole = false;
    private SpriteRenderer rend;
    private Sprite Box_Hole;

    public void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Box_Hole = Resources.Load<Sprite>("Box_in_Hole");
    }

    public bool Move(Vector2 direction)
    {
        if (BoxBlocked(transform.position, direction))
        {
            return false;
        }
        else
        {
           // lastPosition.writeLastPosition();
            transform.Translate(direction / 4);
            TestForOnHole();
            TestForOnCircle();
            return true;
        }
    }

    bool BoxBlocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPosition = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls)
        {
            if (wall.transform.position.x == newPosition.x && wall.transform.position.y == newPosition.y)
            {
                return true;
            }
        }

        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (var box in boxes)
        {
            if (box.transform.position.x == newPosition.x && box.transform.position.y == newPosition.y)
            {
                return true;
            }
        }

        GameObject[] slides = GameObject.FindGameObjectsWithTag("Slide");
        foreach (var slide in slides)
        {
            if (slide.transform.position.x == newPosition.x && slide.transform.position.y == newPosition.y)
            {
                return true;
            }
        }

        GameObject[] sticks = GameObject.FindGameObjectsWithTag("Stick");
        foreach (var stick in sticks)
        {
            if (stick.transform.position.x == newPosition.x && stick.transform.position.y == newPosition.y)
            {
                return true;
            }
        }

        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
        foreach (var door in doors)
        {
            if (door.transform.position.x == newPosition.x && door.transform.position.y == newPosition.y)
            {
                Door dr = door.GetComponent<Door>();
                if (!(dr.IsOpen()))
                    return true;
            }
        }

        return false;
    }

    void TestForOnCircle()
    {
        GameObject[] circles = GameObject.FindGameObjectsWithTag("Circle");
        foreach (var circle in circles)
        {
            if (transform.position.x == circle.transform.position.x && transform.position.y == circle.transform.position.y)
            {
                GetComponent<SpriteRenderer>().color = Color.grey;
                m_OnCircle = true;
                return;
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        m_OnCircle = false;
    }

    void TestForOnHole()
    {
        GameObject[] holes = GameObject.FindGameObjectsWithTag("Hole");
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");

        foreach (var hole in holes)
        {
            if (transform.position.x == hole.transform.position.x && transform.position.y == hole.transform.position.y)
            {
                foreach (var box in boxes)
                {
                    if (box.transform.position.x == hole.transform.position.x && box.transform.position.y == hole.transform.position.y)
                    {
                        box.tag = "Untagged";                      
                        Object.Destroy(hole);
                        rend.sprite = Box_Hole;
                        m_OnHole = true;
                        rend.sortingOrder = -3;
                    }
                }
                return;
            }
        }
    }
}
