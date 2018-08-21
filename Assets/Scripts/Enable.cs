using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Enable : MonoBehaviour {

	// Use this for initialization
	GameObject spawn;
	Spawn spawnScript;
	InputManager inputManager;
	OverlapSphereScript overlapSphereScript;

	 void OnDrawGizmos() 
	{
		Gizmos.color = Color.white;
        Gizmos.DrawWireCube(this.gameObject.transform.position, this.gameObject.transform.GetComponent<Collider>().bounds.size);
    }  
	void OnTriggerStay(Collider c)
 	{
		spawn = GameObject.Find("AppManager");
		inputManager = c.gameObject.GetComponent<InputManager>();
		spawnScript = spawn.GetComponent<Spawn>();
		overlapSphereScript = c.gameObject.GetComponent<OverlapSphereScript>();
		if(Input.GetMouseButtonUp(0))
		{
			//Debug.Log("released");
			if((c.gameObject.layer == 8)&&(overlapSphereScript.overlapped == false)&&(this.gameObject.CompareTag("Starterslots")))
			{
			//InputManager.carrying = true;
				if(this.gameObject.transform.childCount<2)//if there is no other component attached to the same slot
     			{
					if(InputManager.collide==false)
					{
						if(c.gameObject.CompareTag("Component"))
						{
							//InputManager.collide = true;
							Vector3 currpos;
							c.gameObject.transform.SetParent(this.gameObject.transform);//make the component a child to the slot
							//c.gameObject.GetComponent<Collider>().isTrigger = false;
							//c.gameObject.transform.rotation = this.transform.rotation * Quaternion.Euler(0,0,0);//set the rotation of the component based on the current rotation of the parent slot
							c.transform.localPosition = new Vector3(0,0,0);//set the local vector of the component to zero
							currpos = c.gameObject.transform.parent.position;
							inputManager.currentPosition = currpos;
							//c.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;//disable the rigidbody of the component
							Spawn.isAlreadyClicked = false;
							if(Spawn.isAlreadyClicked == false)
							{
								spawnScript.newComp.Add(c.gameObject);
								spawnScript.ComponentAdded();
							}
						}
						if(c.gameObject.transform.CompareTag("NewComponent"))
						{
							//InputManager.collide = true;
							Vector3 currpos;
							c.gameObject.transform.SetParent(this.gameObject.transform);//make the component a child to the slot
							//c.gameObject.GetComponent<Collider>().isTrigger = false;
							//c.gameObject.transform.rotation = this.transform.rotation * Quaternion.Euler(0,0,0);//set the rotation of the component based on the current rotation of the parent slot
							c.transform.localPosition = new Vector3(0,0,0);//set the local vector of the component to zero
							currpos = c.gameObject.transform.parent.position;
							inputManager.currentPosition = currpos;
							//c.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;//disable the rigidbody of the component
							Spawn.isAlreadyClicked = false;
						}
					}	
				}
			} 
		}
	}	
}
