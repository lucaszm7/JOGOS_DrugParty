using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CutscenesType{
	Dialog,
	Legend
}
[System.Serializable]
public class CutsceneObject {

	public int Id;
	public string Title;
	public string Text;
	public CutscenesType Type;
}
