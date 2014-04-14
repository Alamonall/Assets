using UnityEngine;
using System.Collections;

public class HeroThree : Characters, IJudje {
	public void Display(){
		Debug.Log(" My Name = "  + Name);
	}
}
