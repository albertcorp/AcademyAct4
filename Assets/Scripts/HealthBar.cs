using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarSprite;
    [SerializeField] private float _reduceSpeed = 2;
    private float _target = 1;
    [SerializeField] private Camera _otherPlayerCamera;
    [SerializeField] private Camera _otherPlayerCamera2;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _target = currentHealth / maxHealth;
    }

    private void Update()
    {
        if (_otherPlayerCamera != null)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _otherPlayerCamera.transform.position);
        }
        else if (_otherPlayerCamera2 != null) 
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _otherPlayerCamera2.transform.position);
        }
        
        _healthBarSprite.fillAmount = Mathf.MoveTowards(_healthBarSprite.fillAmount, _target, _reduceSpeed * Time.deltaTime);
    }
}
