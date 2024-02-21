using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Transform playerPosition;
    public new Transform camera;
    public Transform sky;
    public Transform clouds;
    public Transform garbageCollector;
    public GameObject RestartMenu;
    public GameObject MainMenu;
    public GameObject InGameMenu;
    public MeshRenderer mesh;    
    public static float speed = 1f;
    public static int hearts = 3;
    public static int collectedCoins = 0;
    public static bool restarted = false;
    public static int coinMeter;
    
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private bool isSliding = false;
    [SerializeField] private bool isJumping = false;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Time.timeScale = 0;
        gameObject.SetActive(false);
        hearts = 3;
        collectedCoins = 0;

        if (restarted == true)
        {
            InGameMenu.SetActive(true);
            MainMenu.SetActive(false);
            gameObject.SetActive(true);
            Time.timeScale = 1;
        }
    }
    void Update()
    {
        SpeedUp();
        Movement();
        OnGroundCheck();
    }
    private void LateUpdate()
    {
        Camera();
        Sky();
        Clouds();
        GarbageCollector();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            
            if ( hearts > 0 & hearts <= 3)
            {
                Destroy(other.gameObject);
                HealthDown();    
            }
            
            if (hearts == 0) 
            {
                RestartMenu.SetActive(true);
                Time.timeScale = 0;
                gameObject.SetActive(false);
            }
            
        }

        if (other.gameObject.tag == "Tile")
        {
            isJumping = false;
        }
        
        if (other.gameObject.tag == "Coin")
        {
            Coins();
            Destroy(other.gameObject);
        }
    }
    
    IEnumerator Sliding()
    {
        transform.Rotate(-70,0,0);
        yield return new WaitForSeconds(0.5f);
        transform.Rotate(+70,0,0);
        yield return new WaitForSeconds(0.5f);
        isSliding = false;
    }
    IEnumerator Collision()
    {
        for (int i = 0; i <= 3; i++)
        {
            mesh.enabled = false;
            yield return new WaitForSeconds(0.1f);
            mesh.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    private void Coins()
    {
        coinMeter += 1;
        collectedCoins += 1;
    }
    private void Movement()
         {
             if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
             {
                 if (playerPosition.position.x == -1.5f)
                 {
                     if ( hearts > 0 & hearts <= 3)
                     {
                         HealthDown();    
                     }
                 
                     if (hearts == 0) 
                     {
                         RestartMenu.SetActive(true);
                         Time.timeScale = 0;
                         gameObject.SetActive(false);
                     }
                     playerPosition.position = new Vector3(-1.5f,playerPosition.position.y,playerPosition.position.z);
                 }
                 else
                 {
                     playerPosition.position = new Vector3(playerPosition.position.x - 1f,playerPosition.position.y,playerPosition.position.z);
                 }
             } 
             else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
             {
                 if (playerPosition.position.x == 0.5f)
                 {
                     if ( hearts > 0 & hearts <= 3)
                     {
                         HealthDown();    
                     }
                 
                     if (hearts == 0) 
                     {
                         RestartMenu.SetActive(true);
                         Time.timeScale = 0;
                         gameObject.SetActive(false);
                     }
                     playerPosition.position = new Vector3(0.5f,playerPosition.position.y,playerPosition.position.z);
                 }
                 else
                 {
                     playerPosition.position = new Vector3(playerPosition.position.x + 1f,playerPosition.position.y,playerPosition.position.z);   
                 }
             }
             else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
             {
                 if (isJumping == false)
                 {
                     playerRB.AddForce(Vector3.up * 4f,ForceMode.Impulse);
                     isJumping = true;
                 }
             }
             else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
             {
                 if (isSliding == false)
                 {
                     isSliding = true;
                     StartCoroutine(Sliding());
                     
                 }
                 
             }
             
             transform.position = new Vector3(
                 transform.position.x,
                 transform.position.y,
                 transform.position.z + speed
             );
         }
    private void Camera()
    {
        camera.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z - 1.2f);
    }
    private void Sky()
    {
        sky.position = new Vector3(transform.position.x, transform.position.y - 10.5f, transform.position.z );
    }
    private void Clouds()
    {
        clouds.position = new Vector3(transform.position.x, transform.position.y - 10.5f, transform.position.z);
    }
    private void GarbageCollector()
    {
        garbageCollector.position = new Vector3(0.5f, 0, transform.position.z - 50f);
    }
    private void OnGroundCheck()
    {
        
    }
    private void SpeedUp()
    {
        float secondsToMaxDifficulty = 180;
        float speedup = Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
        speed = Mathf.Lerp(0.03f, 0.07f, speedup );
    }
    private void HealthDown()
    {
        hearts -= 1;
        StartCoroutine(Collision());
    }
}
