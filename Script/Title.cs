using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Title : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler,IPointerUpHandler, IPointerClickHandler
{
   void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ClickStart();
        }
    }
   public void OnPointerClick(PointerEventData eventData)
    {
        print("Click");
        if (gameObject.name == "Start")
            ClickStart();
        else if (gameObject.name == "End")
            ClickEnd();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Enter");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        print("Exit");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        print("Up");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        print("Down");
    }
    public void ClickStart()
   {
        SceneManager.LoadScene(2);
   }
    public void ClickEnd()
    {
        Application.Quit();
    }
}
