using UnityEngine;

public class CarManager : MonoBehaviour
{
    GameObject player;
    Vector3 direction = new Vector3(0,0,PlayerScript.speed);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z - player.transform.position.z <= 15)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z - direction.z
            );
        }
    }
}
