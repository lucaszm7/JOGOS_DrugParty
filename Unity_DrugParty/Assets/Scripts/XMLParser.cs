﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class XMLParser {

	public static CutsceneObject[] ParserCutscene(string content){
		XmlReader reader = XmlReader.Create(new StringReader(content));
		List<CutsceneObject> cutscenes = new List<CutsceneObject>();
		CutsceneObject current = null;

		while(reader.Read()){
			if(reader.IsStartElement("cutscene")){
				if(current != null) cutscenes.Add(current);
				current = new CutsceneObject();
			}
			if(current != null){
				if(reader.IsStartElement("id")) current.Id = reader.ReadElementContentAsInt();
				if(reader.IsStartElement("text")) current.Text = reader.ReadElementContentAsString();
				if(reader.IsStartElement("type")) {
					string type = reader.ReadElementContentAsString();
					switch(type){
						case "Dialog":
							current.Type = CutscenesType.Dialog;
						break;
						case "Legend":
							current.Type = CutscenesType.Legend;
						break;
					}
				}
				if(reader.IsStartElement("title")) current.Title = reader.ReadElementContentAsString();
			}
		}

		if(current != null) cutscenes.Add(current);		
		return cutscenes.ToArray();
	}	
}
