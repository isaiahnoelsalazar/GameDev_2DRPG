using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public float JumpStrength, DashCD = 0.3f, DashTimer, JumpCD = 1f, JumpTimer;
    bool CanAttack = true, CanDash = true, CanJump = true;
    public bool Dashing = false;
    float DashPosition;

    [SerializeField]
    GameObject AttackArea, InventoryPanel, InventorySlotPrefab, InventoryItemDialogPanel;
    [SerializeField]
    Text InventoryItemMessage;
    [SerializeField]
    GameObject GameOverPanel;
    [SerializeField]
    Slider HealthBar;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        CurrentHealth = MaxHealth;
        if (PlayerPrefs.GetInt("HasSavedStats", -1) != -1)
        {
            MaxHealth = PlayerPrefs.GetFloat("PlayerMaxHealth", MaxHealth);
            CurrentHealth = PlayerPrefs.GetFloat("PlayerCurrentHealth", CurrentHealth);
            Attack = PlayerPrefs.GetFloat("PlayerAttack", Attack);
        }
        RefreshHealth();
        GlobalValues.GlobalPlayerInstance = this;
        RefreshInventory();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && !InventoryPanel.activeInHierarchy && !GlobalValues.GlobalDialogPanel.activeInHierarchy)
        {
            if (GlobalValues.PlayerInteractableObject != null)
            {
                GlobalValues.PlayerInteractableObject.Interact();
            }
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            InventoryPanel.SetActive(!InventoryPanel.activeInHierarchy);
            InventoryItemDialogPanel.SetActive(false);
        }
        if (!CanJump)
        {
            JumpTimer -= Time.deltaTime;
            if (JumpTimer <= 0)
            {
                CanJump = true;
            }
        }
        if (!CanDash)
        {
            DashTimer -= Time.deltaTime;
            if (DashTimer <= 0)
            {
                Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
                Rigidbody2D.gravityScale = 9.81f;
                Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                Dashing = false;
                CanDash = true;
                GetComponent<BoxCollider2D>().excludeLayers = LayerMask.GetMask("Nothing");
            }
        }
        if (!CanAttack)
        {
            AttackTimer -= Time.deltaTime;
            if (AttackTimer <= 0)
            {
                CanAttack = false;
                AttackArea.SetActive(false);
            }
        }
        if (!GlobalValues.NoMove && !InventoryPanel.activeInHierarchy && !GlobalValues.GlobalDialogPanel.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0))
            {
                AttackArea.SetActive(true);
                CanAttack = false;
                AttackTimer = AttackCD;
            }
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                Animator.SetBool("IsMoving", true);
            }
            else
            {
                Animator.SetBool("IsMoving", false);
            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                FacingLeft = false;
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                FacingLeft = true;
            }
            if (FacingLeft)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                AttackArea.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
                AttackArea.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (CanDash && Input.GetAxisRaw("Horizontal") != 0)
                {
                    Dashing = true;
                    GetComponent<BoxCollider2D>().excludeLayers = LayerMask.GetMask("LayerExclusion");
                    if (FacingLeft)
                    {
                        DashPosition = transform.position.x - 5f;
                    }
                    else
                    {
                        DashPosition = transform.position.x + 5f;
                    }
                    Rigidbody2D.gravityScale = 0;
                    Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    DashTimer = DashCD;
                    CanDash = false;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (CanJump)
                {
                    Rigidbody2D.AddForce(new Vector2(0, JumpStrength), ForceMode2D.Impulse);
                    GetComponent<BoxCollider2D>().excludeLayers = LayerMask.GetMask("LayerExclusion");
                    JumpTimer = JumpCD;
                    CanJump = false;
                }
            }
            AttackArea.transform.position = new Vector2(transform.position.x + (FacingLeft ? -2.1875f : 2.1875f), transform.position.y);
            if (!Dashing)
            {
                transform.position = new Vector2(transform.position.x + (Speed * Input.GetAxisRaw("Horizontal")), transform.position.y);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(DashPosition, transform.position.y), 0.04f);
            }
        }
        else
        {
            Animator.SetBool("IsMoving", false);
        }
        if (GlobalValues.DialogOut)
        {
            GlobalValues.NoMove = false;
            GlobalValues.GlobalDialogPanel.SetActive(false);
            GlobalValues.DialogOut = false;
        }
    }

    public override void TakeDamage(float Damage)
    {
        base.TakeDamage(Damage);
        Rigidbody2D.AddForce(new Vector2(FacingLeft ? (JumpStrength / 2) : -(JumpStrength / 2), JumpStrength / 2), ForceMode2D.Impulse);
        RefreshHealth();
        if (CurrentHealth <= 0)
        {
            GameOverPanel.SetActive(true);
        }
    }

    public void RefreshInventory()
    {
        foreach (Transform Child in InventoryPanel.transform)
        {
            Destroy(Child.gameObject);
        }
        List<string> TempName = new List<string>();
        List<InventoryItem> Temp = new List<InventoryItem>();
        foreach (InventoryItem InventoryItem in GlobalValues.Inventory)
        {
            if (!TempName.Contains(InventoryItem.Name))
            {
                TempName.Add(InventoryItem.Name);
                Temp.Add(InventoryItem);
            }
        }
        TempName.Sort();
        Temp.Sort((a, b) => a.Name.CompareTo(b.Name));
        for (int a = 0; a < Temp.Count; a++)
        {
            GameObject InventorySlot = Instantiate(InventorySlotPrefab, InventoryPanel.transform);
            InventorySlot.GetComponent<InventorySlot>().SetInventoryItem(Temp[a]);
            InventorySlot.GetComponent<InventorySlot>().SetInventoryItemPanel(InventoryItemDialogPanel);
            InventorySlot.GetComponent<InventorySlot>().SetInventoryItemMessage(InventoryItemMessage);
            InventorySlot.GetComponent<Image>().sprite = Temp[a].Icon;
            InventorySlot.GetComponentInChildren<Text>().text = $"x{GlobalValues.CountItemInInventory(Temp[a].Name)}";
        }
    }

    public void AddToInventory(InventoryItem InventoryItem)
    {
        GlobalValues.Inventory.Add(InventoryItem);
        RefreshInventory();
    }

    public void RefreshHealth()
    {
        if (HealthBar != null)
        {
            HealthBar.maxValue = MaxHealth;
            HealthBar.value = CurrentHealth;
        }
    }
}
