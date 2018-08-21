using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PointerListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler {

	public bool Pressed = false;
    private Vector3 myRotation;
	
	void Start() 
	{
        myRotation = Camera.main.transform.rotation.eulerAngles;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
		{
			Pressed = true; 
		}
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Pressed = false;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
    public void Update()
    {
        if (Pressed)
        {
            
        }
    }
}
