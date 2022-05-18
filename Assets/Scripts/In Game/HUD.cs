using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Image heart;
    [SerializeField] Image heartMissing;
    [SerializeField] Image key;
    [SerializeField] Image keyMissing;
    Image[] health;
    Image[] initialHealth;
    Image[] keys;
    Image[] totalKeys;

    void Awake()
    {
        health = new Image[PersistentData.healthPoints];
        initialHealth = new Image[PersistentData.initialHealthPoints];
        keys = new Image[PersistentData.totalKeys];
        totalKeys = new Image[PersistentData.totalKeys];

        for (int i = 0; i < PersistentData.initialHealthPoints; i++)
        {
            Vector2 newPivot = new Vector2(heartMissing.GetComponent<RectTransform>().pivot.x + 0.002f + (.06f * i), heartMissing.GetComponent<RectTransform>().pivot.y);
            initialHealth[i] = Instantiate(heartMissing, transform);
            initialHealth[i].GetComponent<RectTransform>().pivot = newPivot;
            initialHealth[i].name = "lifeMissing_" + i;
            initialHealth[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < PersistentData.healthPoints; i++)
        {
            Vector2 newPivot = new Vector2(heart.GetComponent<RectTransform>().pivot.x + 0.002f + (.06f * i), heart.GetComponent<RectTransform>().pivot.y);
            health[i] = Instantiate(heart,transform);
            health[i].GetComponent<RectTransform>().pivot = newPivot;
            health[i].name = "life_" + i;
            health[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < PersistentData.totalKeys; i++)
        {
            Vector2 newPivot1 = new Vector2(keyMissing.GetComponent<RectTransform>().pivot.x - .01f, keyMissing.GetComponent<RectTransform>().pivot.y + (.07f * i));
            totalKeys[i] = Instantiate(keyMissing, transform);
            totalKeys[i].GetComponent<RectTransform>().pivot = newPivot1;
            totalKeys[i].name = "missingKey_" + i;
            totalKeys[i].gameObject.SetActive(true);

            Vector2 newPivot2 = new Vector2(key.GetComponent<RectTransform>().pivot.x - .01f, key.GetComponent<RectTransform>().pivot.y + (.07f * i));
            keys[i] = Instantiate(key, transform);
            keys[i].GetComponent<RectTransform>().pivot = newPivot2;
            keys[i].name = "key_" + i;
            keys[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < PersistentData.obtainedKeys; i++)
        {
            keys[i].gameObject.SetActive(true);
        }
    }

    void Update()
    {
        int i = PersistentData.healthPoints;
        if(i < health.Length && i >= 0) health[i].enabled = false;
        int j = PersistentData.obtainedKeys - 1;
        if (j < keys.Length && j >= 0) keys[j].gameObject.SetActive(true);
    }
}
