using UnityEngine;
using System.Collections;

public class SebMouseInteractions : MonoBehaviour {
	private const float DRAG_RATIO_FOR_CHANGE_C = 1.0f;

	private GameObject ThisLevelGameObject = null;
	private GameObject SearchSpotlightInThisLevelGameObject = null;
	private GameObject FoundSpotlightInThisLevelGameObject = null;
	private SebMouseInteractions thisScript;
	public  bool Fire1JustPressed_oldWay;
	public  bool Fire2JustPressed_oldWay;
	private bool IsFire1CurrentlyPressed;
	private Vector3 MousePositionWhenClicked = new Vector3 (-101f, -101f, -101f);
	private bool IsMouseOverAGameObject;
	private RaycastHit hitIfThereIsOne;
	private Transform  transformHitPrevious_oldWay;
	private Transform  transformHitPrevious;


	// Use this for initialization
	void Start () {
		FindTheGameObjectWhereThisScriptIsPlaced ();
		FindTheSpotlightsUnderTheGameObjectWhereThisScriptIsPlaced ();
	}
	
	// Update is called once per frame
	void Update () {

		// get a ray to the pixel inder the mouse
		Ray rayToPixelUnderMouse = getRayToMousePixel ();

		// Draw a ray to mouse pixel in the scene view
		Debug.DrawRay(rayToPixelUnderMouse.origin, rayToPixelUnderMouse.direction * 5, Color.green, 0.1f, true);

		bool Fire1JustPressed = Input.GetButtonDown ("Fire1");
		bool Fire2JustPressed = Input.GetButtonDown ("Fire2");
		bool Fire1JustReleased = Input.GetButtonUp ("Fire1");
		bool Fire2JustReleased = Input.GetButtonUp ("Fire2");
		Vector3 CurrentMousePosition = Input.mousePosition;
		float drag;
		if (MousePositionWhenClicked != new Vector3 (-101f, -101f, -101f)) {
			drag = (CurrentMousePosition.y - MousePositionWhenClicked.y) / Screen.height;
		} else {
			drag = 0;
		}
		IsMouseOverAGameObject =  Physics.Raycast (rayToPixelUnderMouse, out hitIfThereIsOne);


		// if just clicked
		if (Fire1JustPressed) {
			// store current mouse position in memory
			IsFire1CurrentlyPressed = true;
			MousePositionWhenClicked = Input.mousePosition;
			// if level just clicked
			if (IsMouseOverAGameObject) {
				// send click event to level
				InformLevelThatFire1ClickedOnIt(hitIfThereIsOne.transform);
				// store level in memory
				transformHitPrevious = hitIfThereIsOne.transform;
			} else {
			// else: so it was just clicked, but not on level
				// send click event to parent
				InformParentThatFire1Clicked();

			}
		}

		// if stays click
		if (IsFire1CurrentlyPressed) {
			// if moved more far, send 1 to 0 to parent
			if (drag > 0) {
				SetParent1to0 (0.9f - drag * DRAG_RATIO_FOR_CHANGE_C);
				if (transformHitPrevious) {
					SetLevel0to1 (0);
				}
			} else  { // if (drag < 0) { // else: so it was moved closer
				// if level was clicked, send 0 to 1 to level
				if (transformHitPrevious) {
					SetLevel0to1 (0.05f + drag / (-DRAG_RATIO_FOR_CHANGE_C));
				}
				SetParent1to0 (1);
			}
		}

		// if click just released
		if (Fire1JustReleased) {

			if (transformHitPrevious) {
				InformSelectedLevelThatFire1Released();
			}
			// in all cases, parent needs to re-ajust
			InformParentThatFire1Released ();
			// if moved more far, send release event to parent
//			if (drag > 0) {
//				InformParentThatFire1Released ();
//			} else { // else: so it was moved closer
//			 if moved closer, send release event to level
//				InformSelectedLevelThatFire1Released();
//			}

			// reset current mouse position in memory
			IsFire1CurrentlyPressed = false;
			MousePositionWhenClicked = new Vector3(-101f,-101f, -101f);
			// reset level in memory
			transformHitPrevious = null;
		}


/*		/////////////////////////
		if (Fire1JustPressed) {
			MousePositionWhenClicked = Input.mousePosition;
			// select this level

			Debug.Log ("========== Fire1 =============");
			InformOveredThatFire1Clicked();
		}
		
		if (Fire2JustPressed_oldWay) {
			Debug.Log ("========== Fire2 =============");
		}
*/
		BeamTheSearchSpotlightToThisDirection (rayToPixelUnderMouse);

	


		if (IsMouseOverAGameObject) {
			BeamTheFoundSpotlightToThisDirection (new Ray(new Vector3(0.5f, 1.5f, 1.5f), hitIfThereIsOne.point - new Vector3(0.5f, 1.5f, 1.5f)));
//			processThereIsCurrentlyHovering(hitIfThereIsOne);
		} else {
			// put the beam behind us
			BeamTheFoundSpotlightToThisDirection (new Ray(new Vector3(0,0,0), new Vector3(0,0,-1)));
//			processThereIsCurrently_No_Hovering();
		}

	}



