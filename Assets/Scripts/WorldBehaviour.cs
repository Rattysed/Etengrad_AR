using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WorldBehaviour : MonoBehaviour {
    public GameObject MovingObject;
    int GameMode = 0;
    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private IEnumerator Controller(){
        while (true){
            switch (Input.touchCount){
                case 1:
                break;
                case 2:
                break;
            }
            yield return null;
        }
    }
    public IEnumerator OneFingerMode()
    {
        Debug.Log("One finger!");
        float old_x = Input.touches[0].position.x;
        float old_y = Input.touches[0].position.y;
        yield return null;
        while (true)
        {
            if (Input.touchCount != 1)
            {
                yield break;
            }
            RaycastHit hit;
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                
                if (hit.transform.name.IndexOf("UI_block") >= 0)
                {

                }
                else
                {
                    /*if (hit.transform.name.IndexOf("visuals") >= 0)
                    {
                        if (hit.transform.gameObject == choosed_building)
                        {
                            time_to_choose -= Time.deltaTime;
                            if (time_to_choose <= 0)
                            {
                                mode = 5; // Переход в режим управления зданием. 
                                continue;
                            }
                        }
                        else
                        {
                            choosed_building = hit.transform.gameObject;
                            time_to_choose = 2;
                        }

                        
                    }*/
                    //if (hit.transform.gameObject == choosed_building)
                    //{
                     //   continue;
                    //}
                    float x = Input.touches[0].position.x;
                    float y = Input.touches[0].position.y;
                    float delta_x = old_x - x;
                    float delta_y = old_y - y;
                    old_x = x;
                    old_y = y;
                    float scale = MovingObject.transform.localScale.x;
                    float all_x = MovingObject.transform.localPosition.x - delta_x / 2000;
                    float all_y = MovingObject.transform.localPosition.y;
                    float all_z = MovingObject.transform.localPosition.z - delta_y / 2000;
                    if (Mathf.Abs(all_x) > 0.5)
                    {
                        all_x = MovingObject.transform.localPosition.x;
                    }
                    if (Mathf.Abs(all_z) > 0.25)
                    {
                        all_z = MovingObject.transform.localPosition.z;
                    }
                    Vector3 target = new Vector3(all_x, all_y,  all_z);
                    MovingObject.transform.localPosition = target;



                }
            }
            yield return null;
        }
        
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