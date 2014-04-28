using UnityEngine;
using System.Collections;

public class DestroyObj : MonoBehaviour {

	public bool ImInside;

	void Start(){
		ImInside = false;
	}

	void OnMouseEnter (){
		Debug.Log ("ADAD");
		renderer.material.color = Color.black;
		ImInside = true;
	}

	void OnMouseExit(){
		Debug.Log ("adad");
		renderer.material.color = Color.white;
		ImInside = false;
	}
	public void Print(){
		Debug.Log ("its Working!");
	}

	void Update () {
		if(Input.GetMouseButton(0) && ImInside){
			Debug.Log ("Destroyed");
			Battlefields.frag = true;
			Destroy(this.gameObject);
			GameObject.Find("Main Camera").GetComponent<Battlefields>().MakeTransition(this.gameObject.transform.position.x, this.gameObject.transform.position.y);	
		}

		if(Input.GetKeyDown(KeyCode.S) || Battlefields.frag){
			Debug.Log("Buum!");
			Battlefields.frag = false;
			Destroy(this.gameObject);
		}
	}
}
