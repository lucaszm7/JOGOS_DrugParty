using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour {

	public Texture2D RectangleTexture;

    SpriteRenderer spriteRenderer;
    [SerializeField]
	CutsceneObject[] _listCustscene;
    CutsceneObject _currentScene;
	int index = -1;
	int max = 0;
	float delay = 0.03f;
    [SerializeField]
 	string TextinLine = "";
    [SerializeField]
    int part = 0;

	bool IsComplete{
		get{
            if(_currentScene.Type == CutscenesType.Legend) return true;
			if(index == -1 || index > max) return false;
			return TextinLine.Equals(_currentScene.Text);
		}
	}

    void Start(){
        part = GameController.part;
        spriteRenderer = GetComponent<SpriteRenderer>();
    	LoadCutScenes();
    }

    void LoadCutScenes(){
        TextAsset CutsceneInfo = Resources.Load<TextAsset>("XML/Part"+part);
        _listCustscene = XMLParser.ParserCutscene(CutsceneInfo.text);
        max = _listCustscene.Length;
        Step();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire1")){
            StopAllCoroutines();
            if(IsComplete){
            	if(index >= max-1){
                    if(part == 2){
                        Application.Quit();
                    }else{
                       LoadScene.Load("Level"+part);
                    }
                }else{
                    Step();
    		    }
            }else{
                TextinLine = _currentScene.Text;
                //GameObject.Destroy(this);
            }
        }
    }

    void Step(){
        index++;
        _currentScene = _listCustscene[index];
        TextinLine = "";
        StartCoroutine(Writing());
        spriteRenderer.sprite = Resources.Load<Sprite>("Cutscenes/Part"+part+"/"+_currentScene.Id);
    }

    IEnumerator Writing(){
    	string text = _currentScene.Text;
    	//int max = text.Length;
        int i =0;
        //for(int i=0;i<max;i++){
        while(!IsComplete){ 
            TextinLine += text[i];
            i++;
            Debug.Log(delay);
            yield return new WaitForSeconds(delay);
        }
    }

    void OnGUI(){
    	if(index == -1 || index > max) return;
		string title = _currentScene.Title;
        CutscenesType type = _currentScene.Type;
        int width = Camera.main.pixelWidth;
        int height = Camera.main.pixelHeight;   
        Rect gui;
        if(title != ""){
            float widthText = LengthMessage(title) + 15;
            if(type == CutscenesType.Dialog){
                gui = new Rect(15,height-110,widthText,25);
            }else{
                gui = new Rect(15,height-40,widthText,25);    
            }
            GUI.DrawTexture(gui,RectangleTexture);
            gui.x += 12;
            gui.y += 1;
            GUILayout.BeginArea(gui);
            GUILayout.Label(title);
            GUILayout.EndArea();            
        }

        if(type == CutscenesType.Dialog){
            gui = new Rect(30,height-80,width-60,70);
            GUI.DrawTexture(gui,RectangleTexture);
            gui.x += 10;
            gui.width -= 20;
            gui.y += 10;
            gui.height -= 20;
            GUILayout.BeginArea(gui);
            GUILayout.Label(TextinLine);
            GUILayout.EndArea();
        }
    }

     float LengthMessage(string message){
        GUIStyle style = GUI.skin.box;
        return style.CalcSize( new GUIContent(message) ).x;
     }
}
