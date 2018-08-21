using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
 
public class InputManager : MonoBehaviour
{
    HandlerCursor cursor;
    private Vector3 screenPoint;
    Text priced;
    private Vector3 offset;
    GameObject spawn;
    Spawn spawnScript;
    private Vector3 _originalPosition;
    public Vector3 currentPosition;
    public static bool carrying;
    public static bool collide = false;
    public Canvas mainCanvas;
    private Plane plane;

    void Start()
    {
        cursor = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HandlerCursor>();
        mainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        spawn = GameObject.Find("AppManager");
		spawnScript = spawn.GetComponent<Spawn>();
        priced = gameObject.GetComponent<Text>();
        collide = false;
    }
    void Update()
    {
        if(carrying)
        {
            cursor.setGrab();
        }
    }
    public void OnMouseEnter()
    {
        cursor.setHand();
    }
    public void OnMouseDown() 
    {
        carrying = true;
        //enable user to drag and drop the component
        if(collide == false)
        {
            plane.SetNormalAndPosition(Camera.main.transform.forward, transform.position);
            if(this.gameObject.transform.parent != null)
            {
                currentPosition = this.gameObject.transform.parent.position;
                this.gameObject.transform.SetParent(null, true);
                //_originalPosition = this.gameObject.transform.localPosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float dist;
                plane.Raycast(ray, out dist);
                offset = transform.position - ray.GetPoint(dist);
                //Ray ray = Camera.main.ScreenPointToRay(_originalPosition);
                //screenPoint = Camera.main.WorldToScreenPoint(_originalPosition);
                //offset = screenPoint - transform.position;
                //offset = _originalPosition - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, spawnScript.distance));
                mainCanvas.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                //_originalPosition = this.gameObject.transform.localPosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float dist;
                plane.Raycast(ray, out dist);
                offset = transform.position - ray.GetPoint(dist);
                //screenPoint = Camera.main.WorldToScreenPoint(_originalPosition);
                //Ray ray = Camera.main.ScreenPointToRay(_originalPosition);
                //offset = screenPoint - transform.position;
                //offset = _originalPosition - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, spawnScript.distance));
                
            }
        }
    }
    public void OnMouseDrag() 
    {
        cursor.setGrab(); 
        if(this.transform.parent == null || this.transform.parent != null)
        {
            //drag the component
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float dist;
            plane.Raycast(ray, out dist);
            Vector3 v3Pos = ray.GetPoint(dist); 
            v3Pos.z = this.gameObject.transform.localPosition.z;
            offset.z = 0;
            this.gameObject.transform.position = new Vector3(v3Pos.x, v3Pos.y, this.gameObject.transform.localPosition.z);
            //Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, spawnScript.distance);
            //Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint); 
            //this.gameObject.transform.position = new Vector3(curPosition.x, curPosition.y,this.gameObject.transform.localPosition.z);
            if(this.gameObject.CompareTag("Component"))
            {
                mainCanvas.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.SetActive(false);
            }
            if(this.gameObject.CompareTag("NewComponent"))
            {
                mainCanvas.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.SetActive(true);
            }  
        }
    }
    public void OnMouseUp()
    {
        carrying = false;
        cursor.setMouse();
        if(this.gameObject.CompareTag("Component"))
        {                                                                                                                                                                     
            spawnScript.newComp.Remove(this.gameObject);
            foreach (Button buttons in spawnScript.button)
			{
				buttons.interactable = true;
                buttons.GetComponent<Image>().raycastTarget = true;
                Destroy(this.gameObject);
			}
            mainCanvas.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.SetActive(false);
            Spawn.isAlreadyClicked = false;
        }
        if(this.gameObject.CompareTag("NewComponent"))
        {
            if(Spawn.isAlreadyClicked == false)
            {
                this.gameObject.transform.position = currentPosition;
                mainCanvas.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject.SetActive(false);
                foreach (Button buttons in spawnScript.button)
			    {
				    buttons.interactable = true;
                    buttons.GetComponent<Image>().raycastTarget = true;
			    }
            }
        }  
    }
    public void OnMouseExit()
    {
        cursor.setMouse();
    }
    public void RemoveComponent()
    {
        spawnScript.dictionary = JsonUtility.FromJson<ItemDictionary>(JsonFileReader.LoadJsonAsResource("Items/ItemDictionary.json"));
        //Debug.Log("Price for this component = RM"+float.Parse(this.gameObject.GetComponent<Text>().text));
        Debug.Log("hello");
        foreach (string dictionaryItem in spawnScript.dictionary.items)
        {
            string jsonDir = this.gameObject.GetComponent<Text>().text;
            if (dictionaryItem == jsonDir)
            {
                string myLoadedItem = JsonFileReader.LoadJsonAsResource(dictionaryItem); //pinz
                Item myItem = JsonUtility.FromJson<Item>(myLoadedItem); //pinz
                Debug.Log(myItem.itemID); //pinz
                Debug.Log(myItem.itemPrice); //pinz
                string json_price; //pinz
                foreach(var key in spawnScript.RepeatedComponentCount.Keys.ToList())//add dictionary to temporary list
                {
                    if(key == this.gameObject.name)
                    {
                        int count = spawnScript.RepeatedComponentCount[key];
                        int otherCount = 0;
                        if(count>1)//if there are more than one type of components, remove one
                        {
                            otherCount+=spawnScript.RepeatedComponentCount[key];
                            otherCount--;
                            spawnScript.RepeatedComponentCount[key] = otherCount;
                        }
                        else if(spawnScript.RepeatedComponentCount[key] > 0)//if there are only one type of component attached, remove it
                        {
                            spawnScript.RepeatedComponentCount.Remove(key);
                        }
                    }
                }
                foreach(var key in spawnScript.ItemPrices.Keys.ToList())
                {
                    json_price = myItem.itemPrice; //pinz
                    if(key == this.gameObject.name)
                    {
                        float totalCount = 0.0f;
                        totalCount = float.Parse(json_price);
                        totalCount=spawnScript.ItemPrices[key]-float.Parse(json_price);
                        spawnScript.ItemPrices[key] = totalCount;
                    }
                    if(spawnScript.ItemPrices[key] == 0.0f)
                    {
                        spawnScript.ItemPrices.Remove(key);
                    }
                }
                spawnScript.stringBuilder.Length = 0;
		        spawnScript.stringBuilderCount.Length = 0;
		        spawnScript.stringBuilderCountPrice.Length = 0;
                foreach (KeyValuePair<string,int>entry in spawnScript.RepeatedComponentCount)
		        {
			        spawnScript.stringBuilder.AppendLine().Append(entry.Key);
			        spawnScript.stringBuilderCount.AppendLine().Append(entry.Value);
			        spawnScript.stringBuilder.AppendLine();
			        spawnScript.stringBuilderCount.AppendLine();
		        }
		        foreach(KeyValuePair<string,float>entry in spawnScript.ItemPrices)
		        {
			        spawnScript.stringBuilderCountPrice.AppendLine().Append("RM"+entry.Value.ToString("F2"));
			        spawnScript.stringBuilderCountPrice.AppendLine();
		        }
                spawnScript.componentName.text = spawnScript.stringBuilder.ToString();
		        spawnScript.eachComponentQuantity.text = spawnScript.stringBuilderCount.ToString();
		        spawnScript.eachItemPrice.text =spawnScript.stringBuilderCountPrice.ToString();
                spawnScript.newComp.Remove(this.gameObject);
                spawnScript.newCompName.Remove(this.gameObject.name);
                cursor.setMouse();
            }
        }
        spawnScript.PassSubmitToWeb(spawnScript.RepeatedComponentCount); //pinz
    }
}