using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

enum MonsterState
{
    Chase, Attack, Move, Dead
}

public class Monster : MonoBehaviour
{
    private string monsterName;
    [SerializeField]
    private float hp;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private float attackRange;

    private MonsterState myState;
    private Vector3 playerPos;
    private Rigidbody myRigidbody;

    private float timeCount = 0;
    private Vector3 movePos;

    private float moveTime;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        myState = MonsterState.Chase;
        playerPos = Camera.main.transform.position; // �÷��̾� ������Ʈ�� ������ �װɷ� ����
    }

    // ���� �������ͽ� ���� �޾ƿ���
    public void SetData()
    {

    }

    void Update()
    {
        switch (myState)
        {
            case MonsterState.Chase:
                if (Vector3.Distance(transform.position, playerPos) <= attackRange)
                {
                    myRigidbody.velocity = Vector3.zero;
                    myState = MonsterState.Attack;
                    break;
                }
                // �ִϸ��̼� �ֱ� 

                transform.LookAt(playerPos);
                myRigidbody.velocity = transform.forward * moveSpeed;
                break;
            case MonsterState.Attack:
                timeCount += Time.deltaTime; // �ӽ÷� ������Ű�� ���� �ڵ�
                // �ִϸ��̼� �ֱ�

                Debug.Log("����");
                if (timeCount >= 1f)
                {
                    myState = MonsterState.Move;
                    timeCount = 0;
                }
                break;
            case MonsterState.Move:
                if (timeCount <= 0)
                {
                    float moveX = Random.Range(-0.3f, 0.3f);
                    float moveY = Random.Range(-0.1f, 0.1f);
                    float moveZ = Random.Range(-0.3f, 0.3f);
                    moveTime = Random.Range(0.5f, 1.5f);
                    movePos = new Vector3(moveX, moveY, moveZ);
                }
                // �̵� �ִϸ��̼� �ֱ�
                myRigidbody.velocity = movePos.normalized * moveSpeed;
                timeCount += Time.deltaTime;

                if (moveTime <= timeCount)
                {
                    myState = MonsterState.Chase;
                    timeCount = 0;
                }
                break;
            case MonsterState.Dead:
                // �״� �ִϸ��̼� �ֱ�
                break;
        }
    }

    public void GetDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        myState = MonsterState.Dead;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
