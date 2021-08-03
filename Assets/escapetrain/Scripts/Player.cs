using UnityEngine;

public class Player : MonoBehaviour {

    public float mapwidth;

    public bool gameover;

    void Start()
    {
        gameover = false;
    }
    
	void FixedUpdate()
    {
        #if UNITY_EDITOR
                if (!gameover)
                {
                    if (Input.GetMouseButton(0))
                    {
                
                        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

                        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                        if (hit.collider == null)
                        {
                            Move(curPosition.x);
                        }
                    }
                }
        #endif
        #if UNITY_ANDROID
                if (!gameover)
                {
                    if(transform.position.x > -mapwidth)
                        transform.Translate(Input.acceleration.x * 30 * Time.deltaTime, 0 ,0);

                    if(transform.position.x < mapwidth)
                        transform.Translate(Input.acceleration.x * 30 * Time.deltaTime, 0 ,0);
                }

                if(transform.position.x > mapwidth)
                {
                    transform.position = new Vector3(mapwidth , transform.position.y, transform.position.z);
                }

                if(transform.position.x < -mapwidth)
                {
                    transform.position = new Vector3(-mapwidth , transform.position.y, transform.position.z);
                }
        #endif
        #if UNITY_IOS
                if (!gameover)
                {
                    if(transform.position.x > -mapwidth)
                        transform.Translate(Input.acceleration.x * 30 * Time.deltaTime, 0 ,0);

                    if(transform.position.x < mapwidth)
                        transform.Translate(Input.acceleration.x * 30 * Time.deltaTime, 0 ,0);
                }

                if(transform.position.x > mapwidth)
                {
                    transform.position = new Vector3(mapwidth , transform.position.y, transform.position.z);
                }

                if(transform.position.x < -mapwidth)
                {
                    transform.position = new Vector3(-mapwidth , transform.position.y, transform.position.z);
                }
        #endif
    }

    void Move(float posX)
    {
        if (transform.position.x - 0.4f > posX && transform.position.x > -mapwidth )
        {
            transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y, 0);
        }
        else if (transform.position.x + 0.2f < posX && transform.position.x < mapwidth )
        {
            transform.position = new Vector3(transform.position.x + 0.3f, transform.position.y, 0);
        }
    }



    void OnCollisionEnter2D()
    {
        gameover = true;
        FindObjectOfType<GameManager>().GetComponent<AudioSource>().Stop();
        GameObject.Find("GameoverSound").GetComponent<AudioSource>().Play();
        FindObjectOfType<GameManager>().EndGame();
    }
}
