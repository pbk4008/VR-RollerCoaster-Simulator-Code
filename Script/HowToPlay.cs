using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class HowToPlay : MonoBehaviour, IPointerClickHandler
{
    public int iCount = 1;
    private int SceneNum = 0;
    [SerializeField]
    private Text howToPlaytxt1;
    [SerializeField]
    private Text howToPlaytxt2;
    [SerializeField]
    private Text howToPlaytxt3;
    [SerializeField]
    private Text howToPlaytxt4;
    [SerializeField]
    private Text howToPlaytxt5;
    [SerializeField]
    private GameObject Stick1;
    [SerializeField]
    private GameObject Stick2;
    [SerializeField]
    private GameObject Obstacle;

    // Start is called before the first frame update
    void Start()
    {
        Stick1.SetActive(false);
        Stick2.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneNum++;
        ChangeHowToPlay(SceneNum);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneNum++;
            if (SceneNum == 5)
                iCount = 0;
            ChangeHowToPlay(SceneNum);
        }
    }
    private void ChangeHowToPlay(int argNum)
    {
        switch(argNum)
        {
            case 1:
                howToPlaytxt1.gameObject.SetActive(false);
                howToPlaytxt2.gameObject.SetActive(true);
                break;
            case 2:
                Stick1.SetActive(true);
                Stick2.SetActive(true);
                howToPlaytxt2.gameObject.SetActive(false);
                howToPlaytxt3.gameObject.SetActive(true);
                break;
            case 3:
                howToPlaytxt2.gameObject.SetActive(false);
                howToPlaytxt3.gameObject.SetActive(true);
                break;
            case 4:
                howToPlaytxt3.gameObject.SetActive(false);
                howToPlaytxt4.gameObject.SetActive(true);
                Obstacle.SetActive(true);
                break;
            case 5:
                if (iCount == 0)
                {
                    howToPlaytxt4.gameObject.SetActive(false);
                    howToPlaytxt5.gameObject.SetActive(true);
                }
                else
                    SceneNum = 4;
                break;
            case 6:
                SceneManager.LoadScene(3);
                break;
        }
    }
    
}
