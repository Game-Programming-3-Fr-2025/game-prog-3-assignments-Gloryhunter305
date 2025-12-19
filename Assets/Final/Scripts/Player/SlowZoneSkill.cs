using System;
using System.Collections;
using UnityEngine;

public class SlowZoneSkill : MonoBehaviour
{
    [SerializeField] private GameObject _slowZone;
    [SerializeField] private float coolDownDuration = 5f;
    private bool isCooldownActive = false;

    private void Start()
    {
        _slowZone = GameObject.FindWithTag("SlowZone").GetComponent<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCooldownActive)
        {
            UseSkill();
        }
    }

    void UseSkill()
    {
        isCooldownActive = true;
        _slowZone.SetActive(true);
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(coolDownDuration);
        _slowZone.SetActive(false);
        isCooldownActive = false;
    }
}
