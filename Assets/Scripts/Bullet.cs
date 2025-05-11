using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
           print("Enemy hit!"); // Düşmana çarptığında yapılacak işlemler
            Destroy(gameObject); // Mermiyi yok et
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Duvar ile çarpışma durumunda yapılacak işlemler
            Destroy(gameObject); // Mermiyi yok et
        }
    }
}
