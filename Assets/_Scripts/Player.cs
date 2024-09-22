using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public bool isPlayer1, isPlayer2;

    public BreadRecipes breadRecipes;
    public int itemInHand;

    public bool isEnterCollider;
    public string colliderTag;
    public GameObject colliderItem;

    Vector3 playerFixPosition;
    float direction_x, direction_z;
    public float rotationSpeed;
    public Rigidbody rb;
    public GameObject[] bread;
    public GameObject hand;
    public GameObject gameWinPanel;

    public Bake bakeScript;
    public LevelManagerment levelManagerment;
    public bool isDizzy, isUnableToMove;
    public float dizzyTime, dizzyTime_count;

    public RoddingArrow roddingArrow;
    public GameObject roddingUI;
    public RaycastHit hit;

    public Animator ani;

    public GameObject totemWall;
    public Material totemWall_after_material;
    public GameObject fire, cutSmoke, rollSmoke;
    public Player otherPlayer;
    public GameObject halo;

    public AudioSource audioSource, audioSource_running;
    public AudioClip[] clips;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerFixPosition = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        if (levelManagerment.isGaming == true)
        {
            if (isDizzy == false)
            {
                /*if (isUnableToMove == false)
                {
                    if (isPlayer1 && !isPlayer2)
                    {
                        //PlayerMoving_Player1_rast();
                        PlayerMoving_Player1();
                    }
                    if (!isPlayer1 && isPlayer2)
                    {
                        PlayerMoving_Player2();
                    }
                }*/
                NoClickMovingKey_Player1();
                NoClickMovingKey_Player2();
                ClickOperateKey();
            }

            else if (isDizzy == true)
            {
                dizzyTime_count += Time.deltaTime;
                if (dizzyTime_count >= dizzyTime)
                {
                    dizzyTime_count = 0;
                    isDizzy = false;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (levelManagerment.isGaming == true)
        {
            if (isDizzy == false)
            {
                if (isUnableToMove == false)
                {
                    if (isPlayer1 && !isPlayer2)
                    {
                        //PlayerMoving_Player1_rast();
                        PlayerMoving_Player1();
                    }
                    if (!isPlayer1 && isPlayer2)
                    {
                        PlayerMoving_Player2();
                    }
                }
               // ClickOperateKey();
            }
        }
        }

    void PlayerMoving_Player1_rast()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            //rb.velocity = Vector3.zero;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //rb.MovePosition(transform.position + transform.right * speed * Time.deltaTime);

                horizontalInput -= 1f; // Move left
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //rb.MovePosition(transform.position - transform.right * speed * Time.deltaTime);

                horizontalInput += 1f; // Move right
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
                verticalInput += 1f; // Move forward
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //rb.MovePosition(transform.position - transform.forward * speed * Time.deltaTime);
                verticalInput -= 1f; // Move backward
            }

            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

            movement.Normalize();
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 2), transform.forward, Color.green, 10);

            //rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), movement, out hit, 3))
            {
                // 处理碰撞逻辑
            }
            else
            {
                transform.position += movement * speed * Time.deltaTime;
                PlayerRotation(movement);
            }
        }



        /*if (movement != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(movement, Vector3.up);

            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, newRotation, rotationSpeed);

        }*/
        /*if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.position += Vector3.forward * speed * Time.deltaTime;
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, new Quaternion(0,0,0,0), rotationSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.position -= Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position -= Vector3.right * speed * Time.deltaTime;
        }
        if(rb.velocity.x != 0)
        {

        }*/
        /*direction_x = gameObject.transform.position.x - playerFixPosition.x;
        direction_z = gameObject.transform.position.z - playerFixPosition.z;
        Quaternion newRotation = Quaternion.LookRotation(new Vector3(direction_x,0,direction_z), gameObject.transform.up);

        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, newRotation, rotationSpeed);*/
    }
    void PlayerMoving_Player1()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;
        /*if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            ani.SetBool("isRunning", false);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }*/
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            audioSource_running.volume = 1;
            ani.SetBool("isRunning", true);
            horizontalInput -= 1f; // Move left
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            audioSource_running.volume = 1;
            ani.SetBool("isRunning", true);
            horizontalInput += 1f; // Move right
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            audioSource_running.volume = 1;
            ani.SetBool("isRunning", true);
            verticalInput += 1f; // Move forward
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            audioSource_running.volume = 1;
            ani.SetBool("isRunning", true);
            verticalInput -= 1f; // Move backward
        }

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        movement.Normalize();
        //rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
        rb.velocity = movement * speed;
        //transform.position += movement * speed * Time.deltaTime;
        PlayerRotation(movement);
    }
    void NoClickMovingKey_Player1()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            audioSource_running.volume = 0;
            ani.SetBool("isRunning", false);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            //rb.velocity = Vector3.zero;
        }
    }
    void PlayerMoving_Player2()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;
        /*if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            ani.SetBool("isRunning", false);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }*/
        if (Input.GetKey(KeyCode.A))
        {
            audioSource_running.volume = 1;
            ani.SetBool("isRunning", true);
            horizontalInput -= 1f; // Move left
        }
        if (Input.GetKey(KeyCode.D))
        {
            audioSource_running.volume = 1;
            ani.SetBool("isRunning", true);
            horizontalInput += 1f; // Move right
        }
        if (Input.GetKey(KeyCode.W))
        {
            audioSource_running.volume = 1;
            ani.SetBool("isRunning", true);
            verticalInput += 1f; // Move forward
        }
        if (Input.GetKey(KeyCode.S))
        {
            audioSource_running.volume = 1;
            ani.SetBool("isRunning", true);
            verticalInput -= 1f; // Move backward
        }

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        movement.Normalize();
        //rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
        rb.velocity = movement * speed;
        //transform.position += movement * speed * Time.deltaTime;
        PlayerRotation(movement);
    }
    void NoClickMovingKey_Player2()
    {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            audioSource_running.volume = 0;
            ani.SetBool("isRunning", false);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            //rb.velocity = Vector3.zero;
        }
    }
    void PlayerRotation(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(direction, Vector3.up);

            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, newRotation, rotationSpeed);

        }
    }
 
    public void ClickOperateKey()
    {
        if ((Input.GetKeyDown(KeyCode.Space) && isPlayer1) || (Input.GetKeyDown(KeyCode.Z) && isPlayer2))
        {
            if (isEnterCollider)
            {
                if (colliderTag == "Dough_Fried")
                {
                    ani.SetBool("isTaking", true);
                    itemInHand = breadRecipes.DoughCabinet_Fried(itemInHand);
                    Instantiate(bread[0], hand.transform);
                }
                else if (colliderTag == "Dough_Bake")
                {
                    ani.SetBool("isTaking", true);
                    itemInHand = breadRecipes.DoughCabinet_Bake(itemInHand);
                    Instantiate(bread[1], hand.transform);
                }
                else if (colliderTag == "Bake")
                {
                    if ((itemInHand == 6 || itemInHand == 5 || itemInHand == 7) && colliderItem.GetComponent<Bake>().isEmpty == true)
                    {
                        audioSource.PlayOneShot(clips[3]);
                        colliderItem.GetComponent<Bake>().itemNum = itemInHand;
                        colliderItem.GetComponent<Bake>().isEmpty = false;
                        colliderItem.GetComponent<Bake>().isBake = true;
                        colliderItem.GetComponent<Bake>().bakeEnergyBar.SetActive(true);
                        ani.SetBool("isTaking", false);
                        bakeScript.whiteSmoke.SetActive(true);
                        bakeScript.blackSmoke.SetActive(false);
                        if (bakeScript.haveFirewood == true)
                            bakeScript.smokeGroup.SetActive(true);
                        else if(bakeScript.haveFirewood == false)
                            bakeScript.smokeGroup.SetActive(false);
                        itemInHand = 0;
                        for (int i = 0; i < hand.transform.childCount; i++)
                        {
                            Destroy(hand.transform.GetChild(i).gameObject);
                        }
                        Debug.Log("空手");
                    }
                    else if (itemInHand == 0 && colliderItem.GetComponent<Bake>().isEmpty == false)
                    {
                        switch (colliderItem.GetComponent<Bake>().itemNum)
                        {
                            case 5:
                                itemInHand = 5;
                                ani.SetBool("isTaking", true);
                                Instantiate(bread[4], hand.transform);
                                Debug.Log("桿好的麵包(烤的)");
                                break;
                            case 6:
                                itemInHand = 6;
                                ani.SetBool("isTaking", true);
                                Instantiate(bread[5], hand.transform);
                                Debug.Log("切好桿好的麵包(烤的)");
                                break;
                            case 7:
                                itemInHand = 7;
                                ani.SetBool("isTaking", true);
                                Instantiate(bread[6], hand.transform);
                                Debug.Log("桿好切好的麵包(烤的)");
                                break;
                            case 9:
                                itemInHand = 9;
                                ani.SetBool("isTaking", true);
                                Instantiate(bread[8], hand.transform);
                                Debug.Log("法棍(熟的)");
                                break;
                            case 10:
                                itemInHand = 10;
                                ani.SetBool("isTaking", true);
                                Instantiate(bread[9], hand.transform);
                                Debug.Log("吐司(熟的)");
                                break;
                            case 11:
                                itemInHand = 11;
                                ani.SetBool("isTaking", true);
                                Instantiate(bread[10], hand.transform);
                                Debug.Log("可頌(熟的)");
                                break;
                            case 19:
                                itemInHand = 19;
                                ani.SetBool("isTaking", true);
                                Instantiate(bread[18], hand.transform);
                                Debug.Log("法棍(焦的)");
                                break;
                            case 20:
                                itemInHand = 20;
                                ani.SetBool("isTaking", true);
                                Instantiate(bread[19], hand.transform);
                                Debug.Log("吐司(焦的)");
                                break;
                            case 21:
                                itemInHand = 21;
                                ani.SetBool("isTaking", true);
                                Instantiate(bread[20], hand.transform);
                                Debug.Log("可頌(焦的)");
                                break;
                        }
                        bakeScript.smokeGroup.SetActive(false);
                        bakeScript.whiteSmoke.SetActive(false);
                        bakeScript.blackSmoke.SetActive(false);
                        colliderItem.GetComponent<Bake>().isEmpty = true;
                        colliderItem.GetComponent<Bake>().ResetValue();
                    }
                    //itemInHand = breadRecipes.BakeCabinet(itemInHand);
                }
                else if (colliderTag == "Fryer")
                {
                    if (itemInHand == 3 && colliderItem.GetComponent<Fryer>().isEmpty == true)
                    {
                        audioSource.PlayOneShot(clips[2]);
                        colliderItem.GetComponent<Fryer>().itemNum = itemInHand;
                        colliderItem.GetComponent<Fryer>().isEmpty = false;
                        colliderItem.GetComponent<Fryer>().isBake = true;
                        colliderItem.GetComponent<Fryer>().fryerEnergyBar.SetActive(true);
                        colliderItem.GetComponent<Fryer>().whiteSmoke.SetActive(true);
                        colliderItem.GetComponent<Fryer>().blackSmoke.SetActive(false);
                        colliderItem.GetComponent<Fryer>().smokeGroup.SetActive(true);
                        ani.SetBool("isTaking", false);
                        itemInHand = 0;
                        for (int i = 0; i < hand.transform.childCount; i++)
                        {
                            Destroy(hand.transform.GetChild(i).gameObject);
                        }
                        Debug.Log("空手");
                    }
                    else if (itemInHand == 0 && colliderItem.GetComponent<Fryer>().isEmpty == false)
                    {
                        switch (colliderItem.GetComponent<Fryer>().itemNum)
                        {
                            case 3:
                                itemInHand = 3;
                                ani.SetBool("isTaking", true);
                                Instantiate(bread[2], hand.transform);
                                Debug.Log("切好的麵包(炸的)");
                                break;
                            case 8:
                                itemInHand = 8;
                                ani.SetBool("isTaking", true);
                                Instantiate(bread[7], hand.transform);
                                Debug.Log("甜甜圈(熟的)");
                                break;
                            case 18:
                                itemInHand = 18;
                                ani.SetBool("isTaking", true);
                                Instantiate(bread[17], hand.transform);
                                Debug.Log("甜甜圈(焦的)");
                                break;
                        }
                        colliderItem.GetComponent<Fryer>().smokeGroup.SetActive(false);

                        colliderItem.GetComponent<Fryer>().isEmpty = true;
                        colliderItem.GetComponent<Fryer>().ResetValue();
                    }
                    //itemInHand = breadRecipes.FryerCabinet(itemInHand);
                }
                else if (colliderTag == "CuttingBoard")
                {
                    if(isUnableToMove == false && (itemInHand == 1 || itemInHand == 2 || itemInHand == 5 || itemInHand == 10))
                    {
                        audioSource.PlayOneShot(clips[0]);

                        itemInHand = breadRecipes.CuttingBoardCabinet(itemInHand);
                        for (int i = 0; i < hand.transform.childCount; i++)
                        {
                            Destroy(hand.transform.GetChild(i).gameObject);
                        }
                        isUnableToMove = true;
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                        if(levelManagerment.levelNum == 0)
                        {
                            PlayerRotation(new Vector3(-1, 0, 0));
                        }
                        else
                        {
                            PlayerRotation(new Vector3(0, 0, -1));
                        }
                        ani.SetBool("isCutting", true);
                        hand.SetActive(false);
                        cutSmoke.SetActive(true);
                        Instantiate(bread[itemInHand - 1], hand.transform);
                        StartCoroutine(Cutting());
                    }                  
                }
                else if (colliderTag == "Rodding")
                {
                    if (isUnableToMove == false && (itemInHand == 2 || itemInHand == 4))
                    {
                        audioSource.PlayOneShot(clips[1]);

                        for (int i = 0; i < hand.transform.childCount; i++)
                        {
                            Destroy(hand.transform.GetChild(i).gameObject);
                        }
                        isUnableToMove = true;
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                        PlayerRotation(new Vector3(1,0,0));
                        ani.SetBool("isRolling", true);
                        hand.SetActive(false);
                        rollSmoke.SetActive(true);
                        roddingUI.SetActive(true);
                    }

                    else if (isUnableToMove == true)
                    {
                        //Debug.Log("true");
                        audioSource.Stop();
                        roddingArrow.rotationSpeed_Run = 0;
                        //Debug.Log(roddingArrow.GetComponent<RectTransform>().localEulerAngles);

                        float currentRotation = (roddingArrow.GetComponent<RectTransform>().localEulerAngles.z > 180) ? (roddingArrow.GetComponent<RectTransform>().localEulerAngles.z - 360) : (roddingArrow.GetComponent<RectTransform>().localEulerAngles.z);

                        Debug.Log(currentRotation);

                        if (currentRotation >= -23.675f && currentRotation <= 22.328f)
                        {
                            itemInHand = breadRecipes.Rodding(itemInHand);
                            for (int i = 0; i < hand.transform.childCount; i++)
                            {
                                Destroy(hand.transform.GetChild(i).gameObject);
                            }
                            Instantiate(bread[itemInHand - 1], hand.transform);
                        }
                        else
                        {
                            itemInHand = 23;
                            for (int i = 0; i < hand.transform.childCount; i++)
                            {
                                Destroy(hand.transform.GetChild(i).gameObject);
                            }
                            Instantiate(bread[itemInHand - 1], hand.transform);
                        }
                        roddingUI.SetActive(false);
                        isUnableToMove = false;
                        rollSmoke.SetActive(false);
                        ani.SetBool("isRolling", false);
                        hand.SetActive(true);
                        roddingArrow.rotationSpeed_Run = roddingArrow.rotationSpeed;
                    }


                }
                else if (colliderTag == "CocoSauce")
                {
                    itemInHand = breadRecipes.CocoSauce(itemInHand);
                    for (int i = 0; i < hand.transform.childCount; i++)
                    {
                        Destroy(hand.transform.GetChild(i).gameObject);
                    }
                    Instantiate(bread[itemInHand - 1], hand.transform);
                }
                else if (colliderTag == "StrawberrySauce")
                {
                    itemInHand = breadRecipes.StrawberrySauce(itemInHand);
                    for (int i = 0; i < hand.transform.childCount; i++)
                    {
                        Destroy(hand.transform.GetChild(i).gameObject);
                    }
                    Instantiate(bread[itemInHand - 1], hand.transform);
                }
                else if (colliderTag == "CocoRice")
                {
                    itemInHand = breadRecipes.CocoRice(itemInHand);
                    for (int i = 0; i < hand.transform.childCount; i++)
                    {
                        Destroy(hand.transform.GetChild(i).gameObject);
                    }
                    Instantiate(bread[itemInHand - 1], hand.transform);
                }
                else if (colliderTag == "TrashCan")
                {
                    itemInHand = breadRecipes.TrashCan(itemInHand);
                    ani.SetBool("isTaking", false);
                    for (int i = 0; i < hand.transform.childCount; i++)
                    {
                        Destroy(hand.transform.GetChild(i).gameObject);
                    }
                }
                else if (colliderTag == "PlateArea")
                {
                    if (colliderItem.GetComponent<PlateArea>().haveMouse == true)
                    {
                        colliderItem.GetComponent<PlateArea>().haveMouse = false;
                        colliderItem.GetComponent<PlateArea>().isShow_exclamation = false;
                        colliderItem.GetComponent<PlateArea>().exclamation.SetActive(false);
                        Destroy(colliderItem.GetComponent<PlateArea>().mouseArea.transform.GetChild(1).gameObject);
                        colliderItem.GetComponent<PlateArea>().mouseResidenceTime_count = colliderItem.GetComponent<PlateArea>().mouseResidenceTime;
                    }
                    else if (colliderItem.GetComponent<PlateArea>().isEmpty && itemInHand != 0)
                    {
                        colliderItem.GetComponent<PlateArea>().itemNum = itemInHand;
                        colliderItem.GetComponent<PlateArea>().isEmpty = false;
                        Instantiate(bread[itemInHand - 1], colliderItem.GetComponent<PlateArea>().area.transform);
                        itemInHand = 0;
                        ani.SetBool("isTaking", false);
                        for (int i = 0; i < hand.transform.childCount; i++)
                        {
                            Destroy(hand.transform.GetChild(i).gameObject);
                        }
                        Debug.Log("空手");
                    }
                    else if (!colliderItem.GetComponent<PlateArea>().isEmpty && itemInHand == 0)
                    {
                        ani.SetBool("isTaking", true);
                        itemInHand = colliderItem.GetComponent<PlateArea>().itemNum;
                        colliderItem.GetComponent<PlateArea>().itemNum = 0;
                        for (int i = 0; i < colliderItem.GetComponent<PlateArea>().area.transform.childCount; i++)
                        {
                            Destroy(colliderItem.GetComponent<PlateArea>().area.transform.GetChild(i).gameObject);
                        }
                        colliderItem.GetComponent<PlateArea>().isEmpty = true;
                        Instantiate(bread[itemInHand - 1], hand.transform);
                        switch (itemInHand)
                        {
                            case 1:
                                Debug.Log("炸麵團");
                                break;
                            case 2:
                                Debug.Log("烤麵團");
                                break;
                            case 3:
                                Debug.Log("切好的麵包(炸的)");
                                break;
                            case 4:
                                Debug.Log("切好的麵包(烤的)");
                                break;
                            case 5:
                                Debug.Log("桿好的麵包(烤的)");
                                break;
                            case 6:
                                Debug.Log("切好桿好的麵包(烤的)");
                                break;
                            case 7:
                                Debug.Log("桿好切好的麵包(烤的)");
                                break;
                            case 8:
                                Debug.Log("甜甜圈(熟的)");
                                break;
                            case 9:
                                Debug.Log("法棍(熟的)");
                                break;
                            case 10:
                                Debug.Log("吐司(熟的)");
                                break;
                            case 11:
                                Debug.Log("可頌(熟的)");
                                break;
                            case 12:
                                Debug.Log("巧克力醬甜甜圈");
                                break;
                            case 13:
                                Debug.Log("巧克力醬吐司");
                                break;
                            case 14:
                                Debug.Log("巧克力醬可颂");
                                break;
                            case 15:
                                Debug.Log("巧克力醬+巧米甜甜圈");
                                break;
                            case 16:
                                Debug.Log("巧克力醬+巧米吐司");
                                break;
                            case 17:
                                Debug.Log("巧克力醬+巧米可颂");
                                break;
                            case 18:
                                Debug.Log("甜甜圈(焦的)");
                                break;
                            case 19:
                                Debug.Log("法棍(焦的)");
                                break;
                            case 20:
                                Debug.Log("吐司(焦的)");
                                break;
                            case 21:
                                Debug.Log("可頌(焦的)");
                                break;

                        }
                    }
                }
                else if (colliderTag == "Firewood" && itemInHand == 0)
                {
                    fire.SetActive(true);
                    bakeScript.smokeGroup.SetActive(true);
                    Debug.Log("f");
                    if (bakeScript.fireWoodNum < 5)
                    {
                        Debug.Log("v");

                        bakeScript.fireWoodNum++;
                        bakeScript.firewoodEnergyBar.sprite = bakeScript.firewoodEnergyBar_color[bakeScript.fireWoodNum];
                        bakeScript.haveFirewood = true;
                        bakeScript.count_firewoodReduceTime = 0;
                    }
                    else if (bakeScript.fireWoodNum >= 5)
                    {
                        Debug.Log("c");

                        bakeScript.count_firewoodReduceTime = 0;
                    }
                }
                else if (colliderTag == "DisplayArea")
                {
                    if (itemInHand != 0)
                    {
                        if (levelManagerment.levelNum == 0)
                        {
                            if (levelManagerment.CanFinishMenu(itemInHand) == true)
                            {
                                itemInHand = 0;
                                ani.SetBool("isTaking", false);
                                for (int i = 0; i < hand.transform.childCount; i++)
                                {
                                    Destroy(hand.transform.GetChild(i).gameObject);
                                }
                            }
                        }
                        else if (levelManagerment.levelNum == 1)
                        {
                            if (levelManagerment.CanIncreaseEnerage(itemInHand) == true)
                            {
                                itemInHand = 0;
                                ani.SetBool("isTaking", false);
                                for (int i = 0; i < hand.transform.childCount; i++)
                                {
                                    Destroy(hand.transform.GetChild(i).gameObject);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (levelManagerment.menuGroup.transform.childCount <= 0)
                        {
                            for (int i = 0; i < hand.transform.childCount; i++)
                            {
                                Destroy(hand.transform.GetChild(i).gameObject);
                            }
                            for (int i = 0; i < levelManagerment.displayAreas.Length; i++)
                            {
                                Destroy(levelManagerment.displayAreas[i].transform.GetChild(0).gameObject);
                            }
                            itemInHand = 22;
                            ani.SetBool("isTaking", true);
                            Instantiate(bread[itemInHand - 1], hand.transform);
                        }
                        /*else if (levelManagerment.IsFinish() == true && levelManagerment.levelNum == 1)
                        {
                            for (int i = 0; i < hand.transform.childCount; i++)
                            {
                                Destroy(hand.transform.GetChild(i).gameObject);
                            }
                            for (int i = 0; i < levelManagerment.displayAreas.Length; i++)
                            {
                                Destroy(levelManagerment.displayAreas[i].transform.GetChild(0).gameObject);
                            }
                            itemInHand = 22;
                            ani.SetBool("isTaking", true);
                            Instantiate(bread[itemInHand - 1], hand.transform);
                        }*/
                    }
                }
                else if (colliderTag == "TotemArea" && itemInHand == 22)
                {
                    levelManagerment.audioSource.Stop();
                    levelManagerment.isGaming = false;
                    totemWall.GetComponent<Renderer>().material = totemWall_after_material;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    otherPlayer.rb.velocity = Vector3.zero;
                    otherPlayer.rb.angularVelocity = Vector3.zero;
                    halo.SetActive(true);
                    StartCoroutine(ShowHalo());
                    //gameoverPanel.SetActive(true);
                    //Time.timeScale = 0;
                }
            }
        }


    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Mouse")
        {
            isDizzy = true;
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mouse")
        {
            Debug.Log("touch mouse");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            isDizzy = true;
            ani.SetBool("isRunning", false);
        }
        colliderItem = other.gameObject;
        colliderTag = other.tag;
        //rb.velocity = new Vector3(0, 0, 0);
        isEnterCollider = true;

    }
    private void OnTriggerExit(Collider other)
    {
        colliderTag = "";
        isEnterCollider = false;
    }
    public void ClickRetryButton(string sceneName)
    {
        FindObjectOfType<BGM>().PlayAudioClip(FindObjectOfType<BGM>().clips[0]);
        SceneManager.LoadScene(sceneName);
    }


    public IEnumerator Cutting()
    {
        cutSmoke.SetActive(true);
        yield return new WaitForSeconds(levelManagerment.cut_roll_time);
        cutSmoke.SetActive(false);
        isUnableToMove = false;
        ani.SetBool("isCutting", false);
        hand.SetActive(true);
    }
    public IEnumerator Rolling()
    {
        rollSmoke.SetActive(true);
        yield return new WaitForSeconds(levelManagerment.cut_roll_time);
        rollSmoke.SetActive(false);
        isUnableToMove = false;
    }

    public IEnumerator ShowHalo()
    {
        yield return new WaitForSeconds(1.5f);
        gameWinPanel.SetActive(true);
    }
}
