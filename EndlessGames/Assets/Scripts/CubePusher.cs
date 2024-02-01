using System.Collections;
using TMPro;
using UnityEngine;

public class CubePusher : MonoBehaviour
{
    [SerializeField]
    private CubePull cubePull;

    private float appearSpeed = GlobalConstants.StartCubeAppearSpeed;

    private void Start()
    {
        CubeEventManager.OnCubeIsFree += SendCubeBackIntoPull;
        StartCoroutine(CubePush());
    }

    private void OnDestroy()
    {
        CubeEventManager.OnCubeIsFree -= SendCubeBackIntoPull;
    }

    IEnumerator CubePush()
    {
        while (true)
        {
            var cube = GetCube();
            CubeEventManager.SendParentPositionChanged(transform.position);
            StartCoroutine(cube.GetComponent<CubeMover>().MoveCube());

            yield return new WaitForSeconds(appearSpeed);
        }
    }

    private GameObject GetCube()
    {
        var cube = cubePull.GetCube();
        cube.transform.position = transform.position;
        cube.transform.SetParent(transform);
        return cube;
    }

    private void SendCubeBackIntoPull(GameObject cube)
    {
        cubePull.ReturnCube(cube);
    }

    public void SetAppearSpeed(TextMeshProUGUI text)
    {
        var strForParse = text.text[..^1].Replace(".", ",");
        double.TryParse(strForParse, out double dAppearSpeed);
        appearSpeed = (float)dAppearSpeed;
    }
}