	Ray getRayToMousePixel() {
		// there are two ways that give the same result:
		// way 1
		Ray rayMethod1 = Camera.main.ScreenPointToRay (Input.mousePosition);

		//way 2
		Vector3 dirMethod2 = new Vector3 ((Input.mousePosition.x - (Screen.width / 2.0f)) / Screen.width, (Input.mousePosition.y - (Screen.height / 2.0f)) / Screen.height, 1.0f);
		Ray rayMethod2 = new Ray(new Vector3(0,0,0), dirMethod2);

		return rayMethod1;
	}

	void FindTheGameObjectWhereThisScriptIsPlaced() {
		thisScript = GetComponent<SebMouseInteractions>();
		ThisLevelGameObject = thisScript.gameObject;
	}

	void FindTheSpotlightsUnderTheGameObjectWhereThisScriptIsPlaced() {
		Transform tempTransform;

		// search spotlight info
		tempTransform = ThisLevelGameObject.transform.Find("SpotlightToMouse");
		if (tempTransform) {
			SearchSpotlightInThisLevelGameObject = tempTransform.gameObject;
		} else {
			// assert because bug
			Debug.LogError ("========== BUG qwewqehhg");
		}

		// found spotlight info
		tempTransform = ThisLevelGameObject.transform.Find("SpotLightOnSelection");
		if (tempTransform) {
			FoundSpotlightInThisLevelGameObject = tempTransform.gameObject;
		} else {
			// assert because bug
			Debug.LogError ("========== BUG iiuiiuggjk");
		}

	}

	void BeamTheSearchSpotlightToThisDirection(Ray theRay) {
		if (SearchSpotlightInThisLevelGameObject) {
			SearchSpotlightInThisLevelGameObject.transform.position = theRay.origin;
			SearchSpotlightInThisLevelGameObject.transform.forward = theRay.direction;
		}
	}

