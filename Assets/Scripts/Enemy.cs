using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PatrolSpots patrolSpots;

    [Header("FOV Settings")]
    public bool hasFOV = true;
    public float fov = 90f;
    public float viewDistance = 3f;

    private FieldOfView fieldOfView;
    [SerializeField] private GameObject fovPrefab;

    [Header("Basic Settings")]
    public float moveSpeed = 5f;

    [HideInInspector]public Vector2 targetPosition;
    [Header("Debug")]
    [SerializeField]private GameObject target;

    Vector2 moveDir;
    private GameObject player;

    private Animator animator;

    public LayerMask layerMask;

    void Start()
    {
        animator = GetComponent<Animator>();

        // init
        targetPosition = transform.position;
        moveDir = this.transform.up;

        player = GameObject.FindGameObjectWithTag("Player");

        if (hasFOV)
        {
            fieldOfView = Instantiate(fovPrefab, null).GetComponent<FieldOfView>();
            fieldOfView.SetFov(fov);
            fieldOfView.SetViewDistance(viewDistance);
        }
    }


    private void Update()
    {
        DrawFov();

        if (player)
        {
            target = null;
            if (Vector2.Distance(transform.position, player.transform.position) < viewDistance)
            {
                Vector2 dirToPlayer = (player.transform.position - transform.position).normalized;
                if (Vector2.Angle(transform.up, dirToPlayer) < fov / 2f)
                {
                    RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, dirToPlayer, viewDistance, layerMask);
                    if (hitInfo.collider != null)
                    {
                        if (hitInfo.collider.CompareTag("Player"))
                        {
                            Debug.Log("Found Player!");

                            target = player;
                            targetPosition = target.transform.position;

                            if (animator.GetBool("isChasing")==false)
                            {
                                animator.SetBool("isChasing", true);
                            }
                        }
                        else
                        {
                            //
                        }
                    }
                }
            }
            if (target == null)
            {
                if (animator.GetBool("isChasing") == true)
                {
                    animator.SetBool("isChasing", false);
                }
            }
        }
    }

    /*
        void Update()
        {
            //Debug.DrawLine(transform.position, this.transform.up * 3f + transform.position);

            // Chase
            if (Vector2.Distance(targetPosition, transform.position) > 0.1f)
            {
                moveDir = (targetPosition - (Vector2)transform.position).normalized;
                transform.position += (Vector3)moveDir * moveSpeed * Time.deltaTime;
            }
            this.transform.up = moveDir;


            if (target != null)
            {
                targetPosition = target.transform.position;
            }

            target = null;

            if (fieldOfView == null) return;

            DrawFov();

            if (player)
            {
                if (Vector2.Distance(transform.position, player.transform.position) < viewDistance)
                {
                    Vector2 dirToPlayer = (player.transform.position - transform.position).normalized;
                    if (Vector2.Angle(transform.up, dirToPlayer) < fov /2f)
                    {
                        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, dirToPlayer, viewDistance);
                        if (hitInfo.collider != null)
                        {
                            if (hitInfo.collider.CompareTag("Player"))
                            {
                                //Debug.Log("Found Player!");
                                target = player;
                            }
                            else
                            {
                                //
                            }
                        }
                    }
                }
            }
        }
    */

    void DrawFov()
    {
        fieldOfView.SetOrigin(this.transform.position);
        fieldOfView.SetAimDirection(this.transform.up);
        //fieldOfView.SetAimDirection(moveDir);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Player>().Die();
        }
    }

}
