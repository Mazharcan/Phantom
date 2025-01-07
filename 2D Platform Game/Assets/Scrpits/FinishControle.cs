using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishControl : MonoBehaviour
{
    private PlayerController playerController;  // Oyuncu kontrol sýnýfýna eriþmek için bir referans
    public GameObject effect;                  // Efekt prefab'ýný tanýmlamak için bir deðiþken
    public GameObject finishPanel;             // Bitiþ panelini temsil eden deðiþken

    private void Start()
    {
        // "Player" tag'ine sahip objeyi bul ve PlayerController bileþenine eriþ
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Eðer "Player" tag'ine sahip bir objeye çarpýlýrsa
        if (collision.gameObject.tag == "Player")
        {
            playerController.coin++;           // Oyuncunun coin sayýsýný bir artýr
            Destroy(gameObject);               // Bu objeyi (bitiþ çizgisini) yok et

            // Efekt prefab'ýný bu objenin pozisyonunda oluþtur
            Instantiate(effect, transform.position, Quaternion.identity);

            finishPanel.SetActive(true);       // Bitiþ panelini görünür yap
            Time.timeScale = 0;                // Oyunu durdur
        }
    }
}