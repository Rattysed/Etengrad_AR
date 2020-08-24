using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House2 : MonoBehaviour
{
    // Start is called before the first frame update
    float time = 5;

    int amount = 0;
    GameObject main;

    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("Directional Light");
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0 && amount < 100)
        {
            time += 5;
            main.GetComponent<Main>().People_count += 2;
            //main.GetComponent<Main>().People_count = (int)(main.GetComponent<Main>().People_count * 1.1);
            main.GetComponent<Main>().money += 1;

        }
        else
        {
            time = time - Time.deltaTime;
        }
    }
}


