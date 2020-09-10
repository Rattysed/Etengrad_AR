using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



public class Main : MonoBehaviour
{
    public List<GameObject> upgradeCubes;
    public GameObject what; // саммонящийся объект
    public int mode = 0; // режим города
    /* 1. - режим строительства дорог
     * 2. - режим сноса дорог
     * 3. - режим постройки зданий
     * 4. - 
     * 5. - режим перемещения зданий
     */
    public float money = 2;
    float timer = 0;
    

    private Camera mainCamera;

    public GameObject dropper;
    public Dropdown bildings;
    public GameObject Button;


    public Text MoneyText;



    public Text PeopleText;
    public Text ElectrisityText;
    public int People_count = 2;
    public float Electrisity_count;
    float msg_time = 2;
    public int old_number = 0;
    public List<List<float>> prices = new List<List<float>>();

    bool showing_message = false;
    public Text message;


    private List<GameObject> things;

    public GameObject choosed_building;


    public GameObject every_fucking_thing;

    public Dictionary<GameObject, RoadData> roadDict = new Dictionary<GameObject, RoadData>(); // Постоянный словарь с дорогами
    public Dictionary<GameObject, RoadData> tempRoadDict; // Временный словарь с дорогами(если юзер начнёт строить и перехочет)

    List<BuildingsData> buildings_list = new List<BuildingsData>();
    // Start is called before the first frame update
    void Start()
    {
        
        made_buttons();
        //what = things[0];
        LoadFile();
        StartCoroutine(routine: Control());
        mainCamera = Camera.main;
    }

    

    [System.Serializable]
    public struct GameData
    {
        public float money;
        public int people;
        public float electrysity;
        public List<BuildingsData> zdaniya;
        

        public GameData(float money, int people, float electrysity, List<BuildingsData> zdaniya)
        {
           
            this.money = money;
            this.people = people;
            this.electrysity = electrysity;
            this.zdaniya = zdaniya;
            
        }
    }
    [System.Serializable]
    public class BuildingsData
    {
        public float x;
        public float y;
        public float z;
        public string parent_name;
        public int object_index;
    }

    [System.Serializable]
    public class RoadData
    {
        public int x_game;
        public int y_game;

        public float x;
        public float y;
        public float z;

    }

    void Nakidish(GameObject Build, int tip, float x, float y, float z)
    {
        y += 1f;
        switch (tip)
        {
            case 0:
                Build.AddComponent<Electro1>();
                Build.transform.localPosition = new Vector3(x, y, z);
                break;

            case 1:
                Build.AddComponent<House1>();
                Build.transform.localPosition = new Vector3(x, y, z);
                break;
            case 2:
                Build.AddComponent<Factory1>();
                Build.transform.localPosition = new Vector3(x, y, z);
                break;
            case 3:
                Build.AddComponent<Laboratory>();
                Build.transform.localPosition = new Vector3(x, y, z);
                break;
            case 4:
                Build.transform.localPosition = new Vector3(x, y, z);
                break;
            case 5:
                Build.transform.localPosition = new Vector3(x, y, z);
                break;

        }
        buildings_list.Add(new BuildingsData
        {
            parent_name = Build.transform.parent.name,
            x = Build.transform.localPosition.x,
            y = Build.transform.localPosition.y,
            z = Build.transform.localPosition.z,
            object_index = bildings.value,
        });
        for (int i = 0; i < buildings_list.Count; i++)
        {
            Debug.Log(buildings_list[i]);
        }
    }

