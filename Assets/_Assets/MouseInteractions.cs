using UnityEngine;
using System.Collections;

public class MouseInteractions : MonoBehaviour {


	private GameObject ThisLevelGameObject = null;
	private GameObject SearchSpotlightInThisLevelGameObject = null;
	private GameObject FoundSpotlightInThisLevelGameObject = null;
	private MouseInteractions thisScript;
	public  bool Fire1JustPressed;
	public  bool Fire2JustPressed;
	private Vector3 MousePositionWhenClicked = new Vector3 (0, 0, 0);
	private bool IsMouseOverAGameObject;
	private RaycastHit hitIfThereIsOne;


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

		Fire1JustPressed = Input.GetButtonDown ("Fire1");
		Fire2JustPressed = Input.GetButtonDown ("Fire2");
		IsMouseOverAGameObject =  Physics.Raycast (rayToPixelUnderMouse, out hitIfThereIsOne);
		
		if (Fire1JustPressed) {
			MousePositionWhenClicked = Input.mousePosition;

			// deselect any previous level

			// select this level

			Debug.Log ("========== Fire1 =============");
		}
		
		if (Fire2JustPressed) {
			Debug.Log ("========== Fire2 =============");
		}

		BeamTheSearchSpotlightToThisDirection (rayToPixelUnderMouse);

	


		if (IsMouseOverAGameObject) {
			BeamTheFoundSpotlightToThisDirection (new Ray(new Vector3(0.5f, 1.5f, 1.5f), hitIfThereIsOne.point - new Vector3(0.5f, 1.5f, 1.5f)));
		} else {
			// put the beam behind us
			BeamTheFoundSpotlightToThisDirection (new Ray(new Vector3(0,0,0), new Vector3(0,0,-1)));
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
		thisScript = GetComponent<MouseInteractions>();
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

	void InformOveredThatFire1Clicked() {
	}
	void InformOveredThatFire1Released() {
	}
	void InformOveredThatHoveringJustStarted() {
	}
	void InformOveredThatHoveringJustEnded() {
	}
}
