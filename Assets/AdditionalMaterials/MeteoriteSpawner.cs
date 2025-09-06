using UnityEngine;
using System.Collections;

public class MeteoriteSpawner : MonoBehaviour {

    public
    float repeatRate=4.4f;
    public
    float maxNum = 50;
    int count = 0;
	void Start () {
       // target =(GameObject) GameObject.Find("Lightning Emitter");
	}
	
	// Update is called once per frame
	void Update () 
	{
        InvokeRepeating("generateMeteorite", 5.0f, repeatRate);
	}

    public void oneLess()
    {
        if (count > 0)
            count--;


    }

    void generateMeteorite(){

        if (count < maxNum) {
            count++;
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            Meteorite mlt = sphere.AddComponent<Meteorite>();
            //mlt.lifeTime = Random.Range(50.5f, 100f);
            mlt.lifeTime = Mathf.Infinity;

            float size = Random.Range(0.75f, 1.5f);
                
            sphere.transform.localScale = new Vector3(size, size, size);
            sphere.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
            sphere.transform.position = transform.position + (Random.insideUnitSphere * 20);
            Rigidbody rb = sphere.AddComponent<Rigidbody>();
            rb.drag = 5;
            sphere.GetComponent<SphereCollider>().radius = size / 2;
            sphere.tag = "falling";
      
            sphere.transform.parent = transform;
        }

    }


}
