using UnityEngine;

public class WorldBehaviour : MonoBehaviour {
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