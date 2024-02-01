using System;
using TMPro;
using UnityEngine;

public class CubeEventManager : MonoBehaviour
{
    public static Action<Vector3> OnParentPositionChanged;

    public static Action<int> OnDistanceChanged;

    public static Action<int> OnSpeedChanged;

    public static Action<GameObject> OnCubeIsFree;

    public static void SendParentPositionChanged(Vector3 parentPosition)
    {
        OnParentPositionChanged?.Invoke(parentPosition);
    }

    public static void SendDistanceChanged(int distance) 
    {
        OnDistanceChanged?.Invoke(distance); 
    }

    public static void SendDistanceChanged(TextMeshProUGUI text)
    {
        int.TryParse(text.text[..^1], out int distance);
        OnDistanceChanged?.Invoke(distance);
    }

    public static void SendSpeedChanged(int speed)
    {
        OnSpeedChanged?.Invoke(speed);
    }

    public static void SendSpeedChanged(TextMeshProUGUI text)
    {
        int.TryParse(text.text[..^1], out int speed);
        OnSpeedChanged?.Invoke(speed);
    }

    public static void SendCubeIsFree(GameObject cube)
    {
        OnCubeIsFree?.Invoke(cube);
    }
}
