using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class train : MonoBehaviour
{
    [SerializeField]
    private float m_fSpeed;
    [SerializeField]
    private bool m_bStart;
    private bool m_bEnd;
    private float m_fMaxSpeed;
    private Vector3 nextPoint;
    private GameObject Curve;
    private Transform tr;
    private float duration=0.0f;
    private int index = 1;
    private AudioSource audio;
    private int SaveIndex=0;
    private MeteorZone sc_MeteorZone;
    private MeteorManager sc_MeteorManager;
    private BoxCollider boxCollider;
    private Animator anim;
    private bool m_bCollisionOverlap;
    private int m_iLife=3;
    public bool BStart { get => m_bStart; set => m_bStart = value; }
    public bool BEnd { get => m_bEnd; set => m_bEnd = value; }
    public int ILife { get => m_iLife; set => m_iLife = value; }

    // Start is called before the first frame update
    void Start()
    {
        m_bEnd = false;
        m_bStart = false;
        m_bCollisionOverlap = false;
        m_fMaxSpeed = 0.03f;
        audio = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        audio.Stop();
        Curve = GameObject.Find("Bezier");
        sc_MeteorZone = GameObject.Find("MeteorZone").GetComponent<MeteorZone>();
        sc_MeteorManager = GameObject.Find("MeteorZone").GetComponent<MeteorManager>();
        nextPoint = Curve.GetComponent<LineRenderer>().GetPosition(index);
        tr = gameObject.GetComponent<Transform>();
        StartCoroutine(move());

        
    }

    // Update is called once per frame
    void Update()
    {
        tr.LookAt(nextPoint);
        
        if(m_bStart)
        {
            if (!audio.isPlaying)
                audio.Play();
        }
        else
        {
            if (m_bEnd)
                audio.Stop();
        }
        if (sc_MeteorManager.BMeteorCheck)
        {
            boxCollider.enabled = true;
        }
        else
        {
            boxCollider.enabled = false;
            anim.enabled = false;
        }
        if(AnimationDone(anim,"trainCollision")&&m_bCollisionOverlap)
        {
            anim.SetBool("TrainCollision", false);
            m_bCollisionOverlap = false;
        }
       if(m_iLife<=0)
        {
            m_bEnd = true;
            m_bStart = false;
           
        }

    }
    private IEnumerator move()
    {
        while(true)
        { 
            if (m_bStart)
            { 
                index++;
                tr.position = nextPoint;
                if (!sc_MeteorManager.BMeteorCheck)
                {
                    if (SaveIndex != 0)
                    {
                        index = SaveIndex;
                        SaveIndex = 0;
                    }
                    if (index >= Curve.GetComponent<LineRenderer>().positionCount - 1)
                    {
                        index = Curve.GetComponent<LineRenderer>().positionCount - 1;
                        audio.Stop();
                        m_bEnd = true;
                    }
                    if (index >= Curve.GetComponent<LineRenderer>().positionCount - 80)
                    {
                        m_fSpeed += 0.001f;
                        audio.volume -= 0.01f;

                    }
                    else
                        m_fSpeed -= 0.0005f;
                    if (m_fSpeed <= m_fMaxSpeed)
                        m_fSpeed = 0.03f;
                    nextPoint = Curve.GetComponent<LineRenderer>().GetPosition(index);
                }
                else
                {

                    if (SaveIndex == 0)
                    { 
                        SaveIndex = index;
                        index = 0;
                    }
                    if (!sc_MeteorManager.BMeteorEnter)//슈팅게임 시작 시 멈춤 아니면 이동
                    {
                        if (index >= 5)
                            index = 5;
                        nextPoint = sc_MeteorZone.LR_Getter.GetPosition(index);
                    }
                    if (index > sc_MeteorZone.LR_Getter.positionCount - 1)//추가 컨텐츠 부분 연출 후 완전 종료
                    {
                        sc_MeteorManager.BMeteorCheck = false;
                        sc_MeteorManager.BMeteorEnter = false;
                    }                 
                }
                
            }
            yield return new WaitForSeconds(m_fSpeed);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            if(!m_bCollisionOverlap)
            {
                ILife--;
                anim.enabled = true;
                anim.SetBool("TrainCollision", true);
                m_bCollisionOverlap = true;
            }
        }
    }
    public bool AnimationDone(Animator argAnim,string AnimName)
    {
        return argAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99
            && argAnim.GetCurrentAnimatorStateInfo(0).IsName(AnimName);
    }

}