	void BeamTheFoundSpotlightToThisDirection(Ray theRay) {
		if (FoundSpotlightInThisLevelGameObject) {
			FoundSpotlightInThisLevelGameObject.transform.position = theRay.origin;
			FoundSpotlightInThisLevelGameObject.transform.forward = theRay.direction;
			Debug.DrawRay(theRay.origin, theRay.direction * 5, Color.cyan, 0.1f, true);
		}
	}
	void InformLevelThatFire1ClickedOnIt( Transform tr) {
		Debug.Log ("> Level clicked <");
		Animator _animator = tr.GetComponent <Animator> ();
		if (_animator) {
			_animator.SetTrigger ("SelectedTrigger");
		} else { 
			// assert because bug
			Debug.LogError ("========== BUG 657654567");
		}	
	}
	void InformSelectedLevelThatFire1Released() {
		Debug.Log ("> Level released <");
		if (transformHitPrevious) {
			Animator _animator = transformHitPrevious.GetComponent <Animator> ();
			if (_animator) {
				_animator.SetTrigger ("UnselectedTrigger");
			} else { 
				// assert because bug
				Debug.LogError ("========== BUG kjh45jtk43");
			}	
		} else { 
			// assert because bug
			Debug.LogError ("========== BUG lllk3j54j4");
		}	
	}
	void InformParentThatFire1Clicked() {
		Debug.Log ("> parent clicked <");
		GameObject parent = GetCurrentParentGameObject ();
		if (parent) {
			Transform _tr = parent.transform;
			if (_tr) {
				Animator _animator = _tr.GetComponent <Animator> ();
				if (_animator) {
					_animator.SetTrigger ("SelectedTrigger");
				} else { 
					// assert because bug
					Debug.LogError ("========== BUG kkfkdd4s");
				}	
			} else { 
				// assert because bug
				Debug.LogError ("========== BUG ff543dj8");
			}	

		} else { 
			// assert because bug
			Debug.LogError ("========== BUG jfjjf7d");
		}	
	}
	void InformParentThatFire1Released() {
		Debug.Log ("> parent released <");
		GameObject parent = GetCurrentParentGameObject ();
		if (parent) {
			Transform _tr = parent.transform;
			if (_tr) {
				Animator _animator = _tr.GetComponent <Animator> ();
				if (_animator) {
					_animator.SetTrigger ("UnselectedTrigger");
				} else { 
					// assert because bug
					Debug.LogError ("========== BUG 76jj67");
				}	
			} else { 
				// assert because bug
				Debug.LogError ("========== BUG 6s6s6d5");
			}	
			
		} else { 
			// assert because bug
			Debug.LogError ("========== BUG lhlhk373474");
		}	
	}
	void SetParent1to0(float OneToZero) {
		GameObject parent = GetCurrentParentGameObject ();
		if (parent) {
			Transform _tr = parent.transform;
			if (_tr) {
				Animator _animator = _tr.GetComponent <Animator> ();
				if (_animator) {
					_animator.SetFloat ("Zero_to_one", OneToZero);
				} else { 
					// assert because bug
					Debug.LogError ("========== BUG ujytiuyt9tyui");
				}	
			} else { 
				// assert because bug
				Debug.LogError ("========== BUG gfdsdfg66gsdf");
			}	
			
		} else { 
			// assert because bug
			Debug.LogError ("========== BUG gfgiigriue3");
		}	
	}
	void SetLevel0to1(float ZeroToOne) {
		if (transformHitPrevious) {
			Animator _animator = transformHitPrevious.GetComponent <Animator> ();
			if (_animator) {
				_animator.SetFloat ("Zero_to_one", ZeroToOne);
			} else { 
				// assert because bug
				Debug.LogError ("========== BUG kjh45jtk83");
			}	
		} else { 
			// assert because bug
			Debug.LogError ("========== BUG lllk3j54a4");
		}	
	}
	GameObject GetCurrentParentGameObject() {
		GameObject GlobalInfoGameObject = GameObject.Find("GlobalInfo");
		SebGlobalInfoScript Script = GlobalInfoGameObject.GetComponent<SebGlobalInfoScript>();
		GameObject parent = Script.CurrentActiveParentLevelGameObject;
		return parent;
	}

	/*
	void processThereIsCurrentlyHovering(RaycastHit hit) {
		Transform transformHit = hit.transform;
		if (transformHit) {
			if (transformHit != transformHitPrevious_oldWay) {
				// means we are entering
				if (transformHitPrevious_oldWay) {
					// since it will replace previous one
					InformOveredThatHoveringJustEnded(transformHitPrevious_oldWay);
				}
				InformOveredThatHoveringJustStarted(transformHit);
				// update state
				transformHitPrevious_oldWay = transformHit;
			}
		} else { 
			// assert because bug
			Debug.LogError ("========== BUG yuytthfgh");
		}	
	}

	void processThereIsCurrently_No_Hovering() {
		if (transformHitPrevious_oldWay) {
			// we should inform the previous level that the hovering is finished
			InformOveredThatHoveringJustEnded(transformHitPrevious_oldWay);
		}
		transformHitPrevious_oldWay = null;
	}
	void InformOveredThatHoveringJustStarted(Transform tr) {
		Debug.Log ("Overed started");
		// get a handle on state machine for this level
		Animator _animator = tr.GetComponent <Animator> ();
		_animator.SetTrigger ("SelectedTrigger");
	}
	void InformOveredThatHoveringJustEnded(Transform tr) {
		Debug.Log ("Overed ended");
		Animator _animator = tr.GetComponent <Animator> ();
		_animator.SetTrigger ("UnselectedTrigger");

	}
*/


}
