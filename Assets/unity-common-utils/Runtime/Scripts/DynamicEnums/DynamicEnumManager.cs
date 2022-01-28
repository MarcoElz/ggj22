﻿using System.Collections.Generic;
using UnityEngine;

namespace CommonUtils.DynamicEnums {
	public static class DynamicEnumManager {
		private const string resourceName = "DynamicEnums";

		private static DynamicEnumDefinitions _definitions;

		private static DynamicEnumDefinitions definitions {
			get {
				if (!_definitions) Reload();
				return _definitions;
			}
		}

		public static IReadOnlyCollection<string> GetValues(string enumName) => definitions[enumName]?.Values;
		public static GUIContent[] GetValuesAsGuiContent(string enumName) => definitions[enumName]?.ValuesAsGuiContent;
		public static int ValueToInt(string enumName, string value) => definitions[enumName]?.ToInt(value) ?? -1;
		public static string IntToValue(string enumName, int index) => definitions[enumName]?.ToValue(index);

		public static bool Reload() {
			_definitions = Resources.Load<DynamicEnumDefinitions>(resourceName);
			_definitions.Reload();
			return _definitions;
		}
	}
}