using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditMove : MonoBehaviour
{
    private int index = 0;
    private Transform tr;
    // Start is called before the first frame update
    public GameObject Curve;
    private Vector3 nextPoint;
    void Start()
    {
        tr = GetComponent<Transform>();
        nextPoint = Curve.GetComponent<LineRenderer>().GetPosition(index);
        StartCoroutine(move());
    }

    // Update is called once per frame
    void Update()
    {
        tr.LookAt(nextPoint);
    }
    private IEnumerator move()
    {
        while (true)
        {
            index++;
            if (index >= Curve.GetComponent<LineRenderer>().positionCount - 1)
            {
                index = 0;
            }
            nextPoint = Curve.GetComponent<LineRenderer>().GetPosition(index);
            tr.position = nextPoint;
            yield return new WaitForSeconds(0.03f);
        }
        
    }
}