using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.EventSystems;

public class StickChange : MonoBehaviour
{
    private Color m_Color;
    private Material m_Mat;
    public int ColorCode=0;
    void Start()
    {
        m_Mat = GetComponent<MeshRenderer>().material;
        m_Color = Color.white;
    }
    // Start is called before the first frame update
    public void ChangeStickColor()
    {
        Debug.Log("Change!!");
        ColorCode++;
        if (ColorCode > 2)
            ColorCode = 0;
        switch(ColorCode)
        {
            case 0:
                m_Color = Color.white;
                break;
            case 1:
                m_Color = Color.blue;
                break;
            case 2:
                m_Color = Color.red;
                break;
        }
        m_Mat.SetColor("_TintColor", m_Color);
    }
}
