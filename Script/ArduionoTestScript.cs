using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;

public class ArduionoTestScript : MonoBehaviour
{
    [SerializeField]
    private Text textUI;
    [SerializeField]
    private GameObject Terminal;
    private Terminal scTerminal;
    // Start is called before the first frame update
    void Start()
    {
        scTerminal = Terminal.GetComponent<Terminal>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.anyKey)
        {
            scTerminal.SendInput("0");
            textUI.text = "Stop";
        }
        if(Input.GetKey(KeyCode.W))
        {
            scTerminal.SendInput("1");
            textUI.text = "Go";
        }
        if(Input.GetKey(KeyCode.S))
        {
            scTerminal.SendInput("2");
            textUI.text = "Back";
        }
        if (Input.GetKey(KeyCode.A))
        {
            scTerminal.SendInput("3");
            textUI.text = "Left";
        }
        if(Input.GetKey(KeyCode.D))
        {
            scTerminal.SendInput("4");
            textUI.text = "Right";
        }
    }
    /*public void Go()
    {
        if(serialPort.IsOpen)
        {
            serialPort.Write("1");
        }
    }
    public void Stop()
    {
        if (serialPort.IsOpen)
        {
            serialPort.Write("1");
        }
    }*/
}
