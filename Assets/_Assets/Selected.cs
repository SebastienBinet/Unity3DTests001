using UnityEngine;
using System.Collections;

public class Selected : StateMachineBehaviour {

//	public float RatioScaleLevelRoot_c = 0.5f;
	public float RatioScaleLevel_c = 1.0f;
	public float DistanceBetweenEachLevelInStack_c = 5;

	public SebLevelActivator otherScript;
	public float      bbb;//A_pproachRatio_0_to_1; // value for bug tracking
	public float Ratio {
		get{ return bbb; }
	}
	public float      PreviousApproachRatio_0_to_1 = 0.0f;

	private GameObject Level1GameObject = null;
	public Vector3    Level1InitialLocalPosition = new Vector3 (-101, -101, -101); // value for bug tracking
	public Vector3    Level1InitialLocalScale = new Vector3 (-101, -101, -101); // value for bug tracking

	private GameObject ThisLevelGameObject = null;
	public string     ThisLevelName = "string not initialized yet";
	public Vector3    ThisLevelInitialLocalPosition = new Vector3 (-101, -101, -101); // value for bug tracking
	public Vector3    ThisLevelInitialLocalScale = new Vector3 (-101, -101, -101); // value for bug tracking
	public int        ThisLevelDepth;


	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Debug.Log ("Entering state (in script Selected)");

		// get info about root level
		Level1GameObject = GameObject.Find("Level 1");
		Animator RootAnimator = Level1GameObject.GetComponent<Animator>();
		SebLevelActivator RootAnimatorScript = RootAnimator.GetComponent<SebLevelActivator>();
		Level1InitialLocalPosition =  RootAnimatorScript.InitialLocalPosition;
		Level1InitialLocalScale    =  RootAnimatorScript.InitialLocalScale;

		// get info about this level
		otherScript = animator.GetComponent<SebLevelActivator>();
		if (! otherScript) {
			Debug.Log("otherScript Not Found !! =========================");
		} else {
			//Debug.Log("Found");
			ThisLevelInitialLocalPosition = otherScript.InitialLocalPosition;
			ThisLevelInitialLocalScale = otherScript.InitialLocalScale;
			ThisLevelGameObject = otherScript.gameObject;
			ThisLevelName = ThisLevelGameObject.name;
			ThisLevelDepth = FindThisLevelDepth();
			Debug.Log("InitialLocal value of \"" +  ThisLevelName + "\" (depth=" + ThisLevelDepth + "): Position=" + ThisLevelInitialLocalPosition + " Scale=" + ThisLevelInitialLocalScale);
		}


		// some inits
		bbb = -101.0f;

		// asserts
		if (! Level1GameObject) {			Debug.Log("========== BUG: Not Found -tresdfgretw !!");		} 
		if (! ThisLevelGameObject) {		Debug.Log("========== BUG: Not Found -hjyj5j55j !!");		} 

	}


	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		bbb = animator.GetFloat ("Zero_to_one");
//		Debug.Log ("New ratio: " + bbb);

		// todelete get GameObject references
		// todelete Level1GameObject = GameObject.Find("Level 1");
		// todelete ThisLevelGameObject = GameObject.Find("Level 1/Level 1.2");

		// todelete // assert
		// todelete if (! Level1GameObject) {			Debug.Log("Not Found -tresdfgretw !!");		} 
		// todelete if (! ThisLevelGameObject) {		Debug.Log("Not Found -hjyj5j55j !!");		} 


		// do the moves only if the ratio changed
		if (bbb != PreviousApproachRatio_0_to_1) {

			// move the level 1
			Vector3 newPosition = Level1InitialLocalPosition;
			newPosition.z = Level1InitialLocalPosition.z + DistanceBetweenEachLevelInStack_c  * (ThisLevelDepth + bbb - 1.0f);
			Level1GameObject.transform.localPosition = newPosition;

//			Vector3 newScale = Level1InitialLocalScale;
//			newScale.x = Mathf.Lerp (Level1InitialLocalScale.x, Level1InitialLocalScale.x * RatioScaleLevelRoot_c, bbb);
//			newScale.y = Mathf.Lerp (Level1InitialLocalScale.y, Level1InitialLocalScale.x * RatioScaleLevelRoot_c, bbb);
//			newScale.z = Mathf.Lerp (Level1InitialLocalScale.z, Level1InitialLocalScale.x * RatioScaleLevelRoot_c, bbb);
//			Level1GameObject.transform.localScale = newScale;

			
			// move & scale the level itself
			// Debug.Log ("At local position :" + ThisLevelGameObject.transform.localPosition);
			newPosition = ThisLevelInitialLocalPosition;
			newPosition.x = Mathf.Lerp (ThisLevelInitialLocalPosition.x, 0, bbb);
			newPosition.y = Mathf.Lerp (ThisLevelInitialLocalPosition.y, 0, bbb);
			newPosition.z = ThisLevelInitialLocalPosition.z + (- DistanceBetweenEachLevelInStack_c - ThisLevelInitialLocalPosition.z) * bbb;
			ThisLevelGameObject.transform.localPosition = newPosition;

			Vector3 newScale = ThisLevelInitialLocalScale;
			newScale.x = ThisLevelInitialLocalScale.x + (RatioScaleLevel_c - ThisLevelInitialLocalScale.x) * bbb;
			newScale.y = ThisLevelInitialLocalScale.y + (RatioScaleLevel_c - ThisLevelInitialLocalScale.x) * bbb;
			newScale.z = ThisLevelInitialLocalScale.z + (RatioScaleLevel_c - ThisLevelInitialLocalScale.x) * bbb;
			ThisLevelGameObject.transform.localScale = newScale;
			Debug.Log ("New ratio: " + bbb + ", scale: " + newScale.x);

			// update for next frame
			PreviousApproachRatio_0_to_1 = bbb;
		}

	}

	int FindThisLevelDepth(GameObject GO = null) {
		// start with this level then recurse
		if (!GO) {
			GO = ThisLevelGameObject;
		}
		int depth = 1;
		if (GO && GO.transform && GO.transform.parent && GO.transform.parent.name != "Level 1") {
			depth = FindThisLevelDepth(GO.transform.parent.gameObject) + 1;
		}
		return depth;
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
