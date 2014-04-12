using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public bool GameMode = true; 
	public bool ActivMenu = false;
	public bool LoadMenu = false; 
	public bool SaveMenu = false; 
	public bool NewConMenu = false; 
	public bool Pause = false; 
	public bool InventoryMenu = false; 
	public bool JournalMenu = false; 
	public bool SettingsMenu = false; 
	public GUIStyle ButtonMenuStyle;
	public GUIStyle BackgroundMenuStyle;
	public GUIStyle InventoryMenuStyle;
	public GUIStyle BackgroundInvJurMenuStyle;
	public GUIStyle JournalMenuStyle;
	void OnGUI () {
		if (ActivMenu || SettingsMenu)
						main_menu ();
		if (LoadMenu) 
						load_menu ();
		if (SaveMenu)
						save_menu ();
		if (NewConMenu)
						NewCon();
		if (InventoryMenu) {
						ShowInventory ();
						Debug.Log("ShowInve");
				}
		if (JournalMenu)
						ShowJournal ();
		if (GameMode)
						ShowMenu ();
	}
	void ShowMenu(){
		GUI.BeginGroup (new Rect (Screen.width / 5, 0, Screen.width / 3, Screen.height / 10),BackgroundInvJurMenuStyle);
		if (GUI.Button (new Rect (Screen.width / 5, 0,
		                          Screen.width / 6, Screen.height / 8),"", InventoryMenuStyle)) {
				if(InventoryMenu)
					InventoryMenu = false;
			   	else 
			   		InventoryMenu = true;
			   }
		if(GUI.Button (new Rect (Screen.width / 10, 0,
		                         Screen.width / 6, Screen.height / 8),"", JournalMenuStyle)) {
			if(JournalMenu)
				JournalMenu = false;
			else 
				JournalMenu = true;
		}
		GUI.EndGroup ();
	}
	void ShowInventory(){
		GameMode = false;
		Time.timeScale = 0;
		GUI.BeginGroup (new Rect (5, 5, Screen.width - 10, Screen.height - 10), BackgroundMenuStyle);
		if (GUI.Button (new Rect (Screen.width - 100, 5, Screen.width / 10,
		                          Screen.height / 10), "X" , ButtonMenuStyle)) {
				GameMode = true;
				InventoryMenu = false;
				Time.timeScale = 1;
		}
		GUI.EndGroup ();
		}

	void ShowJournal(){
		GameMode = false;
		Time.timeScale = 0;
		GUI.BeginGroup (new Rect (5, 5, Screen.width - 10, Screen.height - 10), BackgroundMenuStyle);
		if (GUI.Button (new Rect (Screen.width - 100, 5, Screen.width / 10,
		                          Screen.height / 10), "X" , ButtonMenuStyle)) {
			GameMode = true;
			JournalMenu = false;
			Time.timeScale = 1;
		}
		
		GUI.EndGroup ();
		}
//	void OnMouseEnter(){
//		if (this.gameObject.name == "Inventory") {
//			InventoryS = true;	
//			Debug.Log ("Mouse in");
//		}
//		if (this.gameObject.name == "Settings")
//			Settings = true;
//		if (this.gameObject.name == "Journal")
//			Journal = true;	
//	}
//	void OnMouseExit(){
//		if (this.gameObject.name == "Inventory") {
//				InventoryS = false;
//				Debug.Log ("Mouse out");
//				}
//		if (this.gameObject.name == "Settings")
//			Settings = false;
//		if (this.gameObject.name == "Journal")
//			Journal = false;	
//	}
	#region MainMenu
	void main_menu(){
		GUI.BeginGroup (new Rect (Screen.width / 100, Screen.height / 20, 256, 450),BackgroundMenuStyle);
		if (!GameMode) {
			if(GUI.Button (new Rect(Screen.width / 100 - 3, Screen.height / 20, 253, 100),"New Game", ButtonMenuStyle)){
				ActivMenu = false;
				NewConMenu = true;
			}
			if(GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20 + 102, 253, 100), "Load", ButtonMenuStyle)){
				ActivMenu = false;
				LoadMenu = true;
			}
			if(GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20 + 204, 253, 100), "Exit", ButtonMenuStyle)){
				Application.Quit();
			}
		}
		else {
			if(GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20, 253, 100), "Continue",ButtonMenuStyle)){
				ActivMenu = false;
				Time.timeScale = 1;
			}
			if(GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20 + 102, 253, 100), "Save",ButtonMenuStyle)){
				ActivMenu = false;
				SaveMenu = true;
			}
			if(GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20 + 204, 253, 100), "Load",ButtonMenuStyle)){
				ActivMenu = false;
				LoadMenu = true;
			}
			if(GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20 + 306, 253, 100), "Exit",ButtonMenuStyle)){
				GameMode = false;
			}
		}					
			GUI.EndGroup ();
		}
	#endregion
	#region LoadMeny
	void load_menu(){
		GUI.BeginGroup (new Rect (Screen.width / 100, Screen.height / 20, 256, 450),BackgroundMenuStyle);
		GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20, 253, 100), "Load Game 1",ButtonMenuStyle);
		GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20 + 102, 253, 100), "Load Game 2",ButtonMenuStyle);
		GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20 + 204, 253, 100), "Load Game 3",ButtonMenuStyle);
		if (GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20 + 306, 253, 100), "Back",ButtonMenuStyle)) {
					LoadMenu = false;
					ActivMenu = true;
			}
		GUI.EndGroup ();
		}
	#endregion 
