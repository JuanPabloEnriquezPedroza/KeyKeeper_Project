using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collctibles : MonoBehaviour
{
    public int id = 0; //To know where the key is.
    [SerializeField] AudioClip effect;
    GameObject HUD;
    string[] places = new string[] { "Graveyard", "Catacombs", "Dungeons", "Tower", "Church" };

    void Start()
    {
        HUD = GameObject.FindWithTag("HUD");
        if (PersistentData.keysID[id] == 1)
        {
            Destroy(gameObject);
            Destroy(gameObject.transform.parent.GetChild(2).gameObject);
            Destroy(gameObject.transform.parent.GetChild(3).gameObject);
        }
    }

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(30, 0, 0) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(id != 4)
            {
                string text = "You obtained: " + places[id] + " key";
                HUD.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(text);
                StartCoroutine(CollectibleCollected());
            }
            else
            {
                string text = "You obtained: " + places[id] + " key.\nNow you can escape!\n(This is the end of the game, now you can freely explore the map.)";
                HUD.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(text);
                StartCoroutine(CollectibleCollected());
            }
        }
    }

    IEnumerator CollectibleCollected()
    {
        AudioSource.PlayClipAtPoint(effect, transform.position);
        PersistentData.obtainedKeys++;
        PersistentData.keysID[id] = 1;
        Destroy(gameObject);
        Destroy(gameObject.transform.parent.GetChild(2).gameObject);
        Destroy(gameObject.transform.parent.GetChild(3).gameObject);
        yield return null;
    }
}
