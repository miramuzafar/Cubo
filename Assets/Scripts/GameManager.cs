using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	public GameObject mainMenu;
	public GameObject selectPlan;
	public GameObject plans;
	public Canvas build;
	List<GameObject> allComp = new List<GameObject>();
	public string[] allTags;
	//ComponentInformation componentInformation;

	void Start () 
	{
		selectPlan.SetActive(false);
		mainMenu.SetActive(true);
		//plans.SetActive(false);
		build.gameObject.transform.GetChild(2).gameObject.SetActive(false);
	}
	// Update is called once per frame
	public void StartDesign()
	{
		selectPlan.SetActive(true);
		mainMenu.SetActive(false);
	}
	public void SelectPlan(GameObject plan)
	{
		Debug.Log("selected "+ plan.gameObject.name);
		build.gameObject.transform.GetChild(2).gameObject.SetActive(true);
		plans = Instantiate(plan, plan.transform.position, plan.transform.rotation) as GameObject;
		if(plan != null)
		{
			plans.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
			plans.gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
			plans.gameObject.transform.DetachChildren();
		}
		selectPlan.SetActive(false);
	}
	public void Build()
	{
 		allComp = FindObjectsWithTags(allTags);
		foreach (GameObject comp in allComp)
		{
			comp.gameObject.transform.SetParent(plans.gameObject.transform);
			Debug.Log(comp.name);
		}
		plans.gameObject.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = true;
		plans.gameObject.transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().enabled = true;
		SceneManager.LoadScene(1);
	}
	List<GameObject> FindObjectsWithTags(string[] tags)
	{
		var combinedList = new List<GameObject>();
		for (int i = 0; i < tags.Length; i++)
		{
			var taggedObjects = GameObject.FindGameObjectsWithTag(tags[i]);
			combinedList.AddRange(taggedObjects);
		}
		return combinedList;
	}
}
 