using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShipSettings : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float totalHealth = 100f;
    [SerializeField] float totalShield = 20f;
    [SerializeField] float totalBoost = 20f;

    [Header("Bars")]
    [SerializeField] Image healthBar;
    [SerializeField] Image shieldBar;
    [SerializeField] Image boostBar;

    private float currentHealth;
    private float currentShield;
    private float currentBoost;

    bool isIntangible = false;

    private BoxCollider bc;

    void Start()
    {
        currentHealth = totalHealth;
        currentBoost = totalBoost;
        currentShield = totalShield;

        bc = GetComponent<BoxCollider>();    
    }

    public bool IsIntangible()
    {
        return isIntangible;
    }

    public void ActivateIntangibility(float duration)
    {
        StartCoroutine(IntangibilityRoutine(duration));
    }

    IEnumerator IntangibilityRoutine(float duration)
    {
        isIntangible = true;
        bc.enabled = false;

        yield return new WaitForSeconds(duration);

        bc.enabled = true;
        isIntangible = false;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);

        healthBar.fillAmount = currentHealth / totalHealth;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return totalHealth;
    }

    public void HealShield(float amount)
    {
        currentShield += amount;
        currentShield = Mathf.Clamp(currentShield, 0, totalShield);

        shieldBar.fillAmount = currentShield / totalShield;
    }

    public float GetShield()
    {
        return currentShield;
    }

    public float GetMaxShield()
    {
        return totalShield;
    }

    public void HealBoost(float amount)
    {
        currentBoost += amount;
        currentBoost = Mathf.Clamp(currentBoost, 0, totalBoost);

        boostBar.fillAmount = currentBoost / totalBoost;
    }

    public float GetBoost()
    {
        return currentBoost;
    }

    public float GetMaBoost()
    {
        return totalBoost;
    }
}
