using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI;
    [SerializeField] Button closeBut;
    [SerializeField] Button openBut;
    [SerializeField] Inventory bag;
    [SerializeField] TMPro.TMP_Text money;
    void Start()
    {
        closeBut.onClick.AddListener(CloseInventory);
        openBut.onClick.AddListener(InvShowInventory);
        bag.OnMoneyChangeAddListener(ChangeMoneyText);
        ChangeMoneyText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InvShowInventory();
        }
    }
    private void ChangeMoneyText()
    {
        money.text = bag.GetMoney().ToString();
    }
    private void CloseInventory()
    {
        EventManager.OnInventoryHide.Invoke();
        inventoryUI.SetActive(false);
    }
    private void ShowInventory()
    {
        if ((inventoryUI.transform.position.x > 1910 * Screen.width / 1920 || inventoryUI.transform.position.x < -20 * Screen.width / 1920) ||
    (inventoryUI.transform.position.y > 952 * Screen.height / 1080 || inventoryUI.transform.position.y < -58 * Screen.height / 1080))
        {
            inventoryUI.transform.position = new Vector3(1471 * Screen.width / 1920, 478 * Screen.height / 1080, 0);
        }
        inventoryUI.SetActive(true);
    }
    private void InvShowInventory()
    {
        if ((inventoryUI.transform.position.x > 1910 * Screen.width / 1920 || inventoryUI.transform.position.x < -20 * Screen.width / 1920) ||
    (inventoryUI.transform.position.y > 952 * Screen.height / 1080 || inventoryUI.transform.position.y < -58 * Screen.height / 1080))
        {
            inventoryUI.transform.position = new Vector3(1471 * Screen.width / 1920, 478 * Screen.height / 1080, 0);
        }
        if (inventoryUI.activeSelf) EventManager.OnInventoryHide.Invoke();
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
}
