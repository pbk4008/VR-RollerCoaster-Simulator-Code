using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class finish : MonoBehaviour, IPointerClickHandler
{
    private GameManager GM;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GM.GameStart();
        }
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
        Transform tr = transform.parent;
        tr.position = Camera.main.transform.position + new Vector3(0,0,100);
    }

    // Update is called once per frame
}
