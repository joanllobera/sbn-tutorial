using UnityEngine;
using System.Collections;

public class Meteorite : MonoBehaviour {

	private float startTime;
	public float lifeTime;
	public float secondsAlive;
	
    private Vector3 initialScale;

	// Use this for initialization
	void Start () 
	{
        initialScale = transform.localScale;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
        //Rigidbody rb = gameObject.GetComponent<Rigidbody>();
       
		secondsAlive = Time.time - startTime;


		if(  secondsAlive >= lifeTime|| (transform.position.y < -2 ) )
		{
            MeteoriteSpawner m= transform.parent.GetComponent<MeteoriteSpawner>();
            m.oneLess();
			DestroyImmediate (gameObject);
            
		}


	}


    void OnCollisionEnter(Collision collision) {

        if( gameObject.tag.Equals("falling"))
        {
            gameObject.tag = "pickme";
        }

        if (collision.gameObject.tag.Equals("blueRamp"))
        {
            gameObject.tag = "blueRamp";
        }
        else if (collision.gameObject.tag.Equals("redRamp"))
        {
            gameObject.tag = "redRamp";
        }

        
    }


}
