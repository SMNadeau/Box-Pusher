/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastPosition : MonoBehaviour
{

    public static void writeLastPosition()
    {
    string playerPath = @"C:\Users\Christian\Desktop\Box Pusher\Assets\Resources\Player_Position.txt";
    string boxPath = @"C:\Users\Christian\Desktop\Box Pusher\Assets\Resources\Box_Position.txt";
    string slidingPath = @"C:\Users\Christian\Desktop\Box Pusher\Assets\Resources\Sliding_Position.txt";
    string stickyPath = @"C:\Users\Christian\Desktop\Box Pusher\Assets\Resources\Sticky_Position.txt";
    string holePath = @"C:\Users\Christian\Desktop\Box Pusher\Assets\Resources\Hole_Position.txt";

    string lastPositionBox = "";
    string lastPositionPlayer = "";
    string lastPositionSliding = "";
    string lastPositionSticky = "";
    string lastPositionHole = "";

    GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    GameObject[] slidings = GameObject.FindGameObjectsWithTag("Stick");
    GameObject[] stickies = GameObject.FindGameObjectsWithTag("Slide");
    GameObject[] holes = GameObject.FindGameObjectsWithTag("Hole");

    foreach(var box in boxes)
    {
        lastPositionBox+= "x: " + box.transform.position.x.ToString() + ", y: " + box.transform.position.y.ToString() + "\n";
    }

    foreach(var player in players)
    {
        lastPositionPlayer+= "x: " + player.transform.position.x.ToString() + ", y: " + player.transform.position.y.ToString() + "\n";
    }
    foreach(var sliding in slidings)
    {
        lastPositionSliding+= "x: " + sliding.transform.position.x.ToString() + ", y: " + sliding.transform.position.y.ToString() + "\n";
    }

    foreach(var sticky in stickies)
    {
        lastPositionSticky+= "x: " + sticky.transform.position.x.ToString() + ", y: " + sticky.transform.position.y.ToString() + "\n";
    }

    foreach(var hole in holes)
    {
        lastPositionHole+= "x: " + hole.transform.position.x.ToString() + ", y: " + hole.transform.position.y.ToString() + "\n";
    }

    System.IO.File.WriteAllText(boxPath, lastPositionBox);
    System.IO.File.WriteAllText(playerPath, lastPositionPlayer);
    System.IO.File.WriteAllText(slidingPath, lastPositionSliding);
    System.IO.File.WriteAllText(stickyPath, lastPositionSticky);
    System.IO.File.WriteAllText(holePath, lastPositionHole);
    }
}
*/