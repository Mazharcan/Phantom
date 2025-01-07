using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishControl : MonoBehaviour
{
    private PlayerController playerController;  // Oyuncu kontrol s�n�f�na eri�mek i�in bir referans
    public GameObject effect;                  // Efekt prefab'�n� tan�mlamak i�in bir de�i�ken
    public GameObject finishPanel;             // Biti� panelini temsil eden de�i�ken

    private void Start()
    {
        // "Player" tag'ine sahip objeyi bul ve PlayerController bile�enine eri�
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // E�er "Player" tag'ine sahip bir objeye �arp�l�rsa
        if (collision.gameObject.tag == "Player")
        {
            playerController.coin++;           // Oyuncunun coin say�s�n� bir art�r
            Destroy(gameObject);               // Bu objeyi (biti� �izgisini) yok et

            // Efekt prefab'�n� bu objenin pozisyonunda olu�tur
            Instantiate(effect, transform.position, Quaternion.identity);

            finishPanel.SetActive(true);       // Biti� panelini g�r�n�r yap
            Time.timeScale = 0;                // Oyunu durdur
        }
    }
}