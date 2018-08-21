using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Manager : MonoBehaviour {

	// Use this for initialization
    private Vector3 myRotation;
	public Canvas mainCanvas;
	bool pressed;
	bool pressed1;
	
	void Start() 
	{
        myRotation = Camera.main.transform.parent.rotation.eulerAngles;
		mainCanvas.transform.GetChild(2).gameObject.SetActive(false);
		mainCanvas.transform.GetChild(3).gameObject.SetActive(false);
		pressed = false;
		pressed1 = false;
    }
	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			if(EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject.CompareTag("ButtonLeft"))
			{
				myRotation.y = Mathf.Clamp(myRotation.y - 1f, -360f, 360f);
        		Camera.main.transform.parent.rotation = Quaternion.Euler(myRotation);
			}
			else if(EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject.CompareTag("ButtonRight"))
			{
				myRotation.y = Mathf.Clamp(myRotation.y + 1f, -360f, 360f);
        		Camera.main.transform.parent.rotation = Quaternion.Euler(myRotation);
			}
			else
			return;
		}
	}
	public void File()
	{
		if(pressed == false && pressed1 == false)
		{
			mainCanvas.transform.GetChild(2).gameObject.SetActive(true);
			pressed = true;
		}
		else if(pressed == false && pressed1 == true)
		{
			mainCanvas.transform.GetChild(2).gameObject.SetActive(false);
			//pressed = true;
		}
		else if(pressed == true)
		{
			mainCanvas.transform.GetChild(2).gameObject.SetActive(false);
			pressed = false;
		}
		else
		return;
	}
	public void List()
	{
		if(pressed1 == false && pressed == false)
		{
			mainCanvas.transform.GetChild(3).gameObject.SetActive(true);
			pressed1 = true;
		}
		else if(pressed1 == false && pressed == true)
		{
			mainCanvas.transform.GetChild(3).gameObject.SetActive(false);
			//pressed1 = true;
		}
		else if(pressed1 == true)
		{
			mainCanvas.transform.GetChild(3).gameObject.SetActive(false);
			pressed1 = false;
		}
		else
		return;
	}
}
