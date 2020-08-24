using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory1 : MonoBehaviour
{
    float time = 5;
    GameObject main;

    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("Directional Light");
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0)
        {
            time += 3;
            main.GetComponent<Main>().money += 1;
            //main.GetComponent<Main>().money = (int)(main.GetComponent<Main>().money * 1.1);
        }
        else
        {
            time = time - Time.deltaTime;
        }
    }
}
