using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerseguirPlayer1 : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform limitPointA;
    public Transform limitPointB;
    public float moveSpeed = 2f;
    public float chaseSpeed = 4f;
    public float detectionRange = 5f;
    
    public Transform target;
    public bool estaSeguindo = false;
    public bool isMovingTowardsPointA = true;
    public float distanceToPlayer;
    
    public static EnemyController2 instance;
    
    public Animator anim;
    
    private void Awake()
        {
            instance = this;
        }
    
    void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    
    public void Update()
        {
            distanceToPlayer = Vector2.Distance(transform.position, target.position);
            
            anim.SetFloat("distanciaPlayer", distanceToPlayer);
            
            float valorAB = Vector2.Distance(transform.position, (pointB.position + pointA.position)/2);
            anim.SetFloat("distanciaMeio", valorAB);
        }
    
    public void AnimUpdate()
        {
            if (distanceToPlayer < detectionRange)
            {
                estaSeguindo = true;
            }
            else
            {
                estaSeguindo = false;
            }
    
            if (estaSeguindo)
            {
                if (transform.position.x > target.position.x)
                {
                    transform.eulerAngles = (Vector3)new Vector2(0f, 180f);
                }
                else
                {
                    transform.eulerAngles = (Vector3)new Vector2(0f, 0f);
                }
                SeguePlayer();
            }
            else
            {
                Patrula();
            }
        }
    
    public void Patrula()
        {
            if (isMovingTowardsPointA)
            {
                transform.eulerAngles = (Vector3)new Vector2(0f, 0f);
                transform.position = Vector2.MoveTowards(transform.position, pointA.position, moveSpeed * Time.deltaTime);
    
                if (Vector2.Distance(transform.position, pointA.position) < 0.1f)
                {
                    isMovingTowardsPointA = false;
                }
            }
            else
            {
                transform.eulerAngles = (Vector3)new Vector2(0f, 180f);
                transform.position = Vector2.MoveTowards(transform.position, pointB.position, moveSpeed * Time.deltaTime);
    
                if (Vector2.Distance(transform.position, pointB.position) < 0.1f)
                {
                    isMovingTowardsPointA = true;
                }
            }
        }
    
    public void SeguePlayer()
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
            
            if (Vector2.Distance(transform.position, limitPointA.position) < 0.1f)
            {
                estaSeguindo = false;
                isMovingTowardsPointA = true;
            }
            else if (Vector2.Distance(transform.position, limitPointB.position) < 0.1f)
            {
                estaSeguindo = false;
                isMovingTowardsPointA = false;
            }
}
