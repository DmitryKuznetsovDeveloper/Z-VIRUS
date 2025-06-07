using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
namespace Utils
{
    public static class DebugUtil
    {
        [Conditional("UNITY_EDITOR")]
        public static void DrawCircle(Vector3 position, float radius, Color color, float t = 0)
        {
#if UNITY_EDITOR
            DrawSector(position, radius, 0, 360, color, t);
#endif
        }

        [Conditional("UNITY_EDITOR")]
        public static void DrawSector(Vector3 position, float radius, int angMin, int angMax, Color color, float t = 0)
        {
#if UNITY_EDITOR
            Vector3? pointPrevious = null;
            Vector3? pointInitial = null;
            for (var i = angMin; i <= angMax; i += 10)
            {
                var pointNew = position;
                pointNew.x += radius * Mathf.Cos(i * Mathf.Deg2Rad);
                pointNew.z += radius * Mathf.Sin(i * Mathf.Deg2Rad);
                if (pointPrevious != null)
                {
                    Debug.DrawLine(pointPrevious.Value, pointNew, color, t);
                }
                else
                {
                    pointInitial = pointNew;
                }
                pointPrevious = pointNew;
            }

            if (angMax - angMin < 360 && pointInitial != null)
            {
                Debug.DrawLine(position, pointInitial.Value, color, t);
                Debug.DrawLine(position, pointPrevious.Value, color, t);
            }
#endif
        }

        [Conditional("UNITY_EDITOR")]
        public static void DrawLine(Vector3 from, float degrees, float length, Color color)
        {
#if UNITY_EDITOR
            var wayPos = new Vector3(
                from.x + length * Mathf.Cos(Mathf.Deg2Rad * degrees),
                2.2f,
                from.z + length * Mathf.Sin(Mathf.Deg2Rad * degrees)
            );
            Debug.DrawLine(from + Vector3.up * 2.2f, wayPos, color, 1f);
#endif
        }

        [Conditional("UNITY_EDITOR")]
        public static void DrawRect(Rect rect, Color color, float duration = 1f)
        {
#if UNITY_EDITOR
            Debug.DrawLine(new Vector3(rect.xMin, 2.2f, rect.yMin), new Vector3(rect.xMax, 2.2f, rect.yMin), color, duration);
            Debug.DrawLine(new Vector3(rect.xMin, 2.2f, rect.yMax), new Vector3(rect.xMax, 2.2f, rect.yMax), color, duration);
            Debug.DrawLine(new Vector3(rect.xMin, 2.2f, rect.yMin), new Vector3(rect.xMin, 2.2f, rect.yMax), color, duration);
            Debug.DrawLine(new Vector3(rect.xMax, 2.2f, rect.yMax), new Vector3(rect.xMax, 2.2f, rect.yMin), color, duration);
#endif
        }

        [Conditional("UNITY_EDITOR")]
        public static void DrawLine(Vector3 from, Vector3 to, Color color, float duration = 0)
        {
#if UNITY_EDITOR
            Debug.DrawLine(from, to, color, duration);
#endif
        }

        [Conditional("LOGS")]
        public static void Log(string message)
        {
#if LOGS
            Debug.Log(message);
#endif
        }
        
        [Conditional("LOGS")]
        public static void LogWithStack(string message)
        {
#if LOGS
            Debug.Log($"[LogWithStack]: {message}\n{System.Environment.StackTrace}");
#endif
        }

        [Conditional("LOGS")]
        public static void LogWarning(string message)
        {
#if LOGS
            Debug.LogWarning(message);
#endif
        }

        [Conditional("LOGS")]
        public static void LogError(string message)
        {
#if LOGS
            Debug.LogError(message);
#endif
        }
    }
}