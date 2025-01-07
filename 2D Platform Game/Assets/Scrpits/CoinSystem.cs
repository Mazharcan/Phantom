using TMPro;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    private PlayerController playerController;  // PlayerController s�n�f�na eri�mek i�in bir referans olu�turuyoruz.
    public GameObject effect;                   // Toplama efektinin obje referans�.

    private void Start()
    {
        // "Player" tagine sahip objeyi bul ve PlayerController bile�enine eri�.
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // �ki collider'�n birbiriyle temas�n� kontrol eder.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // Temas eden obje "Player" tagine sahipse:
        {
            playerController.coin++; // Player'�n coin say�s�n� bir art�r.
            Destroy(gameObject);     // Coin objesini sahneden kald�r.

            // Belirtilen pozisyonda ve varsay�lan rotasyonla efekt olu�tur.
            Instantiate(effect, transform.position, Quaternion.identity);
        }
    }
}
