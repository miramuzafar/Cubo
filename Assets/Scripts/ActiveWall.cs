using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWall : MonoBehaviour {

	// Use this for initialization
	Ray ray;
    RaycastHit hit;
	public GameObject colorPicker;
	public bool activeFloor; 

	void Start () {

		activeFloor = false;
	}
	
	// Update is called once per frame
	void Update()
 	{
		if(Input.GetKeyDown (KeyCode.Mouse0)) 
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        	if(Physics.Raycast(ray, out hit))
			{
				if(colorPicker.activeInHierarchy == true)
				{
					if(activeFloor==false)
					{
						colorPicker.transform.GetChild(0).gameObject.SetActive(true);
						colorPicker.transform.GetChild(1).gameObject.SetActive(false);
						activeFloor = true;
					}
					if(hit.collider.CompareTag("Floor"))
					{
						if(activeFloor==true)
						{
							colorPicker.transform.GetChild(0).gameObject.SetActive(false);
							colorPicker.transform.GetChild(1).gameObject.SetActive(true);
							activeFloor = false;
						}
					}
				}
			}
		}
	}
}
