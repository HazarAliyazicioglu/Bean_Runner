using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour
{
    public GameObject[] tiles;

    private int tPos = 34;
    private bool creatingTile = false;
    private int tilNum;

    void Update()
    {
        if (creatingTile == false)
        {
            creatingTile = true;
            StartCoroutine(TileGenerator());
        }
    }

    IEnumerator TileGenerator()
    {
        tilNum = Random.Range(0,7);
        Instantiate(tiles[tilNum], new Vector3(-2, 0, tPos), Quaternion.identity);
        tPos += 34;
        yield return new WaitForSeconds(3);
        creatingTile = false;
    }
}
