using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject Count;
    [SerializeField]
    private GameObject Road;
    [SerializeField]
    private GameObject train;
    [SerializeField]
    private GameObject Camera;
    [SerializeField]
    private GameObject Finish;
    [SerializeField]
    private GameObject SendMgr;
    [SerializeField]
    private GameObject MeteorZone;
    [SerializeField]
    private GameObject[] Controller;

    private bool finishCheck;


    [SerializeField]
    private Material BackGroud;
    [SerializeField]
    private Color BackGroundColor;
    [SerializeField]
    private float Exposure;
    [SerializeField]
    private AudioClip secondWarp;
    private AudioSource audio;
    private Animator anim;
    public bool FinishCheck { get => finishCheck; set => finishCheck = value; }
    private float m_WarpSpeed=1.0f;
   

    // Start is called before the first frame update
    void Start()
    {
        FinishCheck = false;
        menu.SetActive(true);
        Count.SetActive(false);
        Road.SetActive(false);
        train.SetActive(false);
        Finish.SetActive(false);
        SendMgr.SetActive(false);
        MeteorZone.SetActive(false);

        foreach (GameObject tmp in Controller)
        {
            tmp.SetActive(true);
        }
        BackGroundColor = Color.gray;
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
    public void GameStart()
    {
        if (!FinishCheck)
        {
            Debug.Log("GameStart!!");
            menu.SetActive(false);
            Count.SetActive(true);
            Road.SetActive(true);
            train.SetActive(true);
            SendMgr.SetActive(true);
            MeteorZone.SetActive(true);
            anim.SetBool("WarpStart", true);
            Camera.transform.SetParent(train.transform);
            foreach (GameObject tmp in Controller)
            {
                tmp.SetActive(false);
            }
        }
        else
        {
            SceneManager.LoadScene(1);
        }

    }
    // Update is called once per frame
    void Update()
    {
        Animator trainAnimator = train.GetComponent<Animator>();
        if (trainAnimator.GetBool("TrainCollision"))
        {
            anim.SetBool("RedOn", true);
            
        }
        if (train.GetComponent<train>().AnimationDone(anim,"BackGroundRed"))
            anim.SetBool("RedOn", false);
        if (MeteorZone.GetComponent<MeteorManager>().BMeteorEnter)
        {
            anim.SetBool("WarpOn", true);
            if(!audio.isPlaying)
                audio.Play();
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99)
            {
                m_WarpSpeed += 0.15f;
                anim.SetFloat("WarpSpeed", m_WarpSpeed);
            }
        }
        else
        {
            anim.SetBool("WarpOn", false);
            anim.SetFloat("WarpSpeed", 1.0f);
            
        }
        SetBackGround();
        if(MeteorZone.GetComponent<MeteorManager>().BMeteorCheck)
        {
            audio.clip = secondWarp;
            foreach (GameObject tmp in Controller)
            {
                tmp.SetActive(true);
            }
        }
        if (MeteorZone.GetComponent<MeteorManager>().BMeteorClear)
        {
            foreach (GameObject tmp in Controller)
            {
                tmp.SetActive(false);
            }
        }
        if (train.GetComponent<train>().BEnd)
        {
            anim.SetBool("RedOn", false);
            if (train.GetComponent<train>().ILife <= 0)
            {
                Finish.GetComponentInChildren<Text>().text = "\n\nGameOver\n\n롤러코스터가 파괴되어 운행이 중단됩니다\n\n" +
                   "다시 시작할려면 패드를 눌러주세요";
                Finish.GetComponent<RectTransform>().localPosition = new Vector3(0,-150,-1000f);
                GameObject.Find("MeteorZone").SetActive(false);
                
            }
            Finish.SetActive(true);
            Road.SetActive(false);
            FinishCheck = true;
            foreach (GameObject tmp in Controller)
            {
                tmp.SetActive(true);
            }
        }
    }
    private void SetBackGround()
    {
        BackGroud.SetColor("_Tint", BackGroundColor);
        BackGroud.SetFloat("_Exposure", Exposure);
        RenderSettings.skybox = BackGroud;
    }
}
