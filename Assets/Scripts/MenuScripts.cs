using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScripts : MonoBehaviour
{
    public void load_city_one()
    {
        
        Application.LoadLevel("City1");
    }
    public void load_city_two()
    {
        Application.LoadLevel("City2");
    }
    public GameObject menuButton;
    
    public GameObject cubeCurtain;
    public Sprite menu1;
    public Sprite menu2;
    bool menu_active = false;
    public GameObject Mesh;
    public void menu_choosed()
    {
        menu_active = !menu_active;
        for (int i = 0; i < menuButton.transform.childCount; i++)
        {
            menuButton.transform.GetChild(i).transform.gameObject.SetActive(menu_active);
        }
        
        if (menu_active)
        {
            //cubeCurtain.SetActive(true);
            menuButton.GetComponent<Image>().sprite=menu2;
        }
        else
        {
            //cubeCurtain.SetActive(false);
            menuButton.GetComponent<Image>().sprite = menu1;
        }


    }

    public void Mesh_mode()
    {
        Mesh.SetActive(!Mesh.activeSelf);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
