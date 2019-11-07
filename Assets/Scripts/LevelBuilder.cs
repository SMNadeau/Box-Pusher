using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LevelElement
{
    public string m_Character;
    public GameObject m_Prefab;
    
}

public class LevelBuilder : MonoBehaviour
{
    public int m_CurrentLevel;
    public List<LevelElement> m_LevelElements;
    private Level m_Level;
    Door[] doors = new Door[5];
    Switch[] switches = new Switch[5];

    GameObject GetPrefab(char c)
    {
        LevelElement levelElement = m_LevelElements.Find(le => le.m_Character == c.ToString());
        if (levelElement != null)
            return levelElement.m_Prefab;
        else
            return null;
    }

    public void NextLevel()
    {
        m_CurrentLevel++;
        if (m_CurrentLevel >= GetComponent<Levels>().m_Levels.Count)
        {
            m_CurrentLevel = 0; //go back to first level
        }
    }

    public void PreviousLevel()
    {
        m_CurrentLevel--;

    }

    public void Build()
    {
        m_Level = GetComponent<Levels>().m_Levels[m_CurrentLevel];

        int startx = -m_Level.Width / 2;
        int x = startx;
        int y = -m_Level.Height / 2;

        foreach (var row in m_Level.m_Rows)
        {
            //Keeps track of which parts of the map are within the bounds of the walls
            bool firstWall = false;
            foreach (var ch in row)
            {
                Debug.Log(ch);
                GameObject prefab = GetPrefab(ch);
                if (prefab)
                {
                    //keeps tiles from filling before a wall prefab occurs
                    if (prefab.CompareTag("Wall"))
                        firstWall = true;
                    //keeps tiles from filling after a wall prefab occurs
                    if (prefab.CompareTag("BG") && firstWall)
                    {
                        firstWall = false;
                    }
                    Debug.Log(prefab.name);
                    if (prefab.CompareTag("Door"))

                    {
                        int num = ToInt(ch);

                        doors[num] = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity).GetComponent<Door>();

                        Debug.Log("Door added to array " + num);

                    }

                    else if (prefab.CompareTag("Switch"))

                    {

                        int num2 = ToInt(ch) - 5;

                        switches[num2] = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity).GetComponent<Switch>();

                        Debug.Log("Switch added to array " + num2);

                    }

                    else

                        Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                if (x < ((m_Level.Width / 2) - 1) && firstWall)
                {
                    Instantiate(GetPrefab('b'), new Vector3(x, y, 0), Quaternion.identity);
                }
                x++;
            }
            y++;
            x = startx;

        }

        GameObject[] hasDoors = GameObject.FindGameObjectsWithTag("Door");
        int count = 0;
        foreach (var dr in hasDoors)
            count++;


        for (int i = 0; i < count; i++)
        {
            Debug.Log("Reached Door Assignment:" + i);
            doors[i].AssignSwitch(switches[i]);
            if (doors[i].HasSwitch())
                Debug.Log("Successful assignment");
            else
                Debug.Log("Unsuccessful assignment");
            //Simple solution to multiple doors and switches. Needs changing later so switches can be
            //set to specific doors.
        }
     
    }
        private int ToInt(char ch)
    {
        return (int)(ch - '0');
    }
}
