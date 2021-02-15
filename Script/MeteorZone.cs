using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorZone : MonoBehaviour
{
    private LineRenderer LR;
    [SerializeField]
    private Transform MeteorZoneStart;
    [SerializeField]
    private Transform MeteorZoneEnd;
    public LineRenderer LR_Getter { get => LR; set => LR = value; }
    public int IResumMMeteorCount { get => m_iResumMMeteorCount; set => m_iResumMMeteorCount = value; }

    [SerializeField]
    private GameObject[] CreateZones = new GameObject[9];
    [SerializeField]
    private GameObject CreateZone;
    private MeteorManager sc_MeteorManager;
    [SerializeField]
    private int m_iMeteorCount=0;
    private int m_iResumMMeteorCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        LR = GetComponent<LineRenderer>();
        LR.positionCount = 50;
        LR.SetPosition(0,MeteorZoneStart.position);
        sc_MeteorManager = gameObject.GetComponent<MeteorManager>();
       
        for(int i =1; i<50; i++)
        {
            Vector3 road = (MeteorZoneEnd.position - MeteorZoneStart.position) * i / 50;
            LR.SetPosition(i,MeteorZoneStart.position+road);
        }
        LR.SetPosition(50, MeteorZoneEnd.position);
        for(int i=0; i<3; i++)
        {
            int k = i - 2;
            for (int j = 0; j < 3; j++)
            {
                int l = j - 2;
                Vector3 tmpVector = new Vector3(MeteorZoneEnd.position.x+(k*10), MeteorZoneEnd.position.y+(l*10), MeteorZoneEnd.position.z);
                tmpVector.y += 20;
                tmpVector.x += 10;
                GameObject zone= Instantiate(CreateZone, tmpVector,Quaternion.identity);
                zone.GetComponent<Transform>().parent = gameObject.transform;
                CreateZones[i+(3*j)] = zone;
                CreateZones[i+(3*j)].SetActive(false);
                
            }
        }
        StartCoroutine(MeteorStart());
    }

    // Update is called once per frame
    void Update()
    { 
        if(m_iMeteorCount>=10&&m_iResumMMeteorCount==0)//슈팅게임 종료
        {
            m_iMeteorCount = 0;
            //sc_MeteorManager.BMeteorCheck = false;
            sc_MeteorManager.BMeteorClear = true;//슈팅게임 종료후 연출 시작
        }    
    }
    private IEnumerator MeteorStart()
    {
        while(true)
        {        
            if (m_iMeteorCount == 10)
              break;
            if(sc_MeteorManager.BMeteorCheck)
            {
                m_iMeteorCount++;
                m_iResumMMeteorCount++;
                CreateZones[Random.RandomRange(0, 9)].SetActive(true);
            }
            yield return new WaitForSeconds(2.0f);
            
        }
    }
}