//	#region WorkWith
//	void WorkWithInvetary (){
//		// функция меню инвентаря
//		if(ShowInventory){
//			Debug.Log ("Инвентарь выключен");
//			ShowInventory = false;
//			Destroy(InventoryPrefab);
//			Time.timeScale = 1;
//
//		}
//		else{
//			Debug.Log("Включен инвентарь");
//			ShowInventory = true;
//			Time.timeScale = 0;
//			InventoryPrefab = (GameObject)Instantiate(Resources.Load("Inventory"),new Vector3(-12,-20,0),Quaternion.identity);
//		}
//	}
//	void WorkWithJournal(){
//		if (ShowJournal) {
//			Debug.Log ("Включен журнал");
//			ShowJournal = false;
//			Time.timeScale = 1;
//		}
//		else {
//			ShowJournal = true;
//			Debug.Log ("Выключен журнал");
//			Time.timeScale = 0;
//		}
//	}
//	void WorkWithSettings(){
//		if (ShowSettings) {
//		}
//		else {
//		}
//	}
//	#endregion

	#region NewCon
	void NewCon(){
		if (GameMode) {
			ActivMenu = false;
			Debug.Log ("Вышли из мегю");
		}
		else {
			GUI.BeginGroup (new Rect (Screen.width / 100, Screen.height / 20, 256, 450), BackgroundMenuStyle);
			if(GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20, 253, 100), "New Game 1",ButtonMenuStyle)){
				Application.LoadLevel("GlobalWorld");
			}
			if(GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20 + 102, 253, 100), "New Game 2",ButtonMenuStyle)){
				Application.LoadLevel("GlobalWorld");
			}
			if(GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20 + 204, 253, 100), "New Game 3",ButtonMenuStyle)){
				Application.LoadLevel("GlobalWorld");
			}
			if (GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20 + 306, 253, 100), "Back",ButtonMenuStyle)) {
				NewConMenu = false;
				ActivMenu = true;
			}
			GUI.EndGroup ();
		}
	}
	#endregion
	#region SaveMenu
	void save_menu(){
		GUI.BeginGroup (new Rect (Screen.width / 100, Screen.height / 20, 256, 450), BackgroundMenuStyle);
		GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20 , 253, 100), "Save Slote 1",ButtonMenuStyle);
		GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20 + 102, 253, 100), "Save Slote 2",ButtonMenuStyle);
		GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20 + 204, 253, 100), "Save Slote 3",ButtonMenuStyle);
		if (GUI.Button (new Rect (Screen.width / 100 - 3, Screen.height / 20 + 306, 253, 100), "Back",ButtonMenuStyle)) {
			SaveMenu = false;
			ActivMenu = true;
		}
		GUI.EndGroup ();
	}
	#endregion
	#region Update
	void Update(){
//		if (Input.GetMouseButtonDown (0) && GameMode && !ActivMenu && !LoadMenu && !SaveMenu && !NewConMenu) {
//			Debug.Log ("Im in Update" + " " + InventoryS);
//			if(InventoryS){
//				Debug.Log ("Im in Update if inventory");
//				WorkWithInvetary();
//			}
//			if(Journal){
//				Debug.Log ("Im in Update if journal");
//				WorkWithJournal();
//			}
//			if(Settings){
//				WorkWithSettings();
//			}
//		}
		if(Input.GetKeyDown (KeyCode.I) && GameMode && !ActivMenu && !LoadMenu && !SaveMenu && !NewConMenu){
			//WorkWithInvetary();
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(!ActivMenu && !SaveMenu && !NewConMenu && !LoadMenu && GameMode){ 
				//MainMenuTexture = (GameObject)Instantiate(Resources.Load("MainMenuPrefab"),new Vector3(-12,-20,0),Quaternion.identity);
				ActivMenu = true;
				Time.timeScale = 0;
			}
			else if(GameMode){
					Time.timeScale = 1;
					//Destroy(MainMenuTexture);
					ActivMenu = false;	
					LoadMenu = false;
					SaveMenu = false;
					NewConMenu = false;	
			}
		}
	}
	#endregion
}