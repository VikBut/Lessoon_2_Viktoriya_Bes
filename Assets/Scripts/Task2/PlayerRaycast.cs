using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] 
    private Camera m_camera;
    
    [SerializeField]
    private LayerMask m_layerMask;
    
    [SerializeField]
    private float m_raycastDistance = 1f;

    [Header("Inventory")]
    [SerializeField] 
    private InventoryManeger m_inventoryManeger;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit hit;

            if (Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out hit, m_raycastDistance, m_layerMask))
            {
                if (hit.collider.TryGetComponent(out Item item))
                {
                    if (m_inventoryManeger.AddItem(item.item, item.amount))
                    {
                        Destroy(item.gameObject);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (m_camera != null)
        {
            Debug.DrawRay(m_camera.transform.position, m_camera.transform.forward * m_raycastDistance, Color.red);
        }
    }
}