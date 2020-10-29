using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EngineScript : MonoBehaviour
{
    public GameObject Line;
    private int index;
    private int PointsCount = 0;
    public int level = 0;
    public int PressPointIndex = 1;
    private Camera cam;
    public TextAsset jsonFile;
    public GameObject Point;
    public Levels levelData;
    public List<GameObject> Points;
    public List<GameObject> Lines;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        levelData = JsonUtility.FromJson<Levels>(jsonFile.ToString());
        SpawnPoints();
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void SpawnPoints()
    {
        index = 1;
        for (int i = 0; i < levelData.levels[level].level_data.Length; i += 2)
        {
            TextMeshPro mtext = Point.GetComponentInChildren<TextMeshPro>();
            mtext.SetText(index.ToString());
            Point.name = index.ToString();
            double xco = levelData.levels[level].level_data[i] * 0.0154 - 7.7;
            double yco = (levelData.levels[level].level_data[i+1] * 0.0154 - 7.7) * (-1);
            Point.transform.position = new Vector3((float)xco, (float)yco, 0);
            
            index++;
            Points.Add(Instantiate(Point));
            
            PointsCount++;
        }
    }

    public void DrawLine()
    {
        if (PressPointIndex != 1)
        {
            Vector3 start = GameObject.Find((PressPointIndex - 1).ToString() + "(Clone)").transform.position;
            Vector3 end = GameObject.Find((PressPointIndex).ToString() + "(Clone)").transform.position;

            DrawBetweenPoints(start, end);
        }
        if (PointsCount <= PressPointIndex)
        {

            Vector3 start = GameObject.Find(PressPointIndex.ToString() + "(Clone)").transform.position;
            Vector3 end = GameObject.Find("1(Clone)").transform.position;
            PressPointIndex++;
            DrawBetweenPoints(end, start);
            StartCoroutine(NextLevel());
        }

    }

    IEnumerator NextLevel()
    {
        yield return new WaitWhile(() => !Input.GetKeyDown(KeyCode.Space));
        PointsCount = 0;
        level++;
        PressPointIndex = 1;
        foreach (var point in Points)
        {
            Destroy(point);
        }
        foreach (var line in Lines)
        {
            Destroy(line);
        }
        Points.Clear();
        Lines.Clear();

        SpawnPoints();



    }

    public void DrawBetweenPoints(Vector3 start, Vector3 end)
    {
        SpriteRenderer spriterend = Line.GetComponent<SpriteRenderer>();

        Line.transform.position = start;
        Line.name = "Line" + PressPointIndex.ToString();
        float zRot = Vector3.Angle(Vector3.up, start - end);
        if (end.x - start.x < 0.0f)
            zRot *= -1.0f;
        Line.transform.localEulerAngles = new Vector3(0f, 0f, zRot - 180);

        float Distance = Vector3.Distance(start, end);

        spriterend.drawMode = SpriteDrawMode.Tiled;
        spriterend.size = new Vector2(0.5f, Distance);

        
        Lines.Add(Instantiate(Line));
   

    }
}

[Serializable]
public class Level
{
    public int[] level_data;
}

[Serializable]
public class Levels
{
    public Level[] levels;
}
