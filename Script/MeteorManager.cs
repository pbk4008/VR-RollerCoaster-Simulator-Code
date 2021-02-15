using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour
{
    private float timercheck = 0.0f;
    [SerializeField]
    private GameObject Train;
    private train sc_Train;
    private bool m_bMeteorCheck;
    private bool m_bMeteorEnter;
    private bool m_bMeteorClear;
    private AudioSource audio;

    public bool BMeteorCheck { get => m_bMeteorCheck; set => m_bMeteorCheck = value; }
    public bool BMeteorEnter { get => m_bMeteorEnter; set => m_bMeteorEnter = value; }
    public bool BMeteorClear { get => m_bMeteorClear; set => m_bMeteorClear = value; }

    // Start is called before the first frame update
    void Start()
    {
        sc_Train = Train.GetComponent<train>();
        m_bMeteorCheck = false;
        m_bMeteorEnter = false;
        m_bMeteorClear = false;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sc_Train.BStart)
        {
            timercheck += Time.deltaTime;

            if (timercheck >= 39f&&!m_bMeteorClear)//슈팅 게임 시작
            {
                m_bMeteorCheck = true;              
            }
            if (timercheck >= 30.0f && !m_bMeteorCheck&&!m_bMeteorClear)//슈팅게임 입장전 연출
                m_bMeteorEnter = true;
            else if (m_bMeteorClear&&m_bMeteorCheck)
            {
                m_bMeteorEnter = true;

            }
            else
                m_bMeteorEnter = false;
        }
        else
        {
            timercheck = 0.0f;
            m_bMeteorCheck = false;
        }
    }
    public void DestroySound()
    {
        audio.Play();
    }
}
