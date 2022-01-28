using UnityEngine;

namespace Ignita.Utils.Common
{
	public class CompareUtil : MonoBehaviour
	{
		//Most base methods from: https://gist.github.com/openroomxyz/e13dbb13ea33d769ed554ad1933e6ea3

		public static bool IsEqual(float a, float b) {
			if (a >= b - Mathf.Epsilon && a <= b + Mathf.Epsilon) {
				return true;
			} else {
				return false;
			}
		}

		public static bool IsEqual(Vector2 v1, Vector2 v2) {
			if (!IsEqual(v1.x, v2.x)) return false;
			if (!IsEqual(v1.y, v2.y)) return false;
			return true;
		}

		public static bool IsEqual(Vector3 v1, Vector3 v2) {
			if (!IsEqual(v1.x, v2.x)) return false;
			if (!IsEqual(v1.y, v2.y)) return false;
			if (!IsEqual(v1.z, v2.z)) return false;
			return true;
		}

		public static bool IsEqual(Vector4 v1, Vector4 v2) {
			if (!IsEqual(v1.x, v2.x)) return false;
			if (!IsEqual(v1.y, v2.y)) return false;
			if (!IsEqual(v1.z, v2.z)) return false;
			if (!IsEqual(v1.w, v2.w)) return false;
			return true;
		}

		public static bool IsEqual(Quaternion q1, Quaternion q2) {
			if (!IsEqual(q1.w, q2.w)) return false; 
			if (!IsEqual(q1.x, q2.x)) return false;
			if (!IsEqual(q1.y, q2.y)) return false;
			if (!IsEqual(q1.z, q2.z)) return false;
			return true;
		}

		public static bool AlmostEqual(float a, float b, float tolerance) {
			if (a >= b - tolerance && a <= b + tolerance) {
				return true;
			} else {
				return false;
			}
		}

		public static bool AlmostEqual(Vector3 v1, Vector3 v2, float tolerance) {
			if (!AlmostEqual(v1.x, v2.x, tolerance)) return false;
			if (!AlmostEqual(v1.y, v2.y, tolerance)) return false;
			if (!AlmostEqual(v1.z, v2.z, tolerance)) return false;
			return true;
		}

		public static bool AlmostEqual(Quaternion q1, Quaternion q2, float tolerance) {
			if (!AlmostEqual(q1.w, q2.w, tolerance)) return false;
			if (!AlmostEqual(q1.x, q2.x, tolerance)) return false;
			if (!AlmostEqual(q1.y, q2.y, tolerance)) return false;
			if (!AlmostEqual(q1.z, q2.z, tolerance)) return false;
			return true;
		}
	}
}
