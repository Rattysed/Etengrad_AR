using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WorldBehaviour : MonoBehaviour {
    public GameObject MovingObject;
    public GameObject buildingsGridObject;
    public Text message;
    public GameObject target;
    BuildingsGrid buildingsGrid;
    int GameMode = 0;
    private Camera mainCamera;

    GameObject bulshit;

    Resolution res;

    private void Start()
    {
        //res = Screen.resolutions[0];
        //buildingsGrid = buildingsGridObject.GetComponent<BuildingsGrid>();
        mainCamera = Camera.main;
        StartCoroutine(routine: Controller());
        bulshit = new GameObject();
        Instantiate(bulshit);
        bulshit.transform.SetParent(target.transform);
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private IEnumerator Controller(){
        while (true){
            /*if (Input.GetMouseButtonDown(0))
                yield return StartCoroutine(routine: OneFingerMode());*/               //Это для теста на компе
            switch (Input.touchCount){
                case 0:
                    message.text = "0 taps";
                    break;
                case 1:
                    message.text = "1 tap";
                    yield return StartCoroutine(routine: OneFingerMode());
                    break;
                case 2:
                    message.text = "2 taps";
                    yield return StartCoroutine(routine: TwoFingersMode());
                    break;
            }
            yield return null;
        }
    }
    public IEnumerator OneFingerMode()
    {
        Debug.Log("YES");
        //int width = res.width;
        //int height = res.height;
        Transform choosedBuilding;
        Debug.Log("One finger!");
        //Vector2 oldPos = Input.touches[0].position;
        //Vector2 oldPos = Input.mousePosition;
        Vector3 oldPos = Vector3.zero;
        yield return null;
        while (Input.touchCount == 1 && !IsMouseOverUI())
        //while (!Input.GetMouseButtonUp(0) && !IsMouseOverUI()) // Это тоже, необходимо будет закомментить строку выше
        {
            RaycastHit hit;
            
            int layerMaskBuildings = (1 << 8) | 5;
            int layerMaskStable = (1 << 9) | 5;
            int layerMask = ~(layerMaskBuildings | layerMaskStable) | 5;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            int moveMask = 1 << 10;
            
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, moveMask)/* &&
                !Graphic.Raycast(Input.mousePosition, mainCamera)*/)
            {
                if (hit.transform.name.IndexOf("UI") < 0)
                {
                    Vector3 pos = hit.point;
                    bulshit.transform.position = pos;
                    pos = bulshit.transform.localPosition;
                    pos.y = 0;
                    if (oldPos != Vector3.zero)
                    {

                        MovingObject.transform.localPosition += pos - oldPos;
                    }
                    oldPos = pos;
                    //Debug.Log(pos);
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
    
    public IEnumerator TwoFingersMode()
    {
        Vector2 oldPos1 = Input.touches[0].position, oldPos2 = Input.touches[1].position;
        float oldDist = Vector2.Distance(oldPos1, oldPos2);
        float oldAngle = Mathf.Atan2((oldPos2.y - oldPos1.y) / oldDist,
            (oldPos2.x - oldPos1.x) / oldDist) * Mathf.Rad2Deg;
        yield return null;
        while(Input.touchCount == 2)
        {
            Vector2 Pos1 = Input.touches[0].position, Pos2 = Input.touches[1].position;
            float Dist = Vector2.Distance(Pos1, Pos2);
            float Angle = Mathf.Atan2((Pos2.y - Pos1.y) / Dist,
                (Pos2.x - Pos1.x) / Dist) * Mathf.Rad2Deg;

            float deltaScale = Dist / oldDist;
            float newScale = target.transform.localScale.x * deltaScale;
            if (0.5f < newScale && newScale < 4) target.transform.localScale = new Vector3(newScale, newScale, newScale);
            target.transform.Rotate(0, oldAngle - Angle, 0);
            oldPos1 = Pos1; oldPos2 = Pos2; oldDist = Dist; oldAngle = Angle;
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