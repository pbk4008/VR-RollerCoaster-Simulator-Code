using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    private LineRenderer lR;
    [SerializeField]
    private List<Transform> point;
    [SerializeField]
    private GameObject plusPoint;
    private Vector3[,] positions = new Vector3[200,50];
    private int iCount = 1;
    private int numPoint = 50;

    public List<Transform> Point { get => point; set => point = value; }//점들을 보관하는 리스트

    // Start is called before the first frame update
    void Start()
    {
        lR = GetComponent<LineRenderer>();
        lR.positionCount = numPoint;
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddPoint();
        }
    }
    private void DrawLine()
    {
            for (int i = 0; i < Point.Count; i+=3)
            {
                for (int j = 1; j <= 50; j++)
                {
                    float t = j / 50.0f;
                    positions[i/3, j - 1] = CalculBezier(t, i);
                }
                if (Point.Count <= 3)
                {
                    break;
                }
                if (i + 4 >= Point.Count)
                {  
                    break;
                }
                iCount= Point.Count / 4 + 1;
                numPoint = 50*iCount;
            }
        lR.positionCount = numPoint;

        Vector3[] resPoint = new Vector3[numPoint];
        for (int i = 0; i < iCount; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                resPoint[50 * i + j] = positions[i, j];
            }
        }
        lR.SetPositions(resPoint);
    }
    private Vector3 CalculBezier(float t, int index)
    {
        //1차 : (1-t)p0+tp1
        //2차 : (1-t)^2p0+2t(1-t)p1+t^2p2
        //3차 : (1-t)^3p0+3t(1-t)^2p1+3t^2(1-t)p2+t^3p3
        //0,1,2,3    4,5,6,7    8,9,10,11   12,13,14,15   16,17,18,19
        float u = 1 - t;
        float uu = u*u;
        float uuu = u * u * u;
        float tt = t * t;
        float ttt = t * t;
        Vector3 p0 = Point[index].position;
        
        Vector3 p1 = Point[index + 1].position;
        
        if (index + 2 >= Point.Count)
        {
            //1차 공식 (1-t)p0+tp1
            return p0 + t * p1;
        }
        Vector3 p2 = Point[index + 2].position;
        if (index + 3 >= Point.Count)
        {
            //2차 공식 (1-t)^2p0+2t(1-t)p1+t^2p2
            return uu * p0 + 2 * t * u * p1 + tt * p2; ;
        }
        Vector3 p3 = Point[index + 3].position;
        //3차공식 (1-t)^3p0+3t(1-t)^2p1+3t^2(1-t)p2+t^3p3

        Vector3 res = Vector3.zero;
        res += uuu * p0;
        res += 3 * t * uu * p1;
        res += 3 * tt * u * p2;
        res += ttt * p3;
        return res;
    }
    private void AddPoint()
    {
        GameObject AddDot = Instantiate(plusPoint, Point[Point.Count-1].position+Vector3.forward, Quaternion.identity, null);
        Point.Add(AddDot.transform);
    }
}