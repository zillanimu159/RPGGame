using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    public LayerMask groundLayer;
    public static float jumpPow;
    public int test;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        groundLayer = LayerMask.GetMask("Ground");
        jumpPow = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, new Vector2(0,-1), .8f, groundLayer);
        if (Input.GetAxis("Jump") == 1 && hit.collider != null) {
            rb.AddForce(new Vector2(0, jumpPow), ForceMode2D.Impulse);
            Debug.Log("AAAAAAA");
        }
        Vector2 moveVector;
        if (hit.collider != null)
        {
            moveVector = new Vector2(8 * Input.GetAxis("Horizontal"), 0);

        }
        else {
            moveVector = new Vector2(3 * Input.GetAxis("Horizontal"), 0);
        }
        rb.AddForce(moveVector, ForceMode2D.Force);

        //Tested scene transition between Overworld and any Scene, add code underneath in other scene to bring back objects
        if (Input.GetKeyDown("space")) {
            foreach (GameObject someObject in SceneManager.GetActiveScene().GetRootGameObjects()) {
                someObject.SetActive(false);
            }
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }
        /*foreach (GameObject someObject in SceneManager.GetSceneAt(0).GetRootGameObjects())
        {
            someObject.SetActive(true);
        }*/
    }
    
    
}
