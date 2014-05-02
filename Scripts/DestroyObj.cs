using UnityEngine;
using System.Collections;

public class DestroyObj : MonoBehaviour {

	public bool ImInside;

	void Start(){
		ImInside = false;
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
			GameObject.Find("Main Camera").GetComponent<Battlefields>().MakeTransition(this.gameObject.transform.position.x, this.gameObject.transform.position.y);	
		}
	}
}
