﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropScript : MonoBehaviour
{
    //Initialize Variables
    GameObject getTarget;
    Ray ray;
    bool isMouseDragging;
    private Plane plane;
    public Camera mainCamera;
    private Vector3 offset;

    // Use this for initialization
    void LateUpdate()
    {
        //Mouse Button Press Down
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            getTarget = ReturnClickedObject(out hitInfo);
            if (getTarget != null)
            {
                getTarget.transform.position = new Vector3(getTarget.transform.position.x,getTarget.transform.position.y,getTarget.transform.position.z);
                isMouseDragging = true;
            }
        }
        //Mouse Button Up
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
        }
        //Is mouse Moving
        if (isMouseDragging)
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            float dist;
            plane.Raycast(ray, out dist);
            Vector3 v3Pos = ray.GetPoint(dist);  
            //tracking mouse position.
            if(getTarget.CompareTag("LeftRightWall") && getTarget.transform.childCount == 1)
            {
                float translate = getTarget.gameObject.transform.position.x - getTarget.gameObject.transform.GetChild(0).gameObject.transform.position.x;
                Debug.Log("value :"+getTarget.transform.position.x);
                foreach (GameObject frontWall in GameObject.FindGameObjectsWithTag("FrontWall"))
                {
                    Vector3 v3Scale = frontWall.gameObject.transform.localScale;
                    float scaleFactor = (translate/v3Scale.x);
                    frontWall.gameObject.transform.localScale = new Vector3(v3Scale.x*scaleFactor, v3Scale.y, v3Scale.z);
                }
                foreach (GameObject roofAndFloor in GameObject.FindGameObjectsWithTag("Floor"))
                {
                    Vector3 v3Scale = roofAndFloor.gameObject.transform.localScale;
                    float scaleFactor = translate/v3Scale.x;
                    roofAndFloor.gameObject.transform.localScale = new Vector3(roofAndFloor.transform.localScale.x*scaleFactor, roofAndFloor.transform.localScale.y, roofAndFloor.transform.localScale.z);
                }
                v3Pos.y = getTarget.gameObject.transform.localPosition.y;
                v3Pos.z = getTarget.gameObject.transform.localPosition.z;
                getTarget.gameObject.transform.position = new Vector3(v3Pos.x, getTarget.gameObject.transform.localPosition.y, getTarget.gameObject.transform.localPosition.z);
                getTarget.gameObject.transform.GetChild(0).gameObject.transform.position = new Vector3(-v3Pos.x, getTarget.gameObject.transform.localPosition.y, getTarget.gameObject.transform.localPosition.z);
            }   
            if(getTarget.CompareTag("FrontWall") && getTarget.transform.childCount == 1)
            {
                float translate = getTarget.transform.localPosition.z - getTarget.transform.GetChild(0).gameObject.transform.position.z;
                foreach (GameObject leftRightWall in GameObject.FindGameObjectsWithTag("LeftRightWall"))
                {
                    Vector3 v3Scale = leftRightWall.transform.localScale;
                    float scaleFactor = (translate/v3Scale.z);
                    leftRightWall.transform.localScale = new Vector3(leftRightWall.transform.localScale.x, leftRightWall.transform.localScale.y, v3Scale.z*scaleFactor);
                    Debug.Log("clicked FrontWall");
                }
                foreach (GameObject roofAndFloor in GameObject.FindGameObjectsWithTag("Floor"))
                {
                    Vector3 v3Scale = roofAndFloor.transform.localScale;
                    float scaleFactor = (translate/v3Scale.z);
                    roofAndFloor.transform.localScale = new Vector3(roofAndFloor.transform.localScale.x, roofAndFloor.transform.localScale.y, v3Scale.z*scaleFactor);
                }
                v3Pos.x = getTarget.gameObject.transform.localPosition.x;
                v3Pos.y = getTarget.gameObject.transform.localPosition.y;
                getTarget.gameObject.transform.position = new Vector3(getTarget.gameObject.transform.localPosition.x, getTarget.gameObject.transform.localPosition.y, v3Pos.z);
                getTarget.gameObject.transform.GetChild(0).gameObject.transform.position = new Vector3(getTarget.gameObject.transform.localPosition.x, getTarget.gameObject.transform.localPosition.y, -v3Pos.z);
            }
        }
    }
    //Method to Return Clicked Object
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction*10, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }
}