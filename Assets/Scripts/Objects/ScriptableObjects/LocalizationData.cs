using Chebureck.Settings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chebureck.Objects.ScriptableObjects
{
	[CreateAssetMenu(fileName = "LocalizationData", menuName = "Chebureck/LocalizationData", order = 1)]
	public class LocalizationData : ScriptableObject
	{
		[SerializeField]
		public List<LocalizationLanguageData> languages;

		public LanguageEnumerators defaultLanguage;

		[Serializable]
		public class LocalizationLanguageData
		{
			public LanguageEnumerators language;
			[SerializeField]
			public List<LocalizationDataInfo> localizedTexts;
		}

		[Serializable]
		public class LocalizationDataInfo
		{
			public string key;
			[TextArea(1, 9999)]
			public string value;
		}
	}
}