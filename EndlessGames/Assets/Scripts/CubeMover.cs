using System.Collections;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    private Vector3 pusherPosition;
    private int speed = GlobalConstants.StartCubeSpeed;
    private int maxDistance = GlobalConstants.StartCubeMaxDistance;

    private void Start()
    {
        CubeEventManager.OnDistanceChanged += SetMaxDistance;
        CubeEventManager.OnParentPositionChanged += SetPusherPosition;
        CubeEventManager.OnSpeedChanged += SetSpeed;
    }

    private void OnDestroy()
    {
        CubeEventManager.OnDistanceChanged -= SetMaxDistance;
        CubeEventManager.OnParentPositionChanged -= SetPusherPosition;
        CubeEventManager.OnSpeedChanged -= SetSpeed;
    }

    private void SetPusherPosition(Vector3 position)
    {
        pusherPosition = position;
    }

    private void SetSpeed(int speed)
    {
        this.speed = speed;
    }

    private void SetMaxDistance(int distance)
    {
        maxDistance = distance;
    }

    public void Init(int speed, int distance)
    {
        this.speed = speed;
        maxDistance = distance;
    }

    public IEnumerator MoveCube()
    {
        while (true)
        {
            transform.Translate(Vector3.forward * speed / 100f);

            var vectorFromCubeToPusher = pusherPosition - transform.position;
            if (vectorFromCubeToPusher.magnitude < maxDistance)
                yield return new WaitForFixedUpdate();
            else
                break;
        }
        CubeEventManager.SendCubeIsFree(gameObject);
    }
}
