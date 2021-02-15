using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject train;
    private TextMeshProUGUI tmpro;
    private train trainScript;
    float currentTime;
    
    void Start()
    {
        trainScript = train.GetComponent<train>();
        tmpro = gameObject.GetComponent<TextMeshProUGUI>();
        currentTime = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -=Time.deltaTime;
        int tmpTime=(int)currentTime;
        tmpro.SetText(tmpTime.ToString());
        if (currentTime <= 0)
        {
            gameObject.SetActive(false);
            trainScript.BStart = true;
        }
    }
}
