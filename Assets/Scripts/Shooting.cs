using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public Button shootingButton;
    public Image cursor;
    public LayerMask EnemyLayerMask;

    void Start()
    {
        // Add listener to the button's onClick event
        shootingButton.onClick.AddListener(Shoot);
    }
    
    public void Shoot()
    {
        // Shoot from the cursor
        Ray ray = Camera.main.ScreenPointToRay(cursor.transform.position);

        // if the cursor hits an enemy
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, EnemyLayerMask))
        {
            // Do something
            Debug.Log(hit.transform.gameObject.name + " takes X amount of DAMAGE");
        }
    }
}
