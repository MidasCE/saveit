using UnityEngine;

public class DragDropObject : MonoBehaviour
{
    private Camera _mainCamera;
    private bool _isDragging = false;
    private Vector3 _dragStartOffset;
    private float _zCoord;

    private Rigidbody _rb;

    void Start()
    {
        _mainCamera = Camera.main;
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
#if UNITY_EDITOR
        HandleMouseDrag();
#else
        HandleTouchDrag();
#endif
    }

    // For handling touch input
    void HandleTouchDrag()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        Vector3 touchPos = touch.position;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                TryStartDrag(touchPos);
                break;

            case TouchPhase.Moved:
                if (_isDragging)
                    DragObject(touchPos);
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (_isDragging)
                    EndDrag();
                break;
        }
    }

    // For handling mouse input (for testing in editor)
    void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryStartDrag(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && _isDragging)
        {
            DragObject(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            EndDrag();
        }
    }

    // Start dragging process
    void TryStartDrag(Vector3 screenPosition)
    {
        Ray ray = _mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            _zCoord = _mainCamera.WorldToScreenPoint(transform.position).z;

            Vector3 worldPoint = _mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, _zCoord));
            _dragStartOffset = transform.position - worldPoint;

            _isDragging = true;
            _rb.useGravity = false;
            _rb.isKinematic = true;
        }
    }

    // Drag the object
    void DragObject(Vector3 screenPosition)
    {
        Vector3 worldPoint = _mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, _zCoord));
        // Update both X and Y, leaving Z unchanged
        float newX = worldPoint.x + _dragStartOffset.x;
        float newY = worldPoint.y + _dragStartOffset.y;

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    // End drag process
    void EndDrag()
    {
        _isDragging = false;
        _rb.useGravity = true;
        _rb.isKinematic = false;
    }
}
