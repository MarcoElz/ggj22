using System;
using UnityEngine;

namespace CommonUtils {
	public static class MathUtils {
		public static float NormalizeAngle(float angle) {
			// reduce the angle  
			angle = angle % 360;

			// force it to be the positive remainder, so that 0 <= angle < 360  
			angle = (angle + 360) % 360;

			// force into the minimum absolute value residue class, so that -180 < angle <= 180  
			if (angle > 180)
				angle -= 360;

			return angle;
		}

		public static void TwoDimenionalLookAt(Transform transf, Vector3 targetPos) {
			var toTarget = targetPos - transf.position;
			transf.rotation = Quaternion.LookRotation(Vector3.forward, -toTarget);
			transf.Rotate(Vector3.forward, -90f);
		}

		public static Vector3 ScreenPointToLocalPointInRectangle(Canvas canvas, Vector2 position) {
			Vector2 pos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
																	position,
																	canvas.worldCamera,
																	out pos);
			return canvas.transform.TransformPoint(pos);
		}

		public static Vector3 GetDirectionFromSpread(Quaternion rotation, float spreadAngle) {
			float angleOff = spreadAngle * Mathf.Deg2Rad;
			var multiplier = new Vector3(UnityEngine.Random.Range(-Mathf.Sin(angleOff), Mathf.Sin(angleOff)),
										 UnityEngine.Random.Range(-Mathf.Sin(angleOff), Mathf.Sin(angleOff)),
										 1.0f);
			return rotation * multiplier;
		}

		public static double DegreeToRadian(this double angle) => Math.PI * angle / 180.0;
		public static double DegreeToRadian(this float  angle) => Math.PI * angle / 180.0;

		public static double RadianToDegree(this double angle) => angle * (180.0 / Math.PI);
		public static double RadianToDegree(this float  angle) => angle * (180.0 / Math.PI);

		public static int WrapValue(int value, int min, int max)
			=> (((value - min) % (max - min)) + (max - min)) % (max - min) + min;

		public static int RandomNegPos() => UnityEngine.Random.Range(0, 2) * 2 - 1;

		public static void SetGlobalScale(Transform transform, Vector3 globalScale) {
			transform.localScale = Vector3.one;
			transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x,
											   globalScale.y / transform.lossyScale.y,
											   globalScale.z / transform.lossyScale.z);
		}

		public static byte ConvertBoolArrayToByte(bool[] source) {
			byte result = 0;

			// This assumes the array never contains more than 8 elements!
			int index  = 8 - source.Length;
			int length = source.Length;

			// Loop through the array
			for (int i = 0; i < length; i++) {
				// if the element is 'true' set the bit at that position
				if (source[i]) {
					result |= (byte) (1 << (7 - index));
				}

				index++;
			}

			return result;
		}

		public static bool[] ConvertByteToBoolArray(byte b) {
			// prepare the return result
			bool[] result = new bool[8];

			// check each bit in the byte. if 1 set to true, if 0 set to false
			for (int i = 0; i < 8; i++)
				result[i] = (b & (1 << i)) == 0 ? false : true;

			// reverse the array
			//System.Array.Reverse(result);

			for (int i = 0; i < result.Length / 2; i++) {
				bool tmp = result[i];
				result[i] = result[result.Length - i - 1];
				result[result.Length - i             - 1] = tmp;
			}

			return result;
		}

		public static bool LineIntersectsRect(Vector2 p1, Vector2 p2, Rect r) {
			return LineIntersectsLine(p1, p2, new Vector2(r.x, r.y), new Vector2(r.x + r.width, r.y)) ||
				   LineIntersectsLine(p1,
									  p2,
									  new Vector2(r.x + r.width, r.y),
									  new Vector2(r.x + r.width, r.y + r.height)) ||
				   LineIntersectsLine(p1,
									  p2,
									  new Vector2(r.x + r.width, r.y + r.height),
									  new Vector2(r.x,           r.y + r.height))                      ||
				   LineIntersectsLine(p1, p2, new Vector2(r.x, r.y + r.height), new Vector2(r.x, r.y)) ||
				   (r.Contains(p1) && r.Contains(p2));
		}

		private static bool LineIntersectsLine(Vector2 l1p1, Vector2 l1p2, Vector2 l2p1, Vector2 l2p2) {
			float q = (l1p1.y - l2p1.y) * (l2p2.x - l2p1.x) - (l1p1.x - l2p1.x) * (l2p2.y - l2p1.y);
			float d = (l1p2.x - l1p1.x) * (l2p2.y - l2p1.y) - (l1p2.y - l1p1.y) * (l2p2.x - l2p1.x);

			if (d == 0) {
				return false;
			}

			float r = q / d;

			q = (l1p1.y - l2p1.y) * (l1p2.x - l1p1.x) - (l1p1.x - l2p1.x) * (l1p2.y - l1p1.y);
			float s = q / d;

			if (r < 0 || r > 1 || s < 0 || s > 1) {
				return false;
			}

			return true;
		}

		public static Vector3 LineIntersection3D(Vector3 p1, Vector3 v1, Vector3 p2, Vector3 v2) {
			//assumes the lines actually intersect

			Vector3 cross    = Vector3.Cross(v1, v2);
			float   magCross = cross.magnitude;

			if (magCross != 0) {

				Vector3 pointDiff        = p2 - p1;
				Vector3 pointDiffCrossV2 = Vector3.Cross(pointDiff, v2);

				float a = pointDiffCrossV2.magnitude / magCross;

				return p1 + v1 * a;
			} else {
				Debug.LogError("error! lines are coplanar!");
				return Vector3.zero;
			}

		}
	}
}