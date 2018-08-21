using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System.IO;
using System.Text;

public class Spawn : MonoBehaviour {

	// Use this for initialization
	Ray ray;
    RaycastHit hit;
	public int isExpanded;
	int expanded;
	float wallSize;
	float compSize;
	public int component;
	public float l;
	public float r;
	public int floorCount;
	public float cameraY;
	public float rotationResetSpeed = 1.0f;
	public float prices;
	public float distance;
	public bool rotated;
	public static bool clicked;
	private Quaternion originalRotationValue;
	private Quaternion originalRotationValueCamera;
	private Vector3 cameraPosition;
	public static Vector3 roomTransform;
	private Text expansionPrice;
	private Plane plane;
	public Text expansionBase;
	public Text price;
	public Text price2;
	public Text componentName;
	HandlerCursor cursor;
	public Text eachComponentQuantity;
	public Text eachItemPrice;
	public Canvas mainCanvas;
	public Camera mainCamera;
	public Camera uiCamera;
	public StringBuilder stringBuilder = new StringBuilder();
	public StringBuilder stringBuilderCount = new StringBuilder();
	public StringBuilder stringBuilderCountPrice = new StringBuilder();
	public List<string> newCompName = new List<string>();
	public GameObject room;
	public GameObject greyRoom;
	public GameObject mainBase;
	public GameObject smallCabinet;
	public GameObject smallCabinetExpansion;
	public GameObject bigCabinet;
	public GameObject bigCabinetExpansion;
	public GameObject frontWall;
	public List<GameObject> expansion;
	public Texture[] textures;
	public Button[] button;
	public Button thisPosition;
	public Renderer rend;
	public GameObject floor;
	public GameObject leftWall;
	public GameObject rightWall;
	public Light leftLight;
	public Light rightLight;
	public Light mainLight;
	public static bool isAlreadyClicked = false;
	public bool displayed = false;
	public ComponentInformation componentInformation;
	public List<GameObject> newComp = new List<GameObject>(); 
    public Dictionary<string, int> RepeatedComponentCount = new Dictionary<string, int>(); 
    public Dictionary<string, float> ItemPrices = new Dictionary<string, float>();
	public ItemDictionary dictionary;
    public Dictionary<string, int> ItemArray; //pinz
    public Dictionary<string, int> listItem; //pinz

	void Start()
	{
		clicked = false;
		rotated = false;
		displayed = false;
		room.SetActive(false);
		room.GetComponent<Transform>().transform.localScale = roomTransform;
		smallCabinet.SetActive(false);
		bigCabinet.SetActive(false);
		expanded = 0;
		component = 0;
		floorCount = 0;
		prices = 20.0f;
		componentName.text = "";
		eachItemPrice.text = "";
		eachComponentQuantity.text = "";
		price.text = "RM"+prices.ToString("F2");
		price2.text = "RM"+prices.ToString("F2");
		componentInformation = new ComponentInformation();
		rend = floor.gameObject.GetComponent<Renderer>();
		wallSize = frontWall.GetComponent<Collider>().bounds.size.z;
		cursor = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HandlerCursor>();
		button = FindObjectsOfType(typeof(Button)) as Button[];
		componentInformation.isExpanded = 0;
		expansionBase.text = "Expansion : "+componentInformation.isExpanded;
		componentInformation.price = prices;
		componentInformation.componentQuantity = component;
		originalRotationValue = mainBase.transform.rotation;
		cameraPosition = mainCamera.transform.localPosition;
		originalRotationValueCamera = mainCamera.transform.rotation;
		mainCanvas.transform.GetChild(0).gameObject.SetActive(false);
		mainCanvas.transform.GetChild(1).gameObject.SetActive(false);
		mainCanvas.transform.GetChild(2).gameObject.SetActive(false);
		mainCanvas.transform.GetChild(7).gameObject.SetActive(true);
		mainCanvas.transform.GetChild(8).gameObject.SetActive(true);
		mainCanvas.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.SetActive(false);
		plane.SetNormalAndPosition(Camera.main.transform.forward, transform.position);
	}

