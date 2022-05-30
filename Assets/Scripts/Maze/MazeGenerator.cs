using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public CinemachineVirtualCamera vcam;
    public GameObject PlayerObject;
    public Transform Player;
    public Transform Goal;
    public Transform Walls;
    public GameObject HWallTemplate;
    public GameObject VWallTemplate;
    public GameObject FloorTemplate;
    public GameObject PoleTemplate;
    public PhoneMovement phoneMovement;
    public Power power;
    public float MovementSmoothing;
    

    public int Width = 10;
    public int Height = 10;
    public int poleCount = 3;
    public bool[,] HWalls, VWalls;
    public float HoleProbability;
    public int GoalX, GoalY;

    public int PlayerX, PlayerY;

    void Start()
    {
        Time.timeScale = 0f;
    }

    void Update()
    {
    }

    public int Rand(int max)
    {
        return UnityEngine.Random.Range(0, max);
    }
    public float frand()
    {
        return UnityEngine.Random.value;
    }

    public void StartNext()
    {
        Time.timeScale = 1f;
        power.IsOn = true;
        phoneMovement.PhoneIn();
        if (GameObject.Find("Ghost(Clone)") != null)
        {
            Destroy(GameObject.Find("Ghost(Clone)"));
        }
        foreach (Transform child in Walls)
            Destroy(child.gameObject);
        PlayerObject.GetComponentInChildren<Power>().PowerPercentage = 100f;
        (HWalls, VWalls) = GenerateLevel(Width, Height);
        PlayerX = Rand(Width);
        PlayerY = Rand(Height);
        List<List<int>> polePosition= new List<List<int>>();
        int poleNumber = poleCount;
        int[] integers = new int[] {-2,2};
        int randYIndex;
        int randXIndex;
        int randY;
        int randX;
        for(int i = 0; i < poleCount; i++){
            int poleX = UnityEngine.Random.Range(4, Width-4);
            int poleY = UnityEngine.Random.Range(4, Height-4);
            foreach(List<int> item in polePosition){
                if(Math.Abs(item[0] - poleX) < 3 && Math.Abs(item[1] - poleY) < 3 
                ){
                    randYIndex = UnityEngine.Random.Range(0, integers.Length - 1);
                    randXIndex = UnityEngine.Random.Range(0, integers.Length - 1);
                    randY = integers[randYIndex];
                    randX = integers[randXIndex];
                    poleX += randX;
                    poleY += randY;
                }
            }
            if(poleX == GoalX && poleY == GoalY){
                poleX += 1;
                poleY += 1;
            }
            List<int> polexy = new List<int>();
            polexy.Add(poleX);
            polexy.Add(poleY);
            polePosition.Add(polexy);
            
        }
        int minDiff = Mathf.Max(Width, Height) / 2;
        while (true)
        {
            GoalX = Rand(Width-1);
            GoalY = Rand(Height-1);
            if (Mathf.Abs(GoalX - PlayerX) >= minDiff) break;
            if (Mathf.Abs(GoalY - PlayerY) >= minDiff) break;
        }

        for (int x = 0; x < Width+1; x++)
            for (int y = 0; y < Height; y++)
                if (HWalls[x, y]){
                    Instantiate(VWallTemplate, new Vector3(x, y + 0.5f, 0), Quaternion.identity, Walls);
            
                }
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height+1; y++)
                if (VWalls[x, y]){
                    Instantiate(HWallTemplate, new Vector3(x + 0.5f, y, 0), Quaternion.identity, Walls);
                   
                }
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                Instantiate(FloorTemplate, new Vector3(x + 0.5f, y + 0.5f), Quaternion.identity, Walls);
        for(int i = 0; i < polePosition.Count; i++)
            Instantiate(PoleTemplate, new Vector3(polePosition[i][0] + 0.5f, polePosition[i][1] + 0.5f), Quaternion.identity, Walls);
        Player.transform.position = new Vector3(PlayerX + 0.5f, PlayerY + 0.5f);
        Goal.transform.position = new Vector3(GoalX + 0.5f, GoalY + 0.5f);
        //vcam.m_Lens.OrthographicSize = Mathf.Pow(Mathf.Max(Width / 1.5f, Height), 0.70f) * 0.95f;
    }

    public (bool[,], bool[,]) GenerateLevel(int w, int h)
    {
        bool[,] hwalls = new bool[w + 1, h];
        bool[,] vwalls = new bool[w, h + 1];

        bool[,] visited = new bool[w, h];
        bool dfs(int x, int y)
        {
            if (visited[x, y])
                return false;
            visited[x, y] = true;

            var dirs = new[]
            {
                (x - 1, y, hwalls, x, y),
                (x + 1, y, hwalls, x + 1, y),
                (x, y - 1, vwalls, x, y),
                (x, y + 1, vwalls, x, y + 1),
            };

            foreach (var (nx, ny, wall, wx, wy) in dirs.OrderBy(t => frand()))
                wall[wx, wy] = !(0 <= nx && nx < w && 0 <= ny && ny < h && (dfs(nx, ny) || frand() < HoleProbability));

            return true;
        }
        dfs(0, 0);

        return (hwalls, vwalls);
    }
}
