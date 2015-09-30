using UnityEngine;
using System.Collections;

public class ObjectHolderDefaultBehaviorScript : MonoBehaviour {

	public GameObject ThisLevelGameObject = null;
	public ObjectHolderDefaultBehaviorScript thisScript;
	public string ThisGameObjectName = "";
	// parent info
	public GameObject ParentGameObject = null;
	public string ParentGameObjectName = ""; 



	// Use this for initialization
	virtual public void Start () {

		// find this Gameobject, which is of type "ObjectHolder" or a derived one.
		thisScript = GetComponent<ObjectHolderDefaultBehaviorScript>();
		ThisLevelGameObject = thisScript.gameObject;
		ThisGameObjectName = ThisLevelGameObject.name;
		// parent info
		if (ThisLevelGameObject.transform.parent) {
			ParentGameObject = ThisLevelGameObject.transform.parent.gameObject;
			ParentGameObjectName = ParentGameObject.name;
		} else {
			ParentGameObjectName = "THERE IS NO PARENT TO THIS GAME OBJECT";
		}
	}
	
	// Update is called once per frame
	virtual public void Update () {
//		static bool firstTimeInUpdate = true;
//		if (firstTimeInUpdate) {
		if (ParentGameObject) {
			// TOO MUCH DEBUG INFO Debug.Log ("No Behavior in " + ParentGameObjectName + "." + ThisGameObjectName + ".update()");
		} else {
			// TOO MUCH DEBUG INFO Debug.Log ("No Behavior in " + "NULL." + ThisGameObjectName + ".update()");
		}

		// do not repeat the singleton
//		firstTimeInUpdate = false;
	}
}
