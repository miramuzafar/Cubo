using UnityEngine;
using System.Collections;

public class DragObjectPlace : MonoBehaviour
{

    [System.Serializable]
    public class PlaceSelect
    {
        public Color32 selectColor;
        public Color32 busyColor;

        public void Select(PlaceBehaviour place)
        {
            MeshRenderer mesh = place.GetComponent<MeshRenderer>();
            mesh.enabled = true;
            if (place.currentObject == null)
                mesh.material.color = selectColor;
            else
                mesh.material.color = busyColor;
        }
        public void Deselect(PlaceBehaviour place)
        {
            place.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    public GameObject objectInDrag;
    Transform floor;
    Transform roof;
    public PlaceObject placeObject;
    public PlaceSelect placeSelect;

    private PlaceBehaviour lastPlaceSelected;
    
    void Start()
    {
        placeObject = gameObject.GetComponent<PlaceObject>();
        floor = GameObject.Find("Floor").GetComponent<Transform>();
        roof = GameObject.Find("Roof").GetComponent<Transform>();
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfor;
        if (Physics.Raycast(ray, out hitInfor))
        {
            objectInDrag.SetActive(true);
            if (hitInfor.collider.GetComponent<PlaceBehaviour>() != null)
            {
                if (lastPlaceSelected != null)
                    placeSelect.Deselect(lastPlaceSelected);

                lastPlaceSelected = hitInfor.collider.GetComponent<PlaceBehaviour>();
                placeSelect.Select(lastPlaceSelected);

                if (Input.GetMouseButtonDown(0))
                {
                    if (lastPlaceSelected.currentObject != null)
                        return;

                    GameObject objectInPlace = Instantiate(objectInDrag) as GameObject;
                    objectInPlace.transform.position = objectInDrag.transform.position;
                    lastPlaceSelected.currentObject = objectInPlace;
                    objectInPlace.transform.parent = lastPlaceSelected.transform;
                    objectInPlace.gameObject.layer = 8;
                }
            }

            float yPosition = hitInfor.collider.transform.position.y;
            float zPosition = hitInfor.collider.transform.position.z;
            float yFloor = floor.gameObject.GetComponent<MeshRenderer>().bounds.max.y/1.4f;
            float yRoof = floor.gameObject.GetComponent<MeshRenderer>().bounds.min.y/1.4f;

            yPosition = Mathf.Clamp(yPosition, placeObject.baseGrid.gameObject.GetComponent<MeshRenderer>().bounds.min.y - yFloor, placeObject.baseGrid.gameObject.GetComponent<MeshRenderer>().bounds.max.y + yRoof);
            zPosition = Mathf.Clamp(zPosition, placeObject.baseGrid.gameObject.GetComponent<MeshRenderer>().bounds.min.z/2, placeObject.baseGrid.gameObject.GetComponent<MeshRenderer>().bounds.max.z/2);

            float offX = placeObject.baseGrid.gameObject.transform.position.x + ((placeObject.baseGrid.gameObject.GetComponent<MeshRenderer>().bounds.size.x)/2f - (objectInDrag.gameObject.GetComponent<Collider>().bounds.size.x)/1.3f);
            Vector3 pos = new Vector3(offX, yPosition, zPosition);
            objectInDrag.transform.position = pos;
        }
        else
        {
            objectInDrag.SetActive(false);
        }
    }
}
