using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandlerCursor : MonoBehaviour {

	// Use this for initialization
	public Texture2D mouse;
	public Texture2D hand;
	public Texture2D grab;
	public Texture2D clicked;
	public CursorMode cursorMode = CursorMode.ForceSoftware;
	public Vector2 hotSpot = Vector2.zero;

	void Start () {

		EventTrigger.Entry eventHover = new EventTrigger.Entry();
		EventTrigger.Entry eventClick = new EventTrigger.Entry();
		EventTrigger.Entry eventExit = new EventTrigger.Entry();
    	eventHover.eventID = EventTriggerType.PointerEnter;
    	eventHover.callback.AddListener((eventData) => { setHand(); });
		eventClick.eventID = EventTriggerType.PointerClick;
    	eventClick.callback.AddListener((eventData) => { setClicked(); });
		eventExit.eventID = EventTriggerType.PointerExit;
    	eventExit.callback.AddListener((eventData) => { setMouse(); });
		GameObject[] allButtons = GameObject.FindGameObjectsWithTag("Button");
		foreach (GameObject buttons in allButtons)
		{
			buttons.AddComponent<EventTrigger>();
    		buttons.GetComponent<EventTrigger>().triggers.Add(eventHover);
			buttons.GetComponent<EventTrigger>().triggers.Add(eventClick);	
			buttons.GetComponent<EventTrigger>().triggers.Add(eventExit);		
		}
			
	}

	public void setMouse()
	{
		Cursor.SetCursor(mouse, hotSpot, cursorMode);
	}
	public void setClicked()
	{
		Cursor.SetCursor(clicked, hotSpot, cursorMode);
	}
	public void setHand()
	{
		Cursor.SetCursor(hand, hotSpot, cursorMode);
	}
	public void setGrab()
	{
		Cursor.SetCursor(grab, hotSpot, cursorMode);
	}
}
