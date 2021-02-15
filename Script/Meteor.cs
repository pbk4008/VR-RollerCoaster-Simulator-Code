using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meteor : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform tr;
    private GameObject Train;
    [SerializeField]
    private float m_fSpeed;
    private Transform Train_tr;
    [SerializeField]
    private Transform Camera_tr;
    private GameObject MeteorCreate;
    private MeteorZone sc_MeteorCreate;
    private Material m_Mat;
    private Color m_Color;
    void Start()
    {
        m_Mat = gameObject.GetComponent<MeshRenderer>().material;
        tr = gameObject.GetComponent<Transform>();
        Train = GameObject.FindWithTag("Train");
        Train_tr = Train.GetComponent<Transform>();
        MeteorCreate = GameObject.Find("MeteorZone");
        sc_MeteorCreate = MeteorCreate.GetComponent<MeteorZone>();
        ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 vec;
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            tr.LookAt(Train_tr.position);
            vec= (Train_tr.position - tr.position).normalized;
            if (Train.GetComponent<train>().BEnd)
                Destroy(gameObject);
        }
        else
        {
            tr.LookAt(Camera_tr.position);
            vec = (Camera_tr.position - tr.position).normalized;
        }
        tr.position += vec * m_fSpeed * Time.deltaTime;
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Train")
        {
            sc_MeteorCreate.IResumMMeteorCount--;
            MeteorCreate.GetComponent<MeteorManager>().DestroySound();
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Stick")
        {
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                if (m_Mat.GetColor("_Color") == col.gameObject.GetComponent<MeshRenderer>().material.GetColor("_TintColor"))
                {
                    sc_MeteorCreate.IResumMMeteorCount--;
                    MeteorCreate.GetComponent<MeteorManager>().DestroySound();
                    Destroy(gameObject);
                }
            }
            else
            {
                HowToPlay tmp = GameObject.Find("Button").GetComponent<HowToPlay>();
                tmp.iCount--;
                Destroy(gameObject);
            }
        }

    }
    private void ChangeColor()
    {
        int itmpColorCode;
        itmpColorCode = Random.Range(0, 3);
        switch(itmpColorCode)
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
        m_Mat.SetColor("_Color", m_Color);
    }
}
