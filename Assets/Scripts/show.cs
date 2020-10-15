using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Management.Instrumentation;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class show : MonoBehaviour
{
	public Button button;
	public List<GameObject> image_buttons;
	//public GameObject[] =new GameObject[256];
	public GameObject Menu_canvas;
	// Start is called before the first frame update
    void Start()
    {
     
    }
	public bool make = false;
	string way = "C:\\Users\\docgr\\Pictures\\1.png";
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && make==false)
		{
			make = true;
			PickImage();
		}
	}
	int count_button_in_line = 0;
	int now_line = 0;
	float original_pos_x = -213.8f;
	float original_pos_y = 658f;
	float now_pos_x = 0;
	float now_pos_y = 0;
	float size_x = 102.7694f;
	float size_y = 102.7694f;
	private void PickImage()
	{
		
		Array a =new string[3] { ".PNG", ".png", ".jpg" };
		var info = new DirectoryInfo("C:\\Users\\docgr\\Pictures\\");
		var fileInfo = info.GetFiles();
		int count = 0;
		foreach (var file in fileInfo)
		{
			Debug.Log(file.Extension);
			if (System.Array.IndexOf(a, file.Extension) != -1)
			{
				
				//Создание кнопки
				image_buttons.Add(Instantiate(new GameObject()));
				image_buttons[image_buttons.Count-1].transform.SetParent(Menu_canvas.gameObject.transform);
				image_buttons[image_buttons.Count - 1].AddComponent<RectTransform>();
				image_buttons[image_buttons.Count - 1].AddComponent<Image>();
				image_buttons[image_buttons.Count - 1].GetComponent<RectTransform>().localScale = new Vector3(3.5492f, 3.5492f, 3.5492f);
				image_buttons[image_buttons.Count - 1].GetComponent<RectTransform>().sizeDelta = new Vector2(103.7949f, 102.7694f);
				image_buttons[image_buttons.Count - 1].GetComponent<RectTransform>().localPosition = new Vector3(0,0, 0);
				image_buttons[image_buttons.Count - 1].AddComponent<Button>();
				count++;
				count_button_in_line++;
				if (count_button_in_line == 2)
				{
					now_line++;
					count_button_in_line = 0;
				}
				//окончание создания

					NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
				{
					path = file.FullName;
					Debug.Log("Image path: " + path);
					if (path != null)
					{
						// Create Texture from selected image
						Texture2D texture = new Texture2D(2, 2);
						texture.LoadImage(File.ReadAllBytes(path));
						image_buttons[image_buttons.Count - 1].GetComponent<Image>().sprite =Sprite.Create(texture,new Rect(0.0f, 0.0f,texture.width,texture.height), new Vector2(0.5f, 0.5f));
					}
				}, "Select a PNG image", "image/png");
				
			}
			//Debug.Log(file.Extension);


		}

		//Debug.Log("Permission result: " + permission);
	}
}
