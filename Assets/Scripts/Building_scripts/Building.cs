using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{
    public GameObject main_script_object;
    public Renderer MainRenderer;
    public Vector2Int Size = Vector2Int.one;
    public Canvas canvas;
   
    //public GameObject render_cube;
    [Header("Price")]
    public float price_money = 1;
    public float price_electro = 0.1f;
    public int price_people = 1;
    [Header("Permanent income")]
    public float static_money = 0;
    public float static_electro = 0;
    public int static_people = 0;
    [Header("Timed income (UnitsPerMinute)")]
    public float timed_money = 0;
    public float timed_electro = 0;
    public int timed_people = 0;

    private Main main_game_script;
    private Camera MainCamera;

    private void Awake()
    {
        MainCamera = Camera.main;
    }

    public void SetTransparent(bool available)
    {
        if (available)
        {
            MainRenderer.material.color = new Color(0.07f, 0.84f, 0.07f, 0.5f);
        }
        else
        {
            MainRenderer.material.color = new Color(0.94f, 0.21f, 0.21f, 0.5f);
        }
    }

    public void SetNormal()
    {
        main_game_script = main_script_object.GetComponent<Main>();
        Debug.Log("norma");
        MainRenderer.material.color = new Color(240, 54, 54, 0f);
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }
    public void BuildIncome(GameObject main_script_object)
    {
        main_game_script.money += static_money;
        main_game_script.Electrisity_count += static_electro;
        main_game_script.People_count += static_people;
    }

    public IEnumerator TimeIncome()
    {
        
        while (true)
        {
            main_game_script.money += timed_money;
            main_game_script.Electrisity_count += timed_electro;
            main_game_script.People_count += timed_people;

            yield return new WaitForSeconds(60);
        }
    }

    public void BuyBuilding()
    {
        main_game_script.People_count -= price_people;
        main_game_script.Electrisity_count -= price_electro;
        main_game_script.money -= price_money;
    }

    private void Update()
    {
        Quaternion rotation = Quaternion.FromToRotation(canvas.transform.forward, canvas.transform.position - MainCamera.transform.position);
        canvas.transform.rotation = canvas.transform.rotation * rotation;

    }
}