using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WorldBehaviour : MonoBehaviour {
    private void Start()
    {

    }


    private void Update() {
        if (Input.touchCount == 1)
        {
            Debug.Log(Input.touches[0].position.x.ToString() + " " + Input.touches[0].position.x.ToString());
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
               
            }
        }
    }
}