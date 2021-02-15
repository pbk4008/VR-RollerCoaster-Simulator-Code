using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataSave : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject bc;
    [SerializeField]
    private GameObject dot;
    private BezierCurve bcScript;
    
    private static DataSave instance;
    private int bcSize;
    public static DataSave Instance()
    {
        if(!instance)
        {
            instance = new DataSave();
        }
        return instance;
    }
    private void Save(string filepathIncludingFileName)
    {
        bcScript = bc.GetComponent<BezierCurve>();
        bcSize = bcScript.Point.Count;
        StreamWriter sw = new StreamWriter(filepathIncludingFileName);
        for (int i=0; i<bcSize; i++)
        {
            sw.Write(bcScript.Point[i].position.x);
            sw.Write(",");
            sw.Write(bcScript.Point[i].position.y);
            sw.Write(",");
            sw.WriteLine(bcScript.Point[i].position.z);
        }
        sw.Flush();
        sw.Close();
        Debug.Log("저장 되었습니다.");
    }
    private void Load(string filepathIncludingFileName)
    {
        bcScript = bc.GetComponent<BezierCurve>();
        
        StreamReader sr = File.OpenText(filepathIncludingFileName);
        string input = "";
        int index = 0;
        while(true)
        {
            input = sr.ReadLine();
            if (input == null) { break; }
            float x, y, z;
            string[] word = input.Split(',');
            x = float.Parse(word[0]);
            y = float.Parse(word[1]);
            z = float.Parse(word[2]);
            Vector3 VecCreate = new Vector3(x, y, z);
            GameObject tmpDot = Instantiate(dot, VecCreate,Quaternion.identity);
            bcScript.Point.Add(tmpDot.transform);
            index++;
        }
        sr.Close();
        Debug.Log("로드 되었습니다.");
    }
    void Start()
    {
        Load("CurvePoint.txt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnApplicationQuit()
    {
        Save("CurvePoint.txt");
    }

}
