using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryListWindow : MonoBehaviour {

	// Use this for initialization
	public GameObject itemSlotPrefab;
	public GameObject content;
	public ToggleGroup itemSlotToggleGroup;
	private int xPos = 0;
	private int yPos = 0;
	private GameObject itemSlot;
	void Start () {

		CreateInventorySlotsWindow();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void CreateInventorySlotsWindow()
	{
		for( int i = 0; i < 20; i++) //gameObject find and look for player's inventory and get tge count of the inventory
		{
			itemSlot = (GameObject)Instantiate(itemSlotPrefab);
			itemSlot.name = i.ToString();
			itemSlot.GetComponent<Toggle>().group = itemSlotToggleGroup;
			itemSlot.transform.SetParent(content.transform);
			itemSlot.GetComponent<RectTransform>().localPosition = new Vector3(xPos,yPos,0);
			yPos -= (int)itemSlot.GetComponent<RectTransform>().rect.height;
		}
	}
}