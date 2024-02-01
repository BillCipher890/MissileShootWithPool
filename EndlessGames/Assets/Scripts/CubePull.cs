using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubePull : MonoBehaviour
{
    [SerializeField]
    private GameObject cubeForInstantiate;
    [SerializeField]
    private List<GameObject> cubes;

    private int cubeSpeed = GlobalConstants.StartCubeSpeed;
    private int cubeMaxDistance = GlobalConstants.StartCubeMaxDistance;

    private void Start()
    {
        CubeEventManager.OnDistanceChanged += SetMaxDistance;
        CubeEventManager.OnSpeedChanged += SetSpeed;
    }

    private void OnDestroy()
    {
        CubeEventManager.OnDistanceChanged -= SetMaxDistance;
        CubeEventManager.OnSpeedChanged -= SetSpeed;
    }

    private void SetSpeed(int speed)
    {
        cubeSpeed = speed;
    }

    private void SetMaxDistance(int distance)
    {
        cubeMaxDistance = distance;
    }

    public GameObject GetCube()
    {
        if (cubes.Any())
        {
            var cube = cubes.First();
            cubes.Remove(cube);
            cube.SetActive(true);
            return cube;
        }
        else
        {
            var cube = Instantiate(cubeForInstantiate, transform.position, Quaternion.identity, transform);
            cube.GetComponent<CubeMover>().Init(cubeSpeed, cubeMaxDistance);
            return cube;
        }
    }

    public void ReturnCube(GameObject cube)
    {
        cube.transform.SetParent(transform);
        cube.transform.position = transform.position;
        cube.transform.rotation = transform.rotation;
        cube.SetActive(false);
        cubes.Add(cube);
    }
}
