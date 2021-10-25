using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateController : MonoBehaviour
{
    [SerializeField] private IItemUser _playerItemUse;
    [SerializeField] private GameObject _player;
    [SerializeField] private IHealth _playerHealth;

    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Text _currentItemText;
    void Start()
    {
        _playerItemUse = _player.GetComponent<IItemUser>();
        _playerHealth = _player.GetComponent<IHealth>();
    }

    // Update is called once per frame
    void Update()
    {

        _hpSlider.value = _playerHealth.Health / _playerHealth.MaxHealth;
        _currentItemText.text = _playerItemUse.CurrentItemManager.ItemText;
    }
}
