using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] 
    private Camera m_camera;
    
    [SerializeField]
    private LayerMask _layerMask; // m_
    
    [SerializeField]
    private float _raycastDistance = 1f;

    [Header("Inventory")]
    [SerializeField] 
    private InventoryManeger _inventoryManeger; //m_

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Старый инпут
        {
            RaycastHit hit;

            if (Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out hit, _raycastDistance, _layerMask))
            {
                if (hit.collider.TryGetComponent(out Item _item)) // префикс _
                {
                    if (_inventoryManeger.AddItem(_item.item, _item.amount))
                    {
                        Destroy(_item.gameObject);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (m_camera != null)
        {
            Debug.DrawRay(m_camera.transform.position, m_camera.transform.forward * _raycastDistance, Color.red);
        }
    }
}
