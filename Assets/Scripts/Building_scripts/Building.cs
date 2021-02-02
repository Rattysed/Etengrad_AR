using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Building : MonoBehaviour
{
    //public GameObject main_script_object;
    public Renderer MainRenderer;
    public Vector2Int Size = Vector2Int.one;

    int currentLevel = 0;

    public InterfaceBehaviour inter;

    public int levelsCount;
    //public Canvas canvas;

    //[Min(1)]
    //public int levels = 1;
    [Tooltip("X - money, Y - people, Z - Electro")]
    [Header("Price")]
    public List<Vector3> prices = new List<Vector3>();
    /*
    public float price_money = 1;
    public int price_people = 1;
    public float price_electro = 0.1f;
    */
    
    [Header("LevelModels")]
    public List<GameObject> levelVisuals;
    [Header("Permanent income")]
    public List<Vector3> permanentIncome = new List<Vector3>();

    [Header("Timed income (UnitsPerMinute)")]
    public List<Vector3> timedIncome = new List<Vector3>();
    /*public float timed_money = 0;
    public int timed_people = 0;
    public float timed_electro = 0;*/


    [Header("Score")]
    public AllScoreBehaviour score;

    
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
        //main_game_script = main_script_object.GetComponent<Main>();
        Debug.Log("norma");
        MainRenderer.material.color = new Color(240, 54, 54, 0f);
    }

    /*public void UnsetNormal()
    {
        MainRenderer.GetComponent<BoxCollider>().enabled = false;
    }*/

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
    public void PermIncome(int level)
    {
        Vector3 income = permanentIncome[level];

        score.resourseMoney += income.x;
        score.resoursePeople += (int)income.y;
        score.resourseElectricity += income.z;
        /*main_game_script.money += static_money;
        main_game_script.Electrisity_count += static_electro;
        main_game_script.People_count += static_people;*/
    }

    public IEnumerator TimeIncome()
    {
        yield return new WaitForSeconds(60);
        while (true)
        {
            score.resourseMoney += timedIncome[currentLevel].x;
            //float timed_electro = timedIncome[currentLevel].x;
            score.resoursePeople += (int)timedIncome[currentLevel].y;
            StartCoroutine(routine: inter.ShowMessage("Фабрика сработала"));
            //main_game_script.Electrisity_count += timed_electro;
            

            yield return new WaitForSeconds(60);
        }
    }

    private void SetLevel(int level)
    {
        for (int i = 0; i < levelsCount; ++i)
            levelVisuals[i].SetActive(false);
        levelVisuals[level].SetActive(true);
        StopAllCoroutines();
        StartCoroutine(routine: TimeIncome());
    }

    public void BuyLevel(int level)
    {
        Vector3 price = prices[level];

        score.resourseMoney -= price.x;
        score.resoursePeople -= (int)price.y;
        score.resourseElectricity -= price.z;
        PermIncome(level);
        /*main_game_script.People_count -= price_people;
        main_game_script.Electrisity_count -= price_electro;
        main_game_script.money -= price_money;*/
        SetLevel(level);
    }

    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }
    private void Update()
    {
       
        //Quaternion rotation = Quaternion.FromToRotation(canvas.transform.forward, canvas.transform.position - MainCamera.transform.position);
        //canvas.transform.rotation = canvas.transform.rotation * rotation;

    }
}