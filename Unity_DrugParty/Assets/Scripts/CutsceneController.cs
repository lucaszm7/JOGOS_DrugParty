using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour {

	public Texture2D RectangleTexture;

	CutsceneObject[] _listCustscene;
	int index = -1;
	int max = 0;
	float delay = 0.05f;
	string TextinLine = "";
	

	bool IsComplete{
		get{
			if(index == -1 || index > max) return false;
			return TextinLine.Equals(_listCustscene[index].Text);
		}
	}

    void Start(){
    	TextAsset CutsceneInfo = Resources.Load<TextAsset>("XML/Part1");
        _listCustscene = XMLParser.ParserCutscene(CutsceneInfo.text);
        max = _listCustscene.Length;
        /*foreach(CutsceneObject cutscene in _listCustscene){
			Debug.Log(cutscene.Text);
        }*/
    }

    void Update(){
    	//var sprite = Resources.Load<Sprite>("Sprites/sprite01");
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)){
        	if(index >= max-1){
        		Debug.Log("Acabou");
        	}else{
	        	index++;
		    	StartCoroutine(Writing());
		    }
        }
    }

    IEnumerator Writing(){
    	int i = 0;
    	string text = _listCustscene[index].Text;
    	int max = text.Length;
        for(i=0;i<max;i++){
            TextinLine += text[i];
            yield return new WaitForSeconds(delay);
        }
    }

    void OnGUI(){
    	if(index == -1 || index > max) return;
    		GUI.DrawTexture(new Rect(0,Camera.main.pixelHeight-70, Camera.main.pixelWidth, Camera.main.pixelHeight),RectangleTexture);
		
        GUILayout.BeginArea(new Rect(30, Camera.main.pixelHeight-50, Camera.main.pixelWidth-50, 50));
        GUILayout.Label(TextinLine);
        GUILayout.EndArea();
    }
}
