using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public bool GameMode; // Включен ли игровой режим
	public bool ActivMenu; //Активно ли главное меню
	public bool LoadMenu; // Активно ли меню загрузки
	public bool SaveMenu; // Активно ли меню сохранения
	public bool NewConMenu;  // Активно ли меню Новой игры/Продолжить
	public bool InventoryMenu;  // Активно ли меню инвентаря
	public bool JournalMenu;  // Активно ли меню журнала
	public bool SettingsMenu;  // Активно ли меню настроек
	public GUIStyle ButtonMenuStyle; 
	public GUIStyle BackgroundMenuStyle;
	public GUIStyle InventoryMenuStyle;
	public GUIStyle BackgroundInvJurMenuStyle;
	public GUIStyle JournalMenuStyle;

	void Start(){
		if(Application.loadedLevelName == "GlobalWorld" || Application.loadedLevelName == "Battlefield"){
			GameMode = true;
			ActivMenu = false;
			LoadMenu = false;
			SaveMenu = false;
			NewConMenu = false;  
			InventoryMenu = false;  
			JournalMenu = false;  
			SettingsMenu = false;
		}
		else{
			GameMode = false;
			ActivMenu = true;
			LoadMenu = false;
			SaveMenu = false;
			NewConMenu = false;  
			InventoryMenu = false;  
			JournalMenu = false;  
			SettingsMenu = false;
		}

	}

	void OnGUI () {
		if (ActivMenu || SettingsMenu){
			main_menu ();}
		if (LoadMenu) {
			load_menu ();}
		if (SaveMenu){
			save_menu ();}
		if (NewConMenu){
			NewCon();}
		if (InventoryMenu){
			ShowInventory ();}
		if (JournalMenu){
			ShowJournal ();}
		if (GameMode){
			ShowMenu ();}
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
				Application.LoadLevel("FirstMenu");
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

	#region NewCon
	void NewCon(){
		if (GameMode) {
			ActivMenu = false;
			Debug.Log ("Вышли из меню");
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
		if(Input.GetKeyDown (KeyCode.I) && GameMode && !ActivMenu && !LoadMenu && !SaveMenu && !NewConMenu){
			//WorkWithInvetary();
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(!ActivMenu && !SaveMenu && !NewConMenu && !LoadMenu && GameMode){ 
				ActivMenu = true;
				Time.timeScale = 0;
			}
			else if(GameMode){
					Time.timeScale = 1;
					ActivMenu = false;	
					LoadMenu = false;
					SaveMenu = false;
					NewConMenu = false;	
			}
		}
	}
	#endregion
}