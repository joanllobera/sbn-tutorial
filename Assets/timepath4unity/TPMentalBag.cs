using UnityEngine;
using System.Collections;


/*!
\brief
A handy component usefull to keep track of elements involved in the ongoing actions.
 A cognitive scientist would call this the working memory
 A Game AI programmer would call it a billboard  
 */ 

public class TPMentalBag : MonoBehaviour {

    public Meteorite M;
    public string destinationTag;

    protected float onDestination = 0.0f;

    public float OnDestination{get{return onDestination;}}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /*
    
    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("the tag I am in is:" + collision.collider.tag);

        if (gameObject.tag.Equals(destinationTag))
        {
            onDestination = 1.0f;
        }
        else
        {
            onDestination = 0.0f;

        }
    

    }



    void OnTriggerEnter(Collider c)
    {

        Debug.Log("the trigger is from object :" + c.tag);

        if (c.tag.Equals(destinationTag))
        {
            onDestination = 1.0f;
        }
        else
        {
            onDestination = 0.0f;

        }


    }*/





}
