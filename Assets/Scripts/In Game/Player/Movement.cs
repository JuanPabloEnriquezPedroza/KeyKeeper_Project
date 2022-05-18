using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Movement : MonoBehaviour
{
#pragma warning disable 649
    //Healt Points
    int healthPoints;
    [SerializeField] AudioClip effect;
    GameObject GameOverMenuUI;
    GameObject HUD;

    //Horizontal Movement
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 11f;
    Vector2 horizontalInput;

    //Gravity
    [SerializeField] float gravity = -30f;
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    //Jump
    [SerializeField] float jumpHeight = 3.5f;
    bool jump;

    void Start()
    {
        GameOverMenuUI = GameObject.FindWithTag("GameOver");
        HUD = GameObject.FindWithTag("HUD");
        if (PersistentData.isGraveyard)
        {
            transform.position = PersistentData.intialPositionGraveyard;
            transform.Rotate(PersistentData.intialViewGraveyard);
        }
        if (PersistentData.isDungeons)
        {
            transform.position = PersistentData.intialPositionDungeons;
            transform.Rotate(PersistentData.intialViewDungeons);
        }
        if (PersistentData.isChurch)
        {
            transform.position = PersistentData.intialPositionChurch;
            transform.Rotate(PersistentData.intialViewChurch);
        }
        if (PersistentData.isCatacombs)
        {
            transform.position = PersistentData.intialPositionCatacombs;
            transform.Rotate(PersistentData.intialViewCatacombs);
        }
        if (PersistentData.isTower)
        {
            transform.position = PersistentData.intialPositionTower;
            transform.Rotate(PersistentData.intialViewTower);
        }
    }

    private void Update()
    {
        if(HUD.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text != "")
        {
            StartCoroutine(HideMessage());
        }

        isGrounded = Physics.CheckSphere(transform.position, .2f, groundMask);

        if(isGrounded)
        {
            verticalVelocity.y = 0;
        }

        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        if(jump && !PauseMenu.isPaused)
        {
            if(isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
            }
            jump = false;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void RecieveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    public void OnJumpPressed()
    {
        jump = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("EnemyWeapon"))
        {
            DamageTaken();
        }
        else if (collider.CompareTag("Obstacle"))
        {
            DamageTaken();
        }
        else if(collider.name == "ChurchToOutside")
        {
            if (PersistentData.keysID[4] == 1)
            {
                PersistentData.intialPositionGraveyard = new Vector3(-16.55531f, 4.09f, 195.28f);
                PersistentData.intialViewGraveyard = new Vector3(0f, 0f, 0f);
                PersistentData.isGraveyard = true;
                PersistentData.isChurch = false;
                PersistentData.isCatacombs = false;
                PersistentData.isTower = false;
                PersistentData.isDungeons = false;
                SceneManager.LoadScene("Graveyard");
            }
            else
            {
                HUD.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("You need the Church key to open this door and escape!");
            }
        }
        else if (collider.name == "OutsideToChurch")
        {
            PersistentData.intialPositionChurch = new Vector3(-16.9f, 1.4f, 30.83f); // Done
            PersistentData.intialViewChurch = new Vector3(0f, 0f, 0f); //Done
            PersistentData.isGraveyard = false;
            PersistentData.isChurch = true;
            PersistentData.isCatacombs = false;
            PersistentData.isTower = false;
            PersistentData.isDungeons = false;
            SceneManager.LoadScene("Church");
        }
        else if (collider.name == "ChurchToGraveyard")
        {
            if(PersistentData.keysID[0] == 1)
            {
                PersistentData.intialPositionGraveyard = new Vector3(-16.69f, 3.93f, 112f); //Done
                PersistentData.intialViewGraveyard = new Vector3(0f, 180f, 0f); //Done
                PersistentData.isGraveyard = true;
                PersistentData.isChurch = false;
                PersistentData.isCatacombs = false;
                PersistentData.isTower = false;
                PersistentData.isDungeons = false;
                SceneManager.LoadScene("Graveyard");
            }
            else
            {
                HUD.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("You need the Graveyard key to open this door!");
            }
        }
        else if (collider.name == "ChurchToCatacombs")
        {
            if(PersistentData.keysID[1] == 1)
            {
                PersistentData.intialPositionCatacombs = new Vector3(27.53f, 49.761f, -459.62f); //Done
                PersistentData.intialViewCatacombs = new Vector3(0f, 0f, 0f); //Done
                PersistentData.isGraveyard = false;
                PersistentData.isChurch = false;
                PersistentData.isCatacombs = true;
                PersistentData.isTower = false;
                PersistentData.isDungeons = false;
                SceneManager.LoadScene("Catacombs");
            }
            else
            {
                HUD.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("You need the Catacombs key to open this door!");
            }
        }
        else if (collider.name == "GraveyardToTower")
        {
            if (PersistentData.keysID[3] == 1)
            {
                PersistentData.intialPositionTower = new Vector3(32.3598f, 111.53f, -506.6821f); //Done
                PersistentData.intialViewTower = new Vector3(0f, 0f, 0f); //Done
                PersistentData.isGraveyard = false;
                PersistentData.isChurch = false;
                PersistentData.isCatacombs = false;
                PersistentData.isTower = true;
                PersistentData.isDungeons = false;
                SceneManager.LoadScene("Tower");
            }
            else
            {
                HUD.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("You need the Tower key to open this door!");
            }
        }
        else if (collider.name == "GraveyardToChurch")
        {
            PersistentData.intialPositionChurch = new Vector3(-17.03f, 1.4f, 111.22f); //Done
            PersistentData.intialViewChurch = new Vector3(0f, 180f, 0f); //Done
            PersistentData.isGraveyard = false;
            PersistentData.isChurch = true;
            PersistentData.isCatacombs = false;
            PersistentData.isTower = false;
            PersistentData.isDungeons = false;
            SceneManager.LoadScene("Church");
        }
        else if (collider.name == "GraveyardToDungeons")
        {
            if (PersistentData.keysID[2] == 1)
            {
                PersistentData.intialPositionDungeons = new Vector3(27.38f, 58.4f, -476f); //Done
                PersistentData.intialViewDungeons = new Vector3(0f, 0f, 0f); //Done
                PersistentData.isGraveyard = false;
                PersistentData.isChurch = false;
                PersistentData.isCatacombs = false;
                PersistentData.isTower = false;
                PersistentData.isDungeons = true;
                SceneManager.LoadScene("Dungeons");
            }
            else
            {
                HUD.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("You need the Dungeons key to open this door!");
            }
        }
        else if (collider.name == "TowerToGraveyard")
        {
            PersistentData.intialPositionGraveyard = new Vector3(-16.69f, 3.93f, 78f); //Done
            PersistentData.intialViewGraveyard = new Vector3(0f, 0f, 0f); //Done
            PersistentData.isGraveyard = true;
            PersistentData.isChurch = false;
            PersistentData.isCatacombs = false;
            PersistentData.isTower = false;
            PersistentData.isDungeons = false;
            SceneManager.LoadScene("Graveyard");
        }
        else if (collider.name == "CatacombsToChurch")
        {
            PersistentData.intialPositionChurch = new Vector3(10.2f, 1.4f, 35.99f); //Done
            PersistentData.intialViewChurch = new Vector3(0f, 0f, 0f); //Done
            PersistentData.isGraveyard = false;
            PersistentData.isChurch = true;
            PersistentData.isCatacombs = false;
            PersistentData.isTower = false;
            PersistentData.isDungeons = false;
            SceneManager.LoadScene("Church");
        }
        else if (collider.name == "DungeonsToGraveyard")
        {
            PersistentData.intialPositionGraveyard = new Vector3(-16.58f, 4.12f, 13.82f); //Done
            PersistentData.intialViewGraveyard = new Vector3(0f, 0f, 0f); //Done
            PersistentData.isGraveyard = true;
            PersistentData.isChurch = false;
            PersistentData.isCatacombs = false;
            PersistentData.isTower = false;
            PersistentData.isDungeons = false;
            SceneManager.LoadScene("Graveyard");
        }
    }

    public void DamageTaken()
    {
        PersistentData.healthPoints--;
        AudioSource.PlayClipAtPoint(effect, transform.position);
        if (PersistentData.healthPoints <= 0)
        {
            Time.timeScale = 0f;
            GameOverMenuUI.GetComponent<GameOverMenu>().SetScreenActive(true);
        }
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(3f);
        HUD.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("");
    }
}

//https://youtu.be/tXDgSGOEatk