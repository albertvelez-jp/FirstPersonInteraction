using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public Camera playerCamera;
    public LayerMask pickupLayer;

    public float pickupRange;
    public float minDistance;
    public float maxDistance;
    public float moveSpeed;
    public float scrollSpeed;

    private GameObject heldObject;
    private Rigidbody heldRb;
    private float heldDistance;

    void Update()
    {
        if (heldObject == null)
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.green);

            if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
            {
                if (Input.GetMouseButton(0))
                {
                    heldObject = hit.collider.gameObject;
                    heldRb = heldObject.GetComponent<Rigidbody>();
                    heldDistance = hit.distance;

                    if (heldRb != null)
                    {
                        heldRb.useGravity = false;
                        heldRb.linearVelocity = Vector3.zero;
                        heldRb.angularVelocity = Vector3.zero;
                        heldRb.linearDamping = 10f;
                        heldRb.angularDamping = 5f;
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (heldRb != null)
                {
                    heldRb.useGravity = true;
                    heldRb.linearDamping = 0f;
                    heldRb.angularDamping = 0.05f;
                }
                heldObject = null;
                heldRb = null;
            }
        }

        if (heldObject != null)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            heldDistance += scroll * scrollSpeed;
            heldDistance = Mathf.Clamp(heldDistance, minDistance, maxDistance);
            Vector3 targetPos = playerCamera.transform.position + playerCamera.transform.forward * heldDistance;
            Vector3 moveDir = targetPos - heldObject.transform.position;
            heldRb.linearVelocity = moveDir * moveSpeed;
        }
    }
}