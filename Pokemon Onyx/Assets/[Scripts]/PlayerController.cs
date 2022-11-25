using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum PlayerDirection
{
    UP = 1,
    RIGHT = 2,
    DOWN = 3,
    LEFT = 4,
}

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public LayerMask SolidObjectsLayer;
    public LayerMask FolliageLayer;
    public bool isMoving;
    private bool isSprinting;
    private Vector2 input;
    static public bool encounterActive;

    private Animator animator;

    private PlayerDirection playerDirection;
    private int stepOrder = 0;

    private Vector2 test;

    // Start is called before the first frame update
    void Start()
    {
        encounterActive = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isSprinting = Input.GetButton("Sprint");
        if (isSprinting)
            moveSpeed = 5;
        else
            moveSpeed = 3;


        if (encounterActive == false)
        {

            if (!isMoving)
            {

                input.x = Input.GetAxisRaw("Horizontal");
                input.y = Input.GetAxisRaw("Vertical");


                if (input.y > 0.0f)
                {
                    playerDirection = PlayerDirection.UP;
                }
                else if (input.y < 0.0f)
                {
                    playerDirection = PlayerDirection.DOWN;
                }
                else if (input.x > 0.0f)
                {
                    playerDirection = PlayerDirection.RIGHT;
                }
                else if (input.x < 0.0f)
                {
                    playerDirection = PlayerDirection.LEFT;
                }


                if (input != Vector2.zero)
                {
                    var targetPos = transform.position;
                    targetPos.x += input.x;
                    targetPos.y += input.y;
                    test = targetPos;
                    if (IsWalkable(targetPos))
                    {
                        StartCoroutine(Move(targetPos));

                        RandGrassEnounter(targetPos);

                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            LoadEncounterScene();
        }

    }


    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        stepOrder = stepOrder == 0 ? 1 : 0;
        animator.SetInteger("step", stepOrder);
        animator.SetInteger("direction", (int)playerDirection);
        animator.SetInteger("speed", 1);

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        animator.SetInteger("speed", 0);
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


    void RandGrassEnounter(Vector3 targetPos)
    {
        int encounterRoll = 0;

        if (Physics2D.OverlapCircle(targetPos, 0.1f, FolliageLayer) != null)
        {

            if (moveSpeed == 5)
                encounterRoll = Random.Range(0, 6);
            else if (moveSpeed == 3)
                encounterRoll = Random.Range(0, 10);
            Debug.Log(encounterRoll);

            if (encounterRoll == 0)
            {
                Debug.Log("Encounter!");
                LoadEncounterScene();
                encounterActive = true;

            }
        }
    }

    private void LoadEncounterScene()
    {
        SoundManager.Instance.PlayBgm("BattleBgm");
        FindObjectOfType<DataTransfer>().SetPlayerStatusForBattle();
        SceneManager.LoadScene("EncounterScene", LoadSceneMode.Additive);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(test, 0.1f);
    }
}
