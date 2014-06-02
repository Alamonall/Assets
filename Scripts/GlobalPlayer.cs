using UnityEngine;
using System.Collections;

public class GlobalPlayer : MonoBehaviour {
	Transform Player;
	public int Speed = 5; // скорость хождения
	public int Ran = 0;
	float CurX = -8; // Стартовые координаты ГГ
	float CurY = -13; // Стартовые координаты ГГ
	float CurZ = 1; // Стартовые координаты ГГ
	string Profile = "Prof1"; //Какой профиль загружен
	public static bool Battle = false; 
	public string TypeOfLocation; // Отвечат за то, на каком типе территории находится ГГ
	public bool DangerZone = false; // Опасная ли зона, на которой счас ГГ
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

	void Start(){
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
			Player.Translate (Vector3.up * Speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			Player.Translate (Vector3.right * Speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			Player.Translate(Vector3.left * Speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			Player.Translate (Vector3.down * Speed * Time.deltaTime);
		}
	}
}
