using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePresenter : MonoBehaviour
{
    public GameObject wall;
    public GameObject exit;
    public GameObject Player;
    public GameObject Zombie;
    public GameObject Droppeditem;
    void Start()
    {
        var maze = new MazeGenerator(new PrimsMazeGenerator()).Generate(20, 20 , 0.12f);
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (!maze[i, j] || i == 0 || j == 0)
                {
                    Instantiate(wall, new Vector3(wall.transform.localScale.x * i, wall.transform.localScale.y * j, 0), Quaternion.identity);
                }
            }
        }
        Vector2 charPos;
        Vector2 exitPos;
        do
        {
            charPos = GetPos(maze);
            exitPos = GetPos(maze);
        } while (Mathf.Sqrt(Mathf.Pow(charPos.x - exitPos.x, 2) + Mathf.Pow(charPos.y - exitPos.y , 2)) < 10);

        Instantiate(exit, exitPos * exit.transform.localScale, Quaternion.identity);
        Player.transform.position = charPos * wall.transform.localScale;
        AstarPath.active.Scan();
        for (int i = 0; i < 10; i++)
        {
            Instantiate(Zombie, GetPos(maze) * wall.transform.localScale, Quaternion.identity);
        }
        for (int i = 0; i < 36; i++)
        {
            Instantiate(Droppeditem, GetPos(maze) * wall.transform.localScale, Quaternion.identity);
        }
    }
    Vector2 GetPos(bool[,] maze)
    {
        while (true)
        {
            int x = Random.Range(1, maze.GetLength(0));
            int y = Random.Range(1, maze.GetLength(1));
            if (maze[x, y])
            {
                return new Vector2(x, y);
            }
        }
    }

}
