using UnityEngine;
using System.Collections;

public class Selected : StateMachineBehaviour {

//	public float RatioScaleLevelRoot_c = 0.5f;
	private float RatioScaleLevel_c = 1.0f;
	private float DistanceBetweenEachLevelInStack_c = 5;

	private SebLevelActivator otherScript;
	private float      bbb;//A_pproachRatio_0_to_1; // value for bug tracking
	private float Ratio {
		get{ return bbb; }
	}
	private float      PreviousApproachRatio_0_to_1 = 0.0f;

	private GameObject Level1GameObject = null;
	private Vector3    Level1InitialLocalPosition = new Vector3 (-101, -101, -101); // value for bug tracking
	private Vector3    Level1InitialLocalScale = new Vector3 (-101, -101, -101); // value for bug tracking

	private GameObject ThisLevelGameObject = null;
	private string     ThisLevelName = "string not initialized yet";
	private Vector3    ThisLevelInitialLocalPosition = new Vector3 (-101, -101, -101); // value for bug tracking
	private Vector3    ThisLevelInitialLocalScale = new Vector3 (-101, -101, -101); // value for bug tracking
	private int        ThisLevelDepth;

	private GameObject ParentGameObject = null;

	private bool       ThisIsTheLevel1;


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
			// TOO MUCH DEBUG INFO Debug.Log("InitialLocal value of \"" +  ThisLevelName + "\" (depth=" + ThisLevelDepth + "): Position=" + ThisLevelInitialLocalPosition + " Scale=" + ThisLevelInitialLocalScale);
		}

		// get info about parent of this level
		ParentGameObject = GetParentOfThisLevel ();

		// special flag when this is the "LEVEL 1"
		if (ThisLevelGameObject == Level1GameObject) {
			ThisIsTheLevel1 = true;
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


		// Do something only if this is not applied to "Level 1"
		if (!ThisIsTheLevel1) {

			// do the moves only if the ratio changed
			if (bbb != PreviousApproachRatio_0_to_1) {
				PositionLevel1AndSelectedForThisRatio(bbb);
/* to delete because replaced by a function call
				// move the level 1
				Vector3 newPosition = Level1InitialLocalPosition;
				newPosition.z = Level1InitialLocalPosition.z + DistanceBetweenEachLevelInStack_c * (ThisLevelDepth + bbb - 1.0f);
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
				// TOO MUCH DEBUG INFO  Debug.Log ("New ratio: " + bbb + ", scale: " + newScale.x);

				// update for next frame
				PreviousApproachRatio_0_to_1 = bbb;
to delete because replaced by a function call */
			}
		}

	}

	void PositionLevel1AndSelectedForThisRatio(float Zero_to_one) {
		// move the level 1
		Vector3 newPosition = Level1InitialLocalPosition;
		newPosition.z = Level1InitialLocalPosition.z + DistanceBetweenEachLevelInStack_c * (ThisLevelDepth + Zero_to_one - 1.0f);
		Level1GameObject.transform.localPosition = newPosition;
		
		// move & scale the level itself
		// Debug.Log ("At local position :" + ThisLevelGameObject.transform.localPosition);
		newPosition = ThisLevelInitialLocalPosition;
		newPosition.x = Mathf.Lerp (ThisLevelInitialLocalPosition.x, 0, Zero_to_one);
		newPosition.y = Mathf.Lerp (ThisLevelInitialLocalPosition.y, 0, Zero_to_one);
		newPosition.z = ThisLevelInitialLocalPosition.z + (- DistanceBetweenEachLevelInStack_c - ThisLevelInitialLocalPosition.z) * Zero_to_one;
		ThisLevelGameObject.transform.localPosition = newPosition;
		
		Vector3 newScale = ThisLevelInitialLocalScale;
		newScale.x = ThisLevelInitialLocalScale.x + (RatioScaleLevel_c - ThisLevelInitialLocalScale.x) * Zero_to_one;
		newScale.y = ThisLevelInitialLocalScale.y + (RatioScaleLevel_c - ThisLevelInitialLocalScale.x) * Zero_to_one;
		newScale.z = ThisLevelInitialLocalScale.z + (RatioScaleLevel_c - ThisLevelInitialLocalScale.x) * Zero_to_one;
		ThisLevelGameObject.transform.localScale = newScale;
		// TOO MUCH DEBUG INFO  Debug.Log ("New ratio: " + Zero_to_one + ", scale: " + newScale.x);
		
		// update for next frame
		PreviousApproachRatio_0_to_1 = Zero_to_one;
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
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		// this is called when the level is un selected.
		// Do something only if this is not applied to "Level 1"
		if (!ThisIsTheLevel1) {
			// Now needs to go to proper position
			bbb = animator.GetFloat ("Zero_to_one");
			if (bbb > 0.5f) {
				// move forward
				bbb = 1.0f;

				// Update the selectables (this level and sibling levels now become non-selectable)
				PutThisLevelAndAllSiblingLevels_Non_Selectable();

				// Update the selectables (sub levels of this level now become selectable)
				PutAllSubLevelsSelectable();

				SetCurrentActiveParentLevelGameObject (ThisLevelGameObject);

			} else {
				// move backward
				bbb = 0.0f;
				// Update the selectables (this level and sibling levels now become non-selectable)
				PutAllSubLevels_Non_Selectable();
				// Update the selectables (the parent level and its sibling levels now become selectable)
				PutThisLevelAndAllSiblingLevelsSelectable();

				SetCurrentActiveParentLevelGameObject (ParentGameObject);
			}
			PositionLevel1AndSelectedForThisRatio (bbb);
			animator.SetFloat ("Zero_to_one", bbb);
		}

	}

	void PutAllSubLevelsSelectable() {
		SetSelectStateOfAllSublevelsOf (ThisLevelGameObject, true);
	}
	void PutAllSubLevels_Non_Selectable() {
		SetSelectStateOfAllSublevelsOf (ThisLevelGameObject, false);
	}
	void PutThisLevelAndAllSiblingLevelsSelectable() {
		SetSelectStateOfAllSublevelsOf (ParentGameObject, true);
	}
	void PutThisLevelAndAllSiblingLevels_Non_Selectable() {
		SetSelectStateOfAllSublevelsOf (ParentGameObject, false);
	}
	void PutParentLevelAndAllSiblingLevelsSelectable() {
		GameObject GrandParentLevel = GetGrandParentOfThisLevel ();
		SetSelectStateOfAllSublevelsOf (GrandParentLevel, true);
	}
	void PutParentLevelAndAllSiblingLevels_Non_Selectable() {
		GameObject GrandParentLevel = GetGrandParentOfThisLevel ();
		SetSelectStateOfAllSublevelsOf (GrandParentLevel, false);
	}

	// silently ignore nulls
	void SetSelectStateOfAllSublevelsOf(GameObject LevelOnWhichSubLevelsWillBeAffected, bool StateForLevelsCollider) {
		if (LevelOnWhichSubLevelsWillBeAffected && LevelOnWhichSubLevelsWillBeAffected.transform) {
			foreach (Transform trans in LevelOnWhichSubLevelsWillBeAffected.transform) {
				Collider collider_subLevel = trans.gameObject.GetComponent<Collider> ();
				if (collider_subLevel) {
					collider_subLevel.enabled = StateForLevelsCollider;
					//Debug.Log ("Setting " + collider_subLevel.name + ".enabled = " + (StateForLevelsCollider ? "true" : "false"));
				} else {
					// normal situation. Simply ignore.
				}
			}
		}
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}


	void SetCurrentActiveParentLevelGameObject(GameObject go) {
		if (go != null) {
			GameObject GlobalInfoGameObject = GameObject.Find ("GlobalInfo");
			SebGlobalInfoScript Script = GlobalInfoGameObject.GetComponent<SebGlobalInfoScript> ();
			Script.CurrentActiveParentLevelGameObject = go;
		}
	}

	GameObject GetGrandParentOfThisLevel() {
		return GetRelativeParentLevelOf (GetRelativeParentLevelOf (ThisLevelGameObject));
	}
	GameObject GetParentOfThisLevel() {
		return GetRelativeParentLevelOf (ThisLevelGameObject);
	}
	GameObject GetRelativeParentLevelOf(GameObject LevelOfWhichWeWantTheParent) {
		GameObject RelativeParent = null;
		if (LevelOfWhichWeWantTheParent &&
		    LevelOfWhichWeWantTheParent.transform &&
		    LevelOfWhichWeWantTheParent.transform.parent) {
			RelativeParent = LevelOfWhichWeWantTheParent.transform.parent.gameObject;
		}
		return RelativeParent;
	}


}
