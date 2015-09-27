using UnityEngine;
using System.Collections;

public class SebLevelActivator : MonoBehaviour {


	private Animator _animator;
	private HandModel _HandModel;
	public Vector3 InitialLocalPosition;
	public Vector3 InitialLocalScale;

	void Awake () {
		// Store initial position so the script can revert to it later
		_animator = GetComponent <Animator> ();
		InitialLocalPosition = transform.localPosition;
		InitialLocalScale = transform.localScale;
		Debug.Log ("InitialLocal value: Position =" + InitialLocalPosition + ", Scale =" + InitialLocalScale);
	}




	private bool IsHand(Collider other)
	{
		if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel> ()) {
			_HandModel = other.transform.parent.parent.GetComponent<HandModel> ();
			return true;
		} else {
			_HandModel = null;
			return false;
		}
	}

	//public int health;
	private bool IsHandFar()
	{
		if (_HandModel) { // assert
			Debug.Break();
			// does not compile - health.MustNotBeEqual(0);
			// does not compile - Assert.IsTrue(1);
			Debug.LogError("bug detected. Ref ABC987");
			return false; // random
		} else { // no assert
			float _posZ = _HandModel.GetPalmPosition().z;
			Debug.Log ("Hand collided position = " + _posZ);
			if (_posZ >= 0) {
				return true;
			} else {
				return false;
			}
		}
	}
	

	void OnTriggerEnter (Collider other) {
		if (IsHand(other)) {
			//bool _far = IsHandFar();
			//bool _collided = IsHandCollided();
			//bool _selected = IsHandSelectionGesture();
			//bool _activated = IsHandActivationGesture();


			if (IsHandFar()) {
				Debug.Log("Hand collided far");
				_animator.SetBool("Seb Level Selected From Far Position", true);
			} else {
				Debug.Log("Hand collided near");
				_animator.SetBool("Seb Level Selected From Near Position", true);
			}
		}
	
	}

	void OnTriggerExit (Collider other) {
		if (IsHand(other)) {
			if (IsHandFar()) {
				Debug.Log("Hand exited far");
				_animator.SetBool("Seb Level Selected From Far Position", false);
			} else {
				Debug.Log("Hand exited near");
				_animator.SetBool("Seb Level Selected From Near Position", false);
			}
		}
		
	}

	// Use this for initialization
	//void Start () {
	
	//}
	
	// Update is called once per frame
	//void Update () {
	
	//}
}
