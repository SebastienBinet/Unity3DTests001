using UnityEngine;
using System.Collections;

public class SebObjectTeddyInFire : ObjectHolderDefaultBehaviorScript {


//	public SebObjectTeddyInFire thisScript;
	// Use this for initialization
	override public void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	override public void Update () {
		if (ParentGameObject) {
			Debug.Log ("Now in SebObjectTeddyInFire.cs: " + ParentGameObjectName + "." + ThisGameObjectName + ".update()");
		} else {
			Debug.Log ("Now in SebObjectTeddyInFire.cs: " + "NULL." + ThisGameObjectName + ".update()");
		}

		// update the fire parameters in function of Level size
		if (ParentGameObject) {

			Vector3 currentScale = ParentGameObject.transform.localScale;
//			Vector3 currentScale = ThisLevelGameObject.transform.localScale;

			SebLevelActivator RootAnimatorScript = ParentGameObject.transform.GetComponent<SebLevelActivator>();
			
			Vector3 InitialScale = RootAnimatorScript.InitialLocalScale;
			float ratioCurrentScaleOverInitialScale = currentScale.x / InitialScale.x;
			Debug.Log ("current ratio=" + ratioCurrentScaleOverInitialScale);


		TBD: useGUILayout the ratio Touch ChangeMaterialOnGrab the Fire. fx_fire. startsize
		} else {
			// assert because bug
			Debug.LogError ("ERROR fdasetrgfds");
		}
	}
}
