using System.Collections.Generic;
using UnityEngine;

public class MoveRuller : MonoBehaviour
{
    public PadLockPassword _lockPassword;


    public List<GameObject> _rullers = new List<GameObject>();
    private int _selectedRullerIndex = 0;

    public int[] _numberArray = { 0, 0, 0, 0 };
    private bool _isActveEmission = false;

    void Awake()
    {
        foreach (GameObject r in _rullers)
        {
            r.transform.Rotate(-144, 0, 0, Space.Self);
        }
    }

    void Update()
    {
        HandleInput();
        _lockPassword.Password();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                int index = _rullers.IndexOf(hitObject);

                if (index != -1)
                {
                    _selectedRullerIndex = index;
                    _isActveEmission = true;
                }
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            _rullers[_selectedRullerIndex].transform.Rotate(36 * (int)Mathf.Sign(scroll), 0, 0, Space.Self);
            UpdateNumberArray(_selectedRullerIndex, (int)Mathf.Sign(scroll));
            _isActveEmission = true;
            BlinkRullers();
        }
    }

    void UpdateNumberArray(int index, int direction)
    {
        direction *= -1;
        _numberArray[index] += direction;

        if (_numberArray[index] > 9)
        {
            _numberArray[index] = 0;
        }
        else if (_numberArray[index] < 0)
        {
            _numberArray[index] = 9;
        }

    }

    void BlinkRullers()
    {
        foreach (GameObject r in _rullers)
        {
            if (_isActveEmission)
            {
                if (_rullers.IndexOf(r) == _selectedRullerIndex)
                {
                    r.GetComponent<PadLockEmissionColor>()._isSelect = true;
                    r.GetComponent<PadLockEmissionColor>().BlinkingMaterial();
                }
                else
                {
                    r.GetComponent<PadLockEmissionColor>()._isSelect = false;
                    r.GetComponent<PadLockEmissionColor>().BlinkingMaterial();
                }
            }
        }
    }
}