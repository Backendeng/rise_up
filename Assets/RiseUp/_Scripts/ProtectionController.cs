using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProtectionController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{ 
    public Protection protection;
    private Vector3 lastMousePosition, lastPos;
    private Vector2 limitSize;

    void Start()
    {
        Vector2 canvasSize = transform.parent.GetComponent<RectTransform>().sizeDelta * transform.parent.GetComponent<RectTransform>().localScale.x;
        limitSize = new Vector2(canvasSize.x / 2 - protection.GetComponent<CircleCollider2D>().radius, canvasSize.y / 2 - protection.GetComponent<CircleCollider2D>().radius);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!MainController.IsClassicMode() && MainController.IsLoaded())
        {
            MainController.instance.StartGame();
        }
        if (MainController.IsPlaying())
        {
            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lastPos = protection.transform.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (MainController.IsPlaying())
        {
            Vector3 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 delta = currMousePos - lastMousePosition;
            Vector2 newPos = lastPos + delta;
            newPos.x = Mathf.Clamp(newPos.x, -limitSize.x, limitSize.x);
            newPos.y = Mathf.Clamp(newPos.y, -limitSize.y, limitSize.y);

            protection.rb.MovePosition(newPos);
            protection.RemoveVelocity();

            if (newPos.x == -limitSize.x || newPos.x == limitSize.x || newPos.y == -limitSize.y || newPos.y == limitSize.y)
            {
                lastMousePosition = currMousePos;
                lastPos = protection.transform.position;
            }
        }
    }

    void Update()
    {
        Vector2 newPos = protection.transform.position;
        newPos.x = Mathf.Clamp(newPos.x, -limitSize.x, limitSize.x);
        newPos.y = Mathf.Clamp(newPos.y, -limitSize.y, limitSize.y);
        if (protection.transform.position.x != newPos.x || protection.transform.position.y != newPos.y)
        {
            protection.rb.MovePosition(newPos);
            protection.RemoveVelocity();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
