using UnityEngine;
using System.Collections;

public class GlobalInfoScript : MonoBehaviour {

	public GameObject CurrentActiveParentLevelGameObject = null;

	virtual public void Start () {
		// set to top level
		CurrentActiveParentLevelGameObject = GameObject.Find("Level 1");
	}
}
