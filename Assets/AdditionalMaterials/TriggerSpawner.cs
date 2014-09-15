using UnityEngine;
using System.Collections;

using timepath4unity;

public class TriggerSpawner : MonoBehaviour {
	

	bool once=true;
	Spawner s;
	// Use this for initialization
	void Start () {
		s =(Spawner) this.GetComponent (typeof(Spawner));
		s.BeginSpawning ();
	
	}
	
	// Update is called once per frame
	void Update () {

        
		if (once && s.transform.GetComponentsInChildren<Transform> ().Length >= s.SpawnCount) {
			Debug.Log ("we have spawned the small cans");			
			TPAgent[] agents = Resources.FindObjectsOfTypeAll<TPAgent>();
			foreach(TPAgent a in agents)
				a.gameObject.SetActive(true);
			once = false;
		}
	}
}
