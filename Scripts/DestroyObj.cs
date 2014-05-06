using UnityEngine;
using System.Collections;

public class DestroyObj : MonoBehaviour {

	public bool ImInside;
	public Transform ThisObj;

	void Start(){
		ImInside = false;
		ThisObj = this.gameObject.transform;
	}

	void OnMouseEnter (){
		renderer.material.color = Color.black;
		ImInside = true;
	}

	void OnMouseExit(){
		renderer.material.color = Color.white;
		ImInside = false;
	}

	void Update () {
		if(Input.GetMouseButton(0) && ImInside){
			GameObject.Find("Main Camera").GetComponent<Battlefields>().MakeTransition(ThisObj.position.x, ThisObj.position.y);	
		}
	}
}
