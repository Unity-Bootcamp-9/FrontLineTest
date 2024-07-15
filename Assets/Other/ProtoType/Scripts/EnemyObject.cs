using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    public void Hit()
    {
        Destroy(gameObject);
    }
}
