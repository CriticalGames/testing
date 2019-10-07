using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShipMovement : MonoBehaviour {

    // Use this for initialization

    //planet script
    public ClickOnPlanet planetScript;
    //image square selectable object
    public Image im;
    //coordinate angles
    public float phi1, teta1;
    float phi0, teta0;
    //orbit of the ship
    public float Rorbit;
    // speed of the movement
    public float movementDivisions;
    // spere reference
    public Transform sphere;

	void Start ()
    {
        // initial spherical coordinates
        float x = Rorbit * Mathf.Sin(teta1) * Mathf.Cos(phi1);
        float y = Rorbit * Mathf.Sin(teta1) * Mathf.Sin(phi1);
        float z = Rorbit * Mathf.Cos(teta1);


        transform.position = new Vector3(x, y, z);

        StartCoroutine(moveShip());
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        /*if (objective!=null)
        {
            transform.position = Vector3.Lerp(transform.position, objective.position, 0.2f);
        }

        */
        if (planetScript.selectedShip !=null)
        {
            im.enabled = planetScript.selectedShip.transform.gameObject == gameObject;
        }
        else
        {
            im.enabled = false;
        }
    }

   // corrutine to move ship
    public IEnumerator moveShip()
    {

        //save last angles
        phi0 =phi1;
        teta0 = teta1;
        
        //determine new angles
        phi1 = planetScript.phi ;
        teta1= planetScript.teta;

        //intermediate angles
        float teta =0;
        float phi = 0;

        for (float f =1; f <= movementDivisions; f +=1)
        {
            teta = teta0 + (teta1-teta0)/ movementDivisions*f;
            phi = phi0 + (phi1 - phi0) / movementDivisions * f;

            //float teta = Mathf.Atan(1 / Mathf.Cos(f - phi0));

            //spehrical coordinates
            float x = Rorbit * Mathf.Sin(teta) * Mathf.Cos(phi);
            float z = Rorbit * Mathf.Sin(teta) * Mathf.Sin(phi);
            float y = Rorbit * Mathf.Cos(teta);

           //set the position of the ship to the coordiantes
            transform.position = new Vector3(x, y, z);
            //set the rotation too
            transform.up=(sphere.position)-transform.position;


            yield return null;
        }


         //destoy marker
        planetScript.destroyMarker();
    }

    //this is called when clicking on object
    public void selectShip()
    {
        planetScript.selectedShip = gameObject;
    }
}
