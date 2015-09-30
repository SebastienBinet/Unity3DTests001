using UnityEngine;
using System.Collections;

public class SebObjectTeddyInFire : SebObjectHolderDefaultBehaviorScript {

	public GameObject FireGameObject;
	public GameObject FxFireGameObject;
	public ParticleSystem FxFireParticleSystem;
	private Transform tempTransform;

//	public SebObjectTeddyInFire thisScript;
	// Use this for initialization
	override public void Start () {
		base.Start();
//		tempTransform = ParentGameObject.transform.Find("ObjectHolder");
//		Debug.Log ("asasas: " + tempTransform);
//		FireGameObject = tempTransform.gameObject;
//		Debug.Log ("hghg: " + FireGameObject);

		// find child Fire
		tempTransform = ThisLevelGameObject.transform.Find("Fire");
		Debug.Log ("asasas2: " + tempTransform);
		if (tempTransform) {
			FireGameObject = tempTransform.gameObject;
			Debug.Log ("iuiuiu2: " + FireGameObject);
			// find child Fire.fx_fire
			tempTransform = FireGameObject.transform.Find("fx_fire");
			Debug.Log ("asasas3: " + tempTransform);
			if (tempTransform) {
				FxFireGameObject = tempTransform.gameObject;
				Debug.Log ("iuiuiu3: " + FxFireGameObject);
				// find the component fx_fire."Particle System"
				FxFireParticleSystem = FxFireGameObject.GetComponent<ParticleSystem>();
				Debug.Log ("oopooo3: " + FxFireParticleSystem);
			} else {
				// assert because bug
				Debug.LogError ("========== BUG jjujynhhgg");
			}
		} else {
			// assert because bug
			Debug.LogError ("========== BUG iuiyuy");
		}


	}
	
	// Update is called once per frame
	override public void Update () {
		if (ParentGameObject) {
			// TOO MUCH DEBUG INFO Debug.Log ("Now in SebObjectTeddyInFire.cs: " + ParentGameObjectName + "." + ThisGameObjectName + ".update()");
		} else {
			// TOO MUCH DEBUG INFO Debug.Log ("Now in SebObjectTeddyInFire.cs: " + "NULL." + ThisGameObjectName + ".update()");
		}

		// update the fire parameters in function of Level size
		if (ParentGameObject) {

			Vector3 currentScale = ParentGameObject.transform.localScale;
//			Vector3 currentScale = ThisLevelGameObject.transform.localScale;

			SebLevelActivator RootAnimatorScript = ParentGameObject.transform.GetComponent<SebLevelActivator>();
			
			Vector3 InitialScale = RootAnimatorScript.InitialLocalScale;
			float ratioCurrentScaleOverInitialScale = currentScale.x / InitialScale.x;
			// TOO MUCH DEBUG INFO Debug.Log ("current ratio=" + ratioCurrentScaleOverInitialScale);
			if (FxFireParticleSystem) {
				FxFireParticleSystem.startSize = 0.57f * ratioCurrentScaleOverInitialScale;
			} else {
				// assert because bug
				Debug.LogError ("========== BUG ghgfjjfghj");
			}


//		TBD: useGUILayout the ratio Touch ChangeMaterialOnGrab the Fire. fx_fire. startsize
		} else {
			// assert because bug
			Debug.LogError ("========== BUG fdasetrgfds");
		}
	}
}
