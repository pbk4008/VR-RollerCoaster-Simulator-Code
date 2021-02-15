using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject Meteor;
    void OnEnable()
    {
        Instantiate(Meteor, gameObject.GetComponent<Transform>().position, Quaternion.identity); ;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
