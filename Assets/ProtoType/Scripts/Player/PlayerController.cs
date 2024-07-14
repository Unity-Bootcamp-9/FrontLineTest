using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Button fireButton;
    public event Action fireGun;
    public EventTrigger trigger;

    private void Start()
    {
        fireButton.onClick.AddListener(Fire);
    }

    public void Fire()
    {
        fireGun.Invoke();
    }

}
