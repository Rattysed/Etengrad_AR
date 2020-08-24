using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electro1 : MonoBehaviour
{
    GameObject main;

    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("Directional Light");
        main.GetComponent<Main>().Electrisity_count += 1;
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
