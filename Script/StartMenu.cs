using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class StartMenu : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    private GameManager GM;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GM.GameStart();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Enter");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        print("Click");
        GM.GameStart();
    }
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


}
