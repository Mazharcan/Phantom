using TMPro;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    private PlayerController playerController;  // PlayerController sýnýfýna eriþmek için bir referans oluþturuyoruz.
    public GameObject effect;                   // Toplama efektinin obje referansý.

    private void Start()
    {
        // "Player" tagine sahip objeyi bul ve PlayerController bileþenine eriþ.
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Ýki collider'ýn birbiriyle temasýný kontrol eder.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // Temas eden obje "Player" tagine sahipse:
        {
            playerController.coin++; // Player'ýn coin sayýsýný bir artýr.
            Destroy(gameObject);     // Coin objesini sahneden kaldýr.

            // Belirtilen pozisyonda ve varsayýlan rotasyonla efekt oluþtur.
            Instantiate(effect, transform.position, Quaternion.identity);
        }
    }
}
