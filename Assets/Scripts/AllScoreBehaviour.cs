using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class AllScoreBehaviour : MonoBehaviour
{
    public float resourseMoney;
    public float resourseElectricity;
    public int resoursePeople;
    [Header("Labels")]
    public Text labelResourseMoney;
    public Text labelResourseElectricity;
    public Text labelResoursePeople;
    // Start is called before the first frame update

    [System.Serializable]
    struct GameData
    {
        public float money;
        public float electricity;
        public int people;
        public GameData(float money, float electricity,  int people)
        {
           
            this.money = money;
            this.people = people;
            this.electricity = electricity;
            
        }
    }
    void resourseLabelUpdate() // Обновление табличек с числом ресурсов
    { 
        labelResourseElectricity.text = resourseElectricity.ToString();
        labelResourseMoney.text = resourseMoney.ToString();
        labelResoursePeople.text = resoursePeople.ToString();

    }
    void LoadFile() // Функция загрузки данных из файла с сохранкой. Можем сделать шифрование, но нахера
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
        resourseMoney = data.money;
        resourseElectricity = data.electricity;
        resoursePeople = data.people;
        // 
        // smth = data.smth
        // *место для других данных*
    }

    void SaveFile(){
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        GameData data = new GameData(resourseMoney, resourseElectricity, resoursePeople /*, Что-то ещё,*/);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    private void OnApplicationPause(bool pauseStatus) {
        SaveFile();
    }
    void Start()
    {
    
     
    }
    // Update is called once per frame
    void Update()
    {
        resourseLabelUpdate();
    }

}
