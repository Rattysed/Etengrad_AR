using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WorldBehaviour : MonoBehaviour {
    public GameObject MovingObject;
    public GameObject buildingsGridObject;

    BuildingsGrid buildingsGrid;
    int GameMode = 0;
    private Camera mainCamera;

    Resolution res;
    
    private void Start()
    {
        res = Screen.resolutions[0];
        buildingsGrid = buildingsGridObject.GetComponent<BuildingsGrid>();
        mainCamera = Camera.main;
        StartCoroutine(routine: Controller());
    }

    private IEnumerator Controller(){
        while (true){
            if (Input.GetMouseButtonDown(0))
                yield return StartCoroutine(routine: OneFingerMode());
            switch (Input.touchCount){
                case 1:
                    yield return StartCoroutine(routine: OneFingerMode());
                    break;
                case 2:
                break;
            }
            yield return null;
        }
    }
    public IEnumerator OneFingerMode()
    {
        Debug.Log("YES");
        int width = res.width;
        int height = res.height;
        Transform choosedBuilding;
        Debug.Log("One finger!");
        //Vector2 oldPos = Input.touches[0].position;
        Vector2 oldPos = Input.mousePosition;
        yield return null;
        //while (Input.touchCount == 1 || Input.GetMouseButtonDown(0))
        while (!Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            int layerMaskBuildings = 1 << 8;
            int layerMask = ~layerMaskBuildings;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.transform.name.IndexOf("UI") < 0)
                {
                    
                    //buildingsGrid.flyingBuilding = 
                    
                }

            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMaskBuildings))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.name.IndexOf("HitBox") >= 0)
                {
                    choosedBuilding = hit.transform.parent;
                    Debug.Log(choosedBuilding.name);
                   // buildingsGrid.flyingBuilding = choosedBuilding.GetComponent<Building>();
                }
            }
            yield return null;
        }
        
    }



    private void Update() {
        
        /*if (Input.touchCount == 1)
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
        }*/
    }
}