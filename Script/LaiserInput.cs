using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class LaiserInput : MonoBehaviour
{
    private GameObject currentObject;
    private int currentID;

    // Start is called before the first frame update
    void Start()
    {
        currentObject = null;
        currentID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f);
        Debug.Log(hits.Length);
        for (int i=0; i<hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            int id = hit.collider.gameObject.GetInstanceID();
            
            if (id != currentID)
            {
                currentID = id;
                currentObject = hit.collider.gameObject;
                Debug.Log(currentObject.name);
            }
        }
    }
}
