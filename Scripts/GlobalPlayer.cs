using UnityEngine;
using System.Collections;

public class GlobalPlayer : MonoBehaviour {
	private Animator anim;
	public int Speed = 5;
	public int Ran = 0;
	private Camera Camera;
	private float CurX = -8;
	private float CurY = -13;
	private float CurZ = 1;
	private Transform Player;
	private string Profile = "Prof1";
	public static bool Battle = false;
	public string TypeOfLocation;
	public bool DangerZone = false;
	public float Times = 1;
	void OnTriggerEnter2D(Collider2D other) {
		if (!(other.gameObject.name == "GlobalPlayer")) {
			TypeOfLocation = other.gameObject.name;
			if(other.gameObject.tag == "DangerZone")
				DangerZone = true;
			else
				DangerZone = false;
		}
	}
	void OnTriggersExit2D(Collider2D other){
	}
	void Start(){
			anim = GetComponent<Animator>();
			Player = this.gameObject.transform;
	}
	void MeetTheEnemy(){
			DangerZone = false;
			Application.LoadLevel ("Battlefield");
			CurX = Player.position.x;
			CurY = Player.position.y;
			PlayerPrefs.SetFloat ("PosX" + Profile, CurX); 
			PlayerPrefs.SetFloat ("PosY" + Profile, CurY); 
			PlayerPrefs.SetFloat ("PosZ" + Profile, CurZ); 
	}
	void Update(){
		if (DangerZone) {
						do {
							if (Times > 0)
									Times -= Time.deltaTime;
							if (Times < 0)
									Times = 0; 
							if (Times == 0) {
									Ran = (Random.Range (0, 1000));
									Times = 1;
									Debug.Log (Ran);
									if (Ran > 650 && !Battle) { 
											Debug.Log ("BATTLE!" + Ran);
											MeetTheEnemy ();
									}
							}
						}
					    while(Times == 1 && DangerZone);
				}
	
		if(Input.GetKeyDown (KeyCode.B) && !Battle) {
			Application.LoadLevel ("Battlefield");
			CurX = Player.position.x;
			CurY = Player.position.y;
			PlayerPrefs.SetFloat ("PosX" + Profile, CurX); 
			PlayerPrefs.SetFloat ("PosY" + Profile, CurY); 
			PlayerPrefs.SetFloat ("PosZ" + Profile, CurZ); 
		}
		if (Battle) {
			Vector3 PlayerPosition = new Vector3 (PlayerPrefs.GetFloat ("PosX" + Profile),
			                                      PlayerPrefs.GetFloat ("PosY" + Profile), 
			                                      PlayerPrefs.GetFloat ("PosZ" + Profile));
			Player.position = PlayerPosition;
			Battle = false;			
		}
		if (Input.GetKey (KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			anim.SetBool ("MoveUp", true);
			Player.Translate (Vector3.up * Speed * Time.deltaTime);
		}
		else anim.SetBool ("MoveUp", false);
		if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			anim.SetBool ("MoveRight", true);
			Player.Translate (Vector3.right * Speed * Time.deltaTime);
		}
		else anim.SetBool ("MoveRight", false);
		if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			anim.SetBool ("MoveLeft", true);
			Player.Translate(Vector3.left * Speed * Time.deltaTime);
		}
		else anim.SetBool ("MoveLeft", false);
		if (Input.GetKey (KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			anim.SetBool ("MoveDown", true);
			Player.Translate (Vector3.down * Speed * Time.deltaTime);
		}
		else anim.SetBool ("MoveDown", false);
		if (!Input.anyKey) anim.SetBool ("NotMove", true);
		else anim.SetBool ("NotMove", false);
		}
}
