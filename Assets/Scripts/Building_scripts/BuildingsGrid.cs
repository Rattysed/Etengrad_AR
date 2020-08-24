using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);
    //public GameObject grid_object;
    public GameObject real_grid_object;
    private Building[,] grid;
    private Building flyingBuilding;
    private Camera mainCamera;
    private GameObject help_object;
    
    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];
        help_object = new GameObject();
        help_object.transform.SetParent(real_grid_object.transform);
        mainCamera = Camera.main;
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }
        
        flyingBuilding = Instantiate(buildingPrefab);
        flyingBuilding.transform.SetParent(real_grid_object.transform);
        flyingBuilding.transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

    private void Update()
    {
        if (flyingBuilding != null)
        {
            //var a = flyingBuilding.GetComponent<Building>().price_electro;
            //var groundPlane = new Plane(Vector3.up, Vector3.zero);
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) 
                && hit.transform.gameObject == real_grid_object 
                && hit.transform.name.IndexOf("UI_block") < 0)
            {
                //Debug.Log(hit.point.ToString());
                Vector3 worldPosition = hit.point;
                help_object.transform.position = worldPosition;
                worldPosition = help_object.transform.localPosition;
                //Debug.Log(worldPosition.ToString());


                //int x = Mathf.RoundToInt((worldPosition.x + 0.5f) * GridSize.x);
                // * GridSize.y);
                int x = Mathf.RoundToInt((worldPosition.x + 4.5f));
                int y = Mathf.RoundToInt((worldPosition.z + 4.5f));
                //Debug.Log(x.ToString() + " " + y.ToString());

                bool available = true;

                if (x < 0 || x > GridSize.x - flyingBuilding.Size.x) available = false;
                if (y < 0 || y > GridSize.y - flyingBuilding.Size.y) available = false;

                if (available && IsPlaceTaken(x, y)) available = false;

                flyingBuilding.transform.localPosition = new Vector3(x - 4.5f, 0, y - 4.5f);
                //Debug.Log(x.ToString() + " " + GridSize.x.ToString() + " = " + (x / GridSize.x).ToString() + " " + (y / GridSize.y).ToString());
                flyingBuilding.SetTransparent(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    Debug.Log(x.ToString() + " " + y.ToString());
                    PlaceFlyingBuilding(x, y);
                }
            }
        }
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                if (grid[placeX + x, placeY + y] != null) return true;
            }
        }

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
