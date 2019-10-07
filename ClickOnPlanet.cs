using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnPlanet : MonoBehaviour {

    // Use this for initialization

    //this is the marker prefab used to show the future position of the ship
    public GameObject prefabMarker;
    GameObject goMarker;
    
    //this is the slected ship in the ship scripts
    public GameObject selectedShip;
    
    //these are the variables that store the angular and cartesian data of the marker
    public float R, teta, phi, x, y, z;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void raycastOnPlanet()
    {
        // we use raycast from mouse to know the position of the marker.
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && selectedShip != null)
        {
            Vector3 hitpos = hit.point;
            Vector3 hitNormal = hit.normal;
            
            // we destroy the last marker
            destroyMarker();

            //we instanciate the new marker at the hit point
            goMarker = GameObject.Instantiate(prefabMarker, hitpos,Quaternion.Euler(0, 0, 0)) as GameObject;
            goMarker.transform.forward = hitNormal;
            goMarker.transform.SetParent(transform);

            //we update the coordinates and stablish the final position of the marker
            calculateAngularCoords(goMarker.transform.position);
            calculateSphereCoords();
            goMarker.transform.position = new Vector3(x,y,z);

            //this corrutine moves the ship towards the destination
            StartCoroutine(selectedShip.GetComponent<ShipMovement>().moveShip());

            //this is necesary to re-start the process
            selectedShip = null;
        }
    }

    public void calculateAngularCoords(Vector3 v)
    {
        x = v[0];
        y = v[1];
        z = v[2];

        R = Mathf.Pow(Mathf.Pow(x,2)+ Mathf.Pow(y, 2)+ Mathf.Pow(z, 2),0.5f);
        teta = Mathf.Acos(y/R);
        phi = Mathf.Atan2(z,x);

    }


    public void calculateSphereCoords()
    {
        x = R * Mathf.Sin(teta) * Mathf.Cos(phi);
        z = R * Mathf.Sin(teta) * Mathf.Sin(phi);
        y = R * Mathf.Cos(teta);


    }

    public void destroyMarker()
    {
        if (goMarker != null) { GameObject.Destroy(goMarker); }
    }
}