    public void SaveFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        GameData data = new GameData(money, People_count, Electrisity_count, buildings_list);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();

    }

    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        money = data.money;
        People_count = data.people;
        Electrisity_count = data.electrysity;
        List<BuildingsData> zdaniya = data.zdaniya;
   
        for (int i = 0; i < zdaniya.Count; i++)
        {
            GameObject spawn = Instantiate(things[zdaniya[i].object_index], GameObject.Find(zdaniya[i].parent_name).transform);
            Nakidish(spawn, zdaniya[i].object_index, zdaniya[i].x, zdaniya[i].y, zdaniya[i].z);
            Debug.Log(zdaniya[i].object_index);

        }
    }
    void made_buttons()
    {
        bildings.ClearOptions();
        List<Dropdown.OptionData> dates = new List<Dropdown.OptionData>();
        dates.Add(new UnityEngine.UI.Dropdown.OptionData() { text = "Электростанция" });
        dates.Add(new UnityEngine.UI.Dropdown.OptionData() { text = "Жилой дом первого поколения" });
        dates.Add(new UnityEngine.UI.Dropdown.OptionData() { text = "Завод" });
        bildings.AddOptions(dates);
        prices.Add(new List<float> { 1f, 1f, 0f });
        prices.Add(new List<float> { 50f, 1f, 0.5f });
        prices.Add(new List<float> { 70f, 2f, 3 });
        prices.Add(new List<float> { 120f, 2f, 3 });
        prices.Add(new List<float> { 70f, 2f, 0.5f });
        prices.Add(new List<float> { 150f, 2f, 3 });

    }



    void start_value()
    {
        money = 150f;
        People_count = 20;
        Electrisity_count = 5f;
    }


    public void onclicka()
    {
        if (mode == 2)
        {
            return;
        }
        dropper.SetActive(!dropper.active);
        dropper.GetComponent<Dropdown>().Hide();
        made_buttons();
        if (mode == 0)
        {
            mode = 1;
            Button.GetComponent<Image>().color = Color.green;
        }
        else
        {
            mode = 0;
            Button.GetComponent<Image>().color = Color.white;
        }

        /* dropper.SetActive(false);
         Button.SetActive(!Button.active);
         is_building = true;
         what = things[bildings.value];*/

    }

    public void Add_button()
    {

        what = things[bildings.value];

        // dropper.SetActive(true);
        /* Button.SetActive(!Button.active);*/

    }

    public void choose_city()
    {
        SaveFile();
        Application.LoadLevel("City_choosing");

    }

    private IEnumerator Control()
    {
        Debug.Log("started control");
        while (true)
        {
            switch (Input.touchCount)
            {
                case 1:
                    Debug.Log("one touch!");
                    yield return StartCoroutine(routine: OneFingerMode());
                    break;
                case 2:
                    yield return StartCoroutine(routine: TwoFingersMode());
                    break;
            }
            yield return null;
        }
           
    }
    private IEnumerator ModeChoose()
    {
        while (true)
        {
            switch (mode)
            {
                case 1:
                    yield return StartCoroutine(routine: Road_Build());
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
        
        float time_to_choose = 2;
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
                    if (hit.transform.name.IndexOf("building") >= 0)
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

                        
                    }
                    if (hit.transform.gameObject == choosed_building)
                    {
                        continue;
                    }
                    float x = Input.touches[0].position.x;
                    float y = Input.touches[0].position.y;
                    float delta_x = old_x - x;
                    float delta_y = old_y - y;
                    old_x = x;
                    old_y = y;
                    float scale = every_fucking_thing.transform.localScale.x;
                    float all_x = every_fucking_thing.transform.localPosition.x - delta_x / 2000;
                    float all_y = every_fucking_thing.transform.localPosition.y;
                    float all_z = every_fucking_thing.transform.localPosition.z - delta_y / 2000;
                    if (Mathf.Abs(all_x) > 0.5)
                    {
                        all_x = every_fucking_thing.transform.localPosition.x;
                    }
                    if (Mathf.Abs(all_z) > 0.25)
                    {
                        all_z = every_fucking_thing.transform.localPosition.z;
                    }
                    Vector3 target = new Vector3(all_x, all_y,  all_z);
                    every_fucking_thing.transform.localPosition = target;



                }
            }
            yield return null;
        }
        
    }

    private IEnumerator TwoFingersMode()
    {
        float x1 = Input.touches[0].position.x;
        float y1 = Input.touches[0].position.y;
        float x2 = Input.touches[1].position.x;
        float y2 = Input.touches[1].position.y;
        message.text = "2 ПАЛЬЦА!";
        float old_dist = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
        float old_angle = Mathf.Atan2((y2 - y1) / old_dist, (x2 - x1) / old_dist) * Mathf.Rad2Deg;
        yield return null;
        while (true)
        {
            if (Input.touchCount != 2)
            {
                message.text = "ХУЙ";
                yield break;
            }
            x1 = Input.touches[0].position.x;
            y1 = Input.touches[0].position.y;
            x2 = Input.touches[1].position.x;
            y2 = Input.touches[1].position.y;
            float dist = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
            //float dist = Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
            float angle = Mathf.Atan2((y2 - y1) / dist, (x2 - x1) / dist) * Mathf.Rad2Deg;
            
            //float angle = Vector2.Angle(Input.touches[0].position, Input.touches[1].position);

            float delta_dist = old_dist / dist;
            float delta_angle = old_angle - angle;

            //message.text += "\n" + dist.ToString() + " " + delta_dist.ToString();
            message.text = "\n" + angle.ToString() + " " + delta_angle.ToString() + 
                "\n" + dist.ToString();
            old_dist = dist;
            old_angle = angle;



            every_fucking_thing.transform.Rotate(0, delta_angle, 0);
           

            float scale = every_fucking_thing.transform.localScale.x / delta_dist;

            if (scale > 2)
            {
                scale = every_fucking_thing.transform.localScale.x;
            }
            if (scale < 0.6f)
            {
                scale = every_fucking_thing.transform.localScale.x;
            }
            
            
            every_fucking_thing.transform.localScale = new Vector3(scale, scale, scale);

            yield return null;

        }
    }
    private IEnumerator Road_Build()
    {
        while (true)
        {
            if (mode != 1)
            {
                yield break;
            }
            if (Input.touchCount == 1)
            {
                RaycastHit hit;
                var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.name.IndexOf("Game_field") >= 0)
                    {

                    }
                }
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
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


                
                timer = Time.deltaTime + timer;

                // Check if we have reached beyond 2 seconds.
                // Subtracting two is more accurate over time than resetting to zero.

                PeopleText.text = People_count.ToString();
                MoneyText.text = money.ToString();
                ElectrisityText.text = Electrisity_count.ToString();
            
        
    }
}
