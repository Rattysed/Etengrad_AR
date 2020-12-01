using UnityEngine;
using System.Collections;

public class BuildingsGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);
    //public GameObject grid_object;
    public GameObject real_grid_object;

    [Header("Scripts")]
    public InterfaceBehaviour inter;
    public AllScoreBehaviour allScore;
    [Space]
    public Building flyingBuilding;
    //public GameObject mainScriptObject;
    //private Main mainScript;
    private Building[,] grid;
    
    private Camera mainCamera;
    private GameObject help_object;

    private bool available = true;
    private Vector2Int place;

    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];
        help_object = new GameObject();
        help_object.transform.SetParent(real_grid_object.transform);
        mainCamera = Camera.main;
       // mainScript = mainScriptObject.GetComponent<Main>();
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        Debug.Log("Хуууууй");
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }
        inter.EnterBuildMenu();
        flyingBuilding = Instantiate(buildingPrefab);
        
        flyingBuilding.inter = inter;
        flyingBuilding.score = allScore;
        //flyingBuilding.GetComponent<Building>().main_script_object = mainScriptObject;
        flyingBuilding.transform.SetParent(real_grid_object.transform);
        flyingBuilding.transform.localScale = new Vector3(1, 1, 1);
        flyingBuilding.transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

    private void Update()
    {
        if (flyingBuilding != null)
        {
            //var a = flyingBuilding.GetComponent<Building>().price_electro;
            //var groundPlane = new Plane(Vector3.up, Vector3.zero);
            RaycastHit hit;
            //Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) 
                && hit.transform.gameObject == real_grid_object)
            {
                //Debug.Log(hit.point.ToString());
                Vector3 worldPosition = hit.point;
                help_object.transform.position = worldPosition;
                worldPosition = help_object.transform.localPosition;
                //Debug.Log(worldPosition.ToString());
                Vector2 cords = CoordsByHit(worldPosition);
                int x = (int)cords.x; int y = (int)cords.y;
                place = new Vector2Int(x, y);
                //int x = Mathf.RoundToInt((worldPosition.x + 0.5f) * GridSize.x);
                // * GridSize.y);




                available = true;
                if (x < 0 || x > GridSize.x - flyingBuilding.Size.x) available = false;
                if (y < 0 || y > GridSize.y - flyingBuilding.Size.y) available = false;

                if (available && IsPlaceTaken(x, y)) available = false;

                flyingBuilding.transform.localPosition = new Vector3(x - 4.5f, 0, y - 4.5f);
                //Debug.Log(x.ToString() + " " + GridSize.x.ToString() + " = " + (x / GridSize.x).ToString() + " " + (y / GridSize.y).ToString());
                flyingBuilding.SetTransparent(available);

                /*if (available && Input.GetMouseButtonDown(0))
                {
                    Debug.Log(x.ToString() + " " + y.ToString());
                    PlaceFlyingBuilding(x, y);
                }*/
            }
        }
    }

    public void CancelPlacing()
    {
        if (!(flyingBuilding == null))
            Destroy(flyingBuilding.gameObject);
        flyingBuilding = null;
        inter.QuitBuildMenu();
    }
    public void AcceptPlacing()
    {
        if (available)
        {
            PlaceFlyingBuilding(place.x, place.y);

            inter.QuitBuildMenu();
        }
    }
    public Vector2 CoordsByHit(Vector3 pos)
    {
        return new Vector2(Mathf.RoundToInt((pos.x + 4.5f)), Mathf.RoundToInt((pos.z + 4.5f)));
    }
    

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                if (grid[placeX + x, placeY + y] != null)
                {
                    Debug.Log("place_fucked");
                    return true;
                }
            }
        }
        Debug.Log("place_free");
        return false;
    }

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                grid[placeX + x, placeY + y] = flyingBuilding;
            }
        }
        
        flyingBuilding.SetNormal();
        flyingBuilding = null;
    }
}
