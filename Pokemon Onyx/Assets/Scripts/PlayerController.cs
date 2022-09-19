using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public LayerMask SolidObjectsLayer;
    public LayerMask FolliageLayer;
    public bool isMoving;
    private bool isSprinting;
    private Vector2 input;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isSprinting = Input.GetButton("Sprint");
        if (isSprinting)
            moveSpeed = 5;
        else
            moveSpeed = 3;

        if(!isMoving)
        {

            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                { 
                    StartCoroutine(Move(targetPos));

                    RandGrassEnounter(targetPos);

                }
            }
        }

    }


    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while((targetPos - transform.position).sqrMagnitude>Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.1f, SolidObjectsLayer) != null)
        {
            return false;
        }
        return true;

    }


void RandGrassEnounter (Vector3 targetPos)
    {
        int encounterRoll = 0;

        if (Physics2D.OverlapCircle(targetPos, 0.1f, FolliageLayer) != null)
        {
            
            if (moveSpeed == 5)
                encounterRoll = Random.Range(0, 6);
            else if(moveSpeed == 3)
                encounterRoll = Random.Range(0, 10);
            Debug.Log(encounterRoll);

            if (encounterRoll == 0)
            {
                Debug.Log("Encounter!");

                SceneManager.LoadScene("EncounterScene", LoadSceneMode.Additive);

            }
        }
    }
}
