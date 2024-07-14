using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTest : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    private bool isPointerDown = false;
    private float pointerDownTimer = 0f;
    private bool isHeld = false;

    [SerializeField] private float requiredHoldTime = 1f;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        pointerDownTimer = 0f;
        isHeld = false;
        Debug.Log("Pointer Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;

        if (pointerDownTimer >= requiredHoldTime)
        {
            isHeld = true;
            Debug.Log("Pointer Held");
        }

        Debug.Log("Pointer Up");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isHeld && pointerDownTimer < requiredHoldTime)
        {
            Debug.Log("Pointer Click");
        }
    }

    private void Update()
    {
        if (isPointerDown)
        {
            pointerDownTimer += Time.deltaTime;

            if (pointerDownTimer >= requiredHoldTime)
            {
                isHeld = true;
            }
        }
    }
}
