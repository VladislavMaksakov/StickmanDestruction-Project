using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;

public enum Languages {
	Undefined,
	Unknown,

	Russian,
	Ukrainian,
	Belarusian,

	English,
	Italian,
	Spanish,
	French,
	German,
	Polish,
	Czech,

	Chinese,
	Japanese,
	Korean,

	Afrikaans,
	Arabic,
	Basque,
	Bulgarian,
	Catalan,
	Danish,
	Dutch,
	Estonian,
	Faroese,
	Finnish,
	Greek,
	Hebrew,
	Icelandic,
	Indonesian,
	Latvian,
	Lithuanian,
	Norwegian,
	Portuguese,
	Romanian,
	Slovak,
	Slovenian,
	Swedish,
	Thai,
	Turkish,
	Vietnamese,
	Hungarian
}

public static class Localisation{

	public static Languages CurrentLanguage = Languages.Undefined;
	static public Dictionary<string,string> Strings;
	static private XmlDocument LoadedLanguage;
	static bool LanguageLoaded = false;
	static TextAsset newbyLanguage;

	public static void DetectLanguage(){
			//#if UNITY_ANDROID
			// bugfix for Unity 4.3
			//AndroidJavaClass localeClass = new AndroidJavaClass("java/util/Locale");
			//AndroidJavaObject defaultLocale = localeClass.CallStatic<AndroidJavaObject>("getDefault");
			//AndroidJavaObject usLocale = localeClass.GetStatic<AndroidJavaObject>("US");
			//currentLanguage = defaultLocale.Call<string>("getDisplayLanguage", usLocale);
			//#else
			CurrentLanguage = (Languages)Enum.Parse (typeof(Languages), Application.systemLanguage.ToString());
			//#endif

			#if UNITY_EDITOR
			if(PlayerPrefs.HasKey("TestLanguage")){
				CurrentLanguage = (Languages)Enum.Parse (typeof(Languages),PlayerPrefs.GetString("TestLanguage"));
			}else{
				CurrentLanguage = (Languages)Enum.Parse (typeof(Languages),Application.systemLanguage.ToString());
			}
			#endif
			Debug.Log ("DetectedLanguage");

	}

	static public void LoadLanguage(){
		Localisation.DetectLanguage();
		LoadedLanguage = new XmlDocument ();
		Strings = new Dictionary<string, string>();
		Debug.Log ("LoadLanguage");
		newbyLanguage = (TextAsset) Resources.Load ("Localisation/" + CurrentLanguage.ToString() + ".xml", typeof(TextAsset));
		if(newbyLanguage == null){
			newbyLanguage = (TextAsset) Resources.Load ("Localisation/English.xml", typeof(TextAsset));
		}
		LoadedLanguage.LoadXml(newbyLanguage.text);
		foreach(XmlNode document in LoadedLanguage.ChildNodes){
			foreach(XmlNode newbyString in document.ChildNodes){
				Strings.Add(newbyString.Attributes["name"].Value,newbyString.InnerText);
			}
		}
		LanguageLoaded = true;
	}

	static public Languages GetCurrentLanguage(){
		if(LanguageLoaded == false){
			LoadLanguage();
		}
		return CurrentLanguage;
	}
	
	static public string GetString(string SearchString){
		if(LanguageLoaded == false){
			LoadLanguage();
		}
		if (Strings.ContainsKey (SearchString)) {
			return Strings [SearchString];
		} else {
			return "Unknown string";
		}
		
	}
}