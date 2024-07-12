using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Button fireButton;
    public event Action fireGun;

    private void Start()
    {
        fireButton.onClick.AddListener(Fire);
    }

    private void Fire()
    {
        fireGun.Invoke();
    }
}
