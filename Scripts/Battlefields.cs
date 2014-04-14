using UnityEngine;
using System.Collections;

public class Batlefields : MonoBehaviour {
	Object WarriorTexture;
	Object ArcherTexture;
	Object MageTexture;
	Object ActCell;
	Object PosCell;
	Object En1;
	Object En2;
	Object En3;
	int[,] Field;
	int n, m;
	void Start(){
		n = 6;
		m = 15;
		WarriorTexture = Resources.Load("Warrior");
		ArcherTexture = Resources.Load("Archer");
		MageTexture = Resources.Load("MAge");
		ActCell = Resources.Load("Cell_Act");
		PosCell = Resources.Load("Cell_Pos");
		En1 = Resources.Load("Blue");
		En2 = Resources.Load("Green");
		En3 = Resources.Load("Red");
		Field = new int[n,m];
		int c = 15;
		for(int i = 0; i <= n; i++){
			for(int j = 0; j <= m; j++){
				Debug.Log("i = " + i + ", j = " + j);
				Instantiate(ActCell, new Vector3(i*15 + c, (j + 1) * 15),Quaternion.identity);
				Field[i,j] = 0;
			}
			c=c+14;
		}
		Charachters charS = new Charachters();
        	charS.add(0, new HeroOne());
        	charS.execute(0);
	}
	void move(int x1, int x2, int step){
		if(step > 0){
			if(x1 > 1 && x2 >= 0){
				move(x1 - 1,x2,step - 1);
			}
			if(x2 > 1 && x1 >= 0){
				move(x1, x2 - 1, step - 1);
			}
			if(x1 < n && x2 < m){
				move(x1 + 1, x2, step - 1);
			}
			if(x2 < m && x1 < n){
				move(x1,x2 + 1, step - 1);
			}
			if(Field[x1,x2] == 0)
				Field[x1,x2] = 1;
		}
		else{
			if(Field[x1,x2] == 0){
				Field[x1,x2] = 1;
			}
		}
		
	}

}