	public void FixedUpdate()
	{
		if(Input.GetMouseButtonUp(0))
		{
			InputManager.carrying = false;
        	cursor.setMouse();
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
			{
				if(hit.collider.CompareTag("NewComponent"))
				{
					Vector3 screenPoint = uiCamera.WorldToViewportPoint(hit.collider.gameObject.transform.position);
 					bool onScreen = screenPoint.x >= 0.7f;
					Debug.Log(screenPoint.x);
					dictionary = JsonUtility.FromJson<ItemDictionary>(JsonFileReader.LoadJsonAsResource("Items/ItemDictionary.json"));
					foreach (string dictionaryItem in dictionary.items)
					{
						string jsonDir = hit.collider.gameObject.GetComponent<Text>().text;
						if (dictionaryItem == jsonDir)
						{
							string myLoadedItem = JsonFileReader.LoadJsonAsResource(dictionaryItem); //pinz
                			Item myItem = JsonUtility.FromJson<Item>(myLoadedItem); //pinz
                			Debug.Log(myItem.itemID); //pinz
                			Debug.Log(myItem.itemPrice); //pinz
                			string json_price; //pinz
							float iPrice;
							if(onScreen == true)
							{
								json_price = myItem.itemPrice; //pinz
								iPrice = float.Parse(json_price);
								component = component-1;//reduce the component amount from the base
        						componentInformation.componentQuantity = component;
								Debug.Log("Price for this component = RM"+float.Parse(json_price));
        						prices = prices-(float.Parse(json_price));//reduce the total price based on the selected component price
        						componentInformation.price = prices;//pass the current total price to the base class
								price.text = "RM"+componentInformation.price.ToString("F2");//display the total price
								price2.text = "RM"+componentInformation.price.ToString("F2");//display the total price
        						hit.collider.gameObject.GetComponent<InputManager>().RemoveComponent();
								cursor.setMouse();
								Destroy(hit.collider.gameObject);
								//isAlreadyClicked = true;
								mainCanvas.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.SetActive(false);
							}
							break;
						}
					}
				}
			}
		}
	}
	public void ChangeFloorTexture()
	{
		/*rend.material.mainTexture = textures[floorCount];
		floorCount = floorCount+1;
		if(floorCount>=4)
		{
			floorCount = 0;
		}*/
		if(room.activeInHierarchy == true)
		{
			if(displayed == false)
			{
				mainCanvas.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(true);
				displayed = true;
			}
			else
			{
				mainCanvas.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(false);
				displayed = false;
			}
		}
	}
	public void ChangeTexture(Texture mainTexure)
	{
		//rend.material.mainTexture = textures[floorCount];
		rend.material.mainTexture = mainTexure;
		//rend.material.SetTexture(mainTexure,textures);
	}
	public void SpawnPrefab(Transform prefab)
	{
		InputManager inputManager;
		clicked = false;
		compSize = prefab.gameObject.GetComponent<Collider>().bounds.size.z;
		inputManager = prefab.gameObject.GetComponent<InputManager>();
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit))
		{
			Debug.Log("clicked");
			if(isAlreadyClicked == false)
			{
				OnClickedButton();
				Instantiate(prefab, new Vector3(hit.point.x, hit.point.y, frontWall.gameObject.transform.position.z + compSize*2 - wallSize*3), prefab.transform.rotation);
				isAlreadyClicked = true;
				Debug.Log(clicked);
			}
		}
	}
	public void OnClickedButton()
	{ 
		uiCamera.cullingMask = 1 << 5 | 1 << 8 | 1 << 9;
		thisPosition = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
		Debug.Log("heloo");
		if((EventSystem.current.IsPointerOverGameObject() == thisPosition.CompareTag("Button")) && thisPosition.interactable == true)
		{
			Debug.Log(thisPosition);
			thisPosition.GetComponent<Image>().raycastTarget = false;
			thisPosition.interactable = false;
		}
		else
		return;
 	}
	public void ChooseCabinetSize(GameObject cabinet)
	{
		cabinet.SetActive(true);
		if(cabinet.gameObject.name == bigCabinet.gameObject.name)
		{
			bigCabinet.SetActive(true);
			smallCabinet.SetActive(false);
			//Debug.Log(cabinet);
		}
		if(cabinet.gameObject.name == smallCabinet.gameObject.name)
		{
			smallCabinet.SetActive(true);
			bigCabinet.SetActive(false);
		}
	}
	/*void ExpandLeft()
	{
		if(expansion[1].gameObject.activeInHierarchy == false)
		{
			leftSide.text = "Remove Left";
			isExpanded=isExpanded+1;
			expansion[1].gameObject.SetActive(true);
			componentInformation.isExpanded = isExpanded;
			expansionBase.text = "Expansion : "+componentInformation.isExpanded;
			expansionPrice = expansion[1].gameObject.GetComponent<Text>() as Text;
			prices = prices+(float.Parse(expansionPrice.text));
			componentInformation.price = prices;
			price.text = "Price = RM"+componentInformation.price.ToString("F2");
		}
		else if(expansion[1].gameObject.activeInHierarchy == true)
		{
			leftSide.text = "Expand Left";
			expansion[1].gameObject.SetActive(false);
			expansionPrice = expansion[1].gameObject.GetComponent<Text>() as Text;
			prices = prices-(float.Parse(expansionPrice.text));
			componentInformation.price = prices;
			price.text = "Price = RM"+componentInformation.price.ToString("F2");
			isExpanded=isExpanded-1;
			componentInformation.isExpanded = isExpanded;
			expansionBase.text = "Expansion : "+componentInformation.isExpanded;
		}
		else
		return;
	}
	void ExpandRight()
	{
		if(expansion[2].gameObject.activeInHierarchy == false)
		{
			rightSide.text = "Remove Right";
			isExpanded=isExpanded+1;
			expansion[2].gameObject.SetActive(true);
			componentInformation.isExpanded = isExpanded;
			expansionBase.text = "Expansion : "+componentInformation.isExpanded;
			expansionPrice = expansion[2].gameObject.GetComponent<Text>() as Text;
			prices = prices+(float.Parse(expansionPrice.text));
			componentInformation.price = prices;
			price.text = "Price = RM"+componentInformation.price.ToString("F2");
		}
		else if(expansion[2].gameObject.activeInHierarchy == true)
		{
			rightSide.text = "Expand Right";
			expansion[2].gameObject.SetActive(false);
			expansionPrice = expansion[2].gameObject.GetComponent<Text>() as Text;
			prices = prices-(float.Parse(expansionPrice.text));
			componentInformation.price = prices;
			price.text = "Price = RM"+componentInformation.price.ToString("F2");
			isExpanded=isExpanded-1;
			componentInformation.isExpanded = isExpanded;
			expansionBase.text = "Expansion : "+componentInformation.isExpanded;
		}
		else
		return;
	}*/
	public void Expand()
	{
		if(expanded<1)
		{
			isExpanded=isExpanded+1;
			componentInformation.isExpanded = isExpanded;
			expansion[isExpanded].gameObject.SetActive(true);
			expansionPrice = expansion[isExpanded].gameObject.GetComponent<Text>() as Text;
			expansionBase.text = "Expansion = "+componentInformation.isExpanded;
			prices = prices+(float.Parse(expansionPrice.text));
			componentInformation.price = prices;
			price.text = "RM"+componentInformation.price.ToString("F2");
			price2.text = "RM"+componentInformation.price.ToString("F2");
			Debug.Log(expansion[isExpanded]);
			Debug.Log(componentInformation.isExpanded);
			expanded++;
		}
		else
		return;
	}
	public void RemoveExpansion()
	{
		Debug.Log(componentInformation.isExpanded);
		if((expansion[isExpanded].gameObject.activeInHierarchy == true))
		{
			foreach(GameObject compGameObject in GameObject.FindGameObjectsWithTag("Base"))
			{
				if(compGameObject == expansion[isExpanded].gameObject)
				{
					Debug.Log("hoo ="+compGameObject);
					foreach(Transform slot in compGameObject.transform)
					{
						Debug.Log("compGameObject = "+compGameObject.transform);
						//Debug.Log(expansion[isExpanded]);
						foreach(Transform child in slot.transform)
						{
							Debug.Log(child);
							Debug.Log("slot.transform.parent = "+slot.transform);
							foreach(Transform componentChild in child.transform)
							{
								if(componentChild.CompareTag("NewComponent"))
								{
									Debug.Log("There's a child in here");
									dictionary = JsonUtility.FromJson<ItemDictionary>(JsonFileReader.LoadJsonAsResource("Items/ItemDictionary.json"));
									foreach (string dictionaryItem in dictionary.items)
									{
										string jsonDir = componentChild.gameObject.GetComponent<Text>().text;
										if (dictionaryItem == jsonDir)
										{
											string myLoadedItem = JsonFileReader.LoadJsonAsResource(dictionaryItem); //pinz
                							Item myItem = JsonUtility.FromJson<Item>(myLoadedItem); //pinz
                							Debug.Log(myItem.itemID); //pinz
                							Debug.Log(myItem.itemPrice); //pinz
                							string json_price; //pinz
											float iPrice;
											if(componentChild.gameObject.activeInHierarchy == true)
											{
												json_price = myItem.itemPrice; //pinz
												iPrice = float.Parse(json_price);
												component = component-1;//reduce the component amount from the base
        										componentInformation.componentQuantity = component;
												Debug.Log("Price for this component = RM"+float.Parse(json_price));
        										prices = prices-(float.Parse(json_price));//reduce the total price based on the selected component price
        										componentInformation.price = prices;//pass the current total price to the base class
												price.text = "RM"+componentInformation.price.ToString("F2");//display the total price
												price2.text = "RM"+componentInformation.price.ToString("F2");//display the total price
        										componentChild.gameObject.GetComponent<InputManager>().RemoveComponent();
												Destroy(componentChild.gameObject);
											}
											break;
										}
									}
								}
							}
							child.tag = "Starterslots";
						}
					}
				}
			}
			if(expanded>0)
			{
				expansion[isExpanded].gameObject.SetActive(false);
				expansionPrice = expansion[isExpanded].gameObject.GetComponent<Text>() as Text;
				expansionBase.text = "Expansion = "+componentInformation.isExpanded;
				prices = prices-(float.Parse(expansionPrice.text));
				componentInformation.price = prices;
				price.text = "RM"+componentInformation.price.ToString("F2");
				price2.text = "RM"+componentInformation.price.ToString("F2");
				isExpanded=isExpanded-1;
				componentInformation.isExpanded = isExpanded;
				expansionBase.text = "Expansion = "+componentInformation.isExpanded;
				Debug.Log(componentInformation.isExpanded);
				--expanded;
			}
			else if(expanded==0)
			Debug.Log("done");
			return;
		}
	}
    public void ComponentAdded()
	{
        dictionary = JsonUtility.FromJson<ItemDictionary>(JsonFileReader.LoadJsonAsResource("Items/ItemDictionary.json"));
        foreach (string dictionaryItem in dictionary.items)
        {
            string jsonDir = GameObject.FindWithTag("Component").GetComponent<Text>().text;

            if (dictionaryItem == jsonDir)
            {
                
                string myLoadedItem = JsonFileReader.LoadJsonAsResource(dictionaryItem); //pinz
                Item myItem = JsonUtility.FromJson<Item>(myLoadedItem); //pinz
                Debug.Log(myItem.itemID); //pinz
                Debug.Log(myItem.itemPrice); //pinz
                string json_price; //pinz

		        Text priced;
		        float iPrice;
		        newCompName = new List<string>();
		        newComp = new List<GameObject>();
		        if(clicked==false)
		        {
                    json_price = myItem.itemPrice; //pinz
			        priced = GameObject.FindWithTag("Component").GetComponent<Text>() as Text;
                    iPrice = float.Parse(json_price);
			        component=component+1;
			        componentInformation.componentQuantity = component;
			        Debug.Log("components attached = "+componentInformation.componentQuantity);
                    prices = prices + (float.Parse(json_price));
			        componentInformation.price = prices;
			        Debug.Log("prices = "+componentInformation.price);
			        price.text = "RM"+componentInformation.price.ToString("F2");
			        price2.text = "RM"+componentInformation.price.ToString("F2");
			        priced.transform.name = priced.transform.name.Replace("(Clone)","").Trim();
			        priced.transform.gameObject.tag ="NewComponent";
			        if(!newCompName.Contains(name))
			        {
				        newCompName.Add(priced.transform.name);
                        Debug.Log(priced.transform.name + " " + float.Parse(json_price));
				        foreach(string item in newCompName)
				        {
					        if(!RepeatedComponentCount.ContainsKey(item))
					        {
						        RepeatedComponentCount.Add(item,1);
					        }
					        else
					        {
						        int count = 0;
						        RepeatedComponentCount.TryGetValue(item,out count);
						        RepeatedComponentCount.Remove(item);
						        RepeatedComponentCount.Add(item,count+1);
					        }

                            ItemArray = RepeatedComponentCount; //pinz

					        if(!ItemPrices.ContainsKey(item))
					        {
						        ItemPrices.Add(item,iPrice);
					        }
					        else
					        {
						        float count = iPrice;
						        ItemPrices.TryGetValue(item,out count);
						        ItemPrices.Remove(item);
						        ItemPrices.Add(item,count+iPrice);
					        }
				        }
				        stringBuilder.Length = 0;
				        stringBuilderCount.Length = 0;
				        stringBuilderCountPrice.Length = 0;
				        foreach (KeyValuePair<string,int>entry in RepeatedComponentCount)
				        {
					        stringBuilder.AppendLine().Append(entry.Key);
					        stringBuilderCount.AppendLine().Append(entry.Value);
					        stringBuilder.AppendLine();
					        stringBuilderCount.AppendLine();
				        }
				        foreach(KeyValuePair<string,float>entry in ItemPrices)
				        {
					        stringBuilderCountPrice.AppendLine().Append("RM"+entry.Value.ToString("F2"));
					        stringBuilderCountPrice.AppendLine();
				        }
				        componentName.text = stringBuilder.ToString();
				        eachComponentQuantity.text = stringBuilderCount.ToString();
				        eachItemPrice.text =stringBuilderCountPrice.ToString();
			        }
		        }
                break; //pinz
            }
        }
        PassSubmitToWeb(RepeatedComponentCount); //pinz
	}

    public void PassSubmitToWeb(Dictionary<string, int> loadItem)
    {
        listItem = loadItem; //pinz
    }

	public void RotateLeft()
	{
		if(rotated == false)
		{
			leftLight.enabled = false;
			rightLight.enabled = true;
			mainLight.enabled = false;
			leftWall.SetActive(true);
			rightWall.SetActive(false);
			uiCamera.cullingMask = 1 << 5;
			mainBase.transform.Rotate(0, l, 0);//rotate the base to its x-axis
			mainCamera.transform.Rotate(-15,0,0);
			mainCamera.transform.Translate(0,cameraY,0);
			rotated = true;
		}
	}
	public void RotateRight()
	{
		if(rotated == false)
		{
			leftLight.enabled = true;
			rightLight.enabled = false;
			mainLight.enabled = false;
			leftWall.SetActive(false);
			rightWall.SetActive(true);
			uiCamera.cullingMask = 1 << 5;
			mainBase.transform.Rotate(0, r, 0);//rotate the base to its x-axis
			mainCamera.transform.Rotate(-15,0,0);
			mainCamera.transform.Translate(0,cameraY,0);
			rotated = true;
		}
	}
	public void Front()
	{
		if(rotated == true)
		{
			leftWall.SetActive(false);
			rightWall.SetActive(false);
			leftLight.enabled = false;
			rightLight.enabled = false;
			mainLight.enabled = true;
			uiCamera.cullingMask = 1 << 5;
			mainBase.transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.time * rotationResetSpeed); 
			mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, cameraPosition, Time.time * rotationResetSpeed);
			mainCamera.transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValueCamera, Time.time * rotationResetSpeed);  
			rotated = false;
		}
	}
	public void Preview()
	{
		GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
		clicked=false;
		room.SetActive(true);
		greyRoom.SetActive(false);
		mainCanvas.transform.GetChild(0).gameObject.SetActive(false);
		mainCanvas.transform.GetChild(1).gameObject.SetActive(false);
		mainCanvas.transform.GetChild(2).gameObject.SetActive(true);
		uiCamera.cullingMask = 1 << 5;
		//mainCanvas.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.SetActive(true);
		Front();
		foreach (GameObject componentGameObject in GameObject.FindGameObjectsWithTag("Component"))
		{
			if(room.activeInHierarchy==true)
			{
				componentGameObject.transform.GetChild(0).gameObject.SetActive(false);
			}
		}
		foreach(GameObject go in gos)
 		{
     		if(go.layer == 8 && go.CompareTag("NewComponent"))
     		{
         		go.GetComponent<Collider>().enabled = false;
     		}
 		} 
	}
	public void Edit()
	{
		//mainCamera.transform.Translate(3.2881f,0,0);
		//cameraPosition = mainCamera.transform.localPosition;
		if(bigCabinet.activeInHierarchy == true || smallCabinet.activeInHierarchy == true)
		{
			GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
			clicked=false;
			room.SetActive(false);
			greyRoom.SetActive(true);
			mainCanvas.transform.GetChild(0).gameObject.SetActive(true);
			mainCanvas.transform.GetChild(1).gameObject.SetActive(true);
			mainCanvas.transform.GetChild(2).gameObject.SetActive(false);
			mainCanvas.transform.GetChild(7).gameObject.SetActive(false);
			mainCanvas.transform.GetChild(8).gameObject.SetActive(false);
			uiCamera.cullingMask = 1 << 5 | 1 << 8 | 1 << 9;
			//mainCanvas.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.SetActive(false);
			displayed = false;
			if(expansion.Any())
			{
				expansion.Clear();
			}
			if(!expansion.Any())
			{
				if(bigCabinet.activeInHierarchy == true)
				{
					expansion.Add(bigCabinet);
					expansion.Add(bigCabinetExpansion);
				}
				if(smallCabinet.activeInHierarchy == true)
				{
					expansion.Add(smallCabinet);
					expansion.Add(smallCabinetExpansion);
				}
			}
			foreach (GameObject componentGameObject in GameObject.FindGameObjectsWithTag("Component"))
			{
				if(room.activeInHierarchy==false)
				{
					componentGameObject.transform.GetChild(0).gameObject.SetActive(true);
				}
			}
			foreach(GameObject go in gos)
 			{
     			if(go.layer == 8 && go.CompareTag("NewComponent"))
     			{
         			go.GetComponent<Collider>().enabled = true;
     			}
 			} 
		}
		
	}
	public void ChooseCabinetSizeWindow()
	{
		//mainCamera.transform.Translate(-3.2881f,0,0);
		//RemoveExpansion();
		RemoveAll();
		greyRoom.SetActive(true);
		mainCanvas.transform.GetChild(0).gameObject.SetActive(false);
		mainCanvas.transform.GetChild(1).gameObject.SetActive(false);
		mainCanvas.transform.GetChild(2).gameObject.SetActive(false);
		mainCanvas.transform.GetChild(7).gameObject.SetActive(true);
		mainCanvas.transform.GetChild(8).gameObject.SetActive(true);
		//uiCamera.cullingMask = 1 << 5 | 1 << 8 | 1 << 9;
		//mainCanvas.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.SetActive(false);
		displayed = false;
	}
	public void RemoveAll()
	{
		foreach(GameObject compGameObject in GameObject.FindGameObjectsWithTag("Base"))
		{
			foreach(Transform slot in compGameObject.transform)
			{
				Debug.Log("compGameObject = "+compGameObject.transform);
				//Debug.Log(expansion[isExpanded]);
				foreach(Transform child in slot.transform)
				{
					Debug.Log(child);
					Debug.Log("slot.transform.parent = "+slot.transform);
					foreach(Transform componentChild in child.transform)
					{
						if(componentChild.CompareTag("NewComponent"))
						{
							Debug.Log("There's a child in here");
							dictionary = JsonUtility.FromJson<ItemDictionary>(JsonFileReader.LoadJsonAsResource("Items/ItemDictionary.json"));
							foreach (string dictionaryItem in dictionary.items)
							{
								string jsonDir = componentChild.gameObject.GetComponent<Text>().text;
								if(dictionaryItem == jsonDir)
								{
									string myLoadedItem = JsonFileReader.LoadJsonAsResource(dictionaryItem); //pinz
                					Item myItem = JsonUtility.FromJson<Item>(myLoadedItem); //pinz
                					Debug.Log(myItem.itemID); //pinz
                					Debug.Log(myItem.itemPrice); //pinz
                					string json_price; //pinz
									float iPrice;
									if(componentChild.gameObject.activeInHierarchy == true)
									{
										json_price = myItem.itemPrice; //pinz
										iPrice = float.Parse(json_price);
										component = component-1;//reduce the component amount from the base
        								componentInformation.componentQuantity = component;
										Debug.Log("Price for this component = RM"+float.Parse(json_price));
        								prices = prices-(float.Parse(json_price));//reduce the total price based on the selected component price
        								componentInformation.price = prices;//pass the current total price to the base class
										price.text = "RM"+componentInformation.price.ToString("F2");//display the total price
										price2.text = "RM"+componentInformation.price.ToString("F2");//display the total price
        								componentChild.gameObject.GetComponent<InputManager>().RemoveComponent();
										Destroy(componentChild.gameObject);
									}
									break;
								}
							}
						}
					}
					child.tag = "Starterslots";
				}
			}
		}
		if(expanded>0)
		{
			expansion[isExpanded].gameObject.SetActive(false);
			expansionPrice = expansion[isExpanded].gameObject.GetComponent<Text>() as Text;
			expansionBase.text = "Expansion = "+componentInformation.isExpanded;
			prices = prices-(float.Parse(expansionPrice.text));
			componentInformation.price = prices;
			price.text = "RM"+componentInformation.price.ToString("F2");
			price2.text = "RM"+componentInformation.price.ToString("F2");
			isExpanded=isExpanded-1;
			componentInformation.isExpanded = isExpanded;
			expansionBase.text = "Expansion = "+componentInformation.isExpanded;
			Debug.Log(componentInformation.isExpanded);
			--expanded;
		}
		else if(expanded==0)
		Debug.Log("done");
		return;
	}
	public void OnApplyChanges()
	{
		SaveDesign();
	}
	public void SaveDesign()
	{
		string jsonData = JsonUtility.ToJson(componentInformation, true);
		File.WriteAllText(Application.persistentDataPath + "/Componentinformation.json", jsonData);
		Debug.Log("Saved");
	}
}
