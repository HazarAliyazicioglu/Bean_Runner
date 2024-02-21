using UnityEngine;

public class DestroyerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Garbage Collector")
        {
            Destroy(gameObject);
        }
    }
}
