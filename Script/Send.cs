using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Send : MonoBehaviour
{
    private Transform Train;
    [SerializeField]
    private GameObject Terminal;
    private Terminal scTerminal;
    private GameManager GM;
    private MeteorManager sc_MeteorMgr;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        Train = GameObject.FindWithTag("Train").GetComponent<Transform>();
        Terminal = GameObject.Find("Terminal");
        sc_MeteorMgr = GameObject.Find("MeteorZone").GetComponent<MeteorManager>();
        scTerminal = Terminal.GetComponent<Terminal>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GM.FinishCheck)
        {
            float xtmp,ytmp,ztmp;
            xtmp = Mathf.Round(Train.localRotation.x * 10) * 0.1f;
            ytmp = Mathf.Round(Train.localRotation.y * 10) * 0.1f;
            ztmp = Mathf.Round(Train.rotation.z * 10) * 0.1f;

            xtmp *= -1;
            if (sc_MeteorMgr.BMeteorCheck)
            {
                scTerminal.SendInput("0.0");
                scTerminal.SendInput(ztmp.ToString());
            }
            else
            {
                scTerminal.SendInput(xtmp.ToString());
                scTerminal.SendInput(ytmp.ToString());
            }
        }
    }
}
