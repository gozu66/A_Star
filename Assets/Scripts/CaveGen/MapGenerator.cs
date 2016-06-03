using UnityEngine;
using System.Collections;
using System;

public class MapGenerator : MonoBehaviour
{
    public int width, height;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;

    int[,] map;

    public int smoothIterations;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GenerateMap();
        }
    }

    void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();

        for(int i = 0; i < smoothIterations; i++)
        {
            SmoothMap();
        }

        MeshGenerator meshGen = GetComponent<MeshGenerator>();
        meshGen.GenereateMesh(map, 1);
    }

    void RandomFillMap()
    {
        if(useRandomSeed)
        {
            seed = Time.time.ToString();
        }

        System.Random prng = new System.Random(seed.GetHashCode());

        print(Time.time);
        print(prng.Next(0, 100));

        for(int x =  0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (prng.Next(0, 100) < randomFillPercent) ? 0 : 1;
                }

            }
        }
    }

    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x, y] = 0;
            }
        }
    }
    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;

        for(int x = gridX - 1; x <= gridX + 1; x++)
        {
            for(int y = gridY - 1; y <= gridY + 1; y++)
            {
                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    if (x != gridX || y != gridY)
                    {
                        wallCount += map[x, y];
                    }
                }
                else
                {
                    wallCount++;
                }

            }
        }
        return wallCount;
    }
/*
    void OnDrawGizmos()
    {

        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Gizmos.color = map[x, y] != 0 ? Color.black : Color.white;
                    Vector3 pos = new Vector3(-width / 2 + x + 0.5f, 0, -height / 2 + y + 0.5f);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }
 */
}
