using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [SerializeField] bool _canBeTurnedOff = false;
    [SerializeField] bool _startTurnedOn = false;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Color _colorOn = Color.green;
    [SerializeField] Color _colorOff = Color.gray;

    bool _activated = false;

    public UnityEvent SwitchedOn;
    public UnityEvent SwitchedOff;

    // Start is called before the first frame update
    void Start()
    {
        if (_startTurnedOn)
        {
            _activated = true;
            _spriteRenderer.color = _colorOn;
        }
        else
        {
            _activated = false;
            _spriteRenderer.color = _colorOff;
        }
    }


    public void Activate()
    {
        if (!_activated)
        {
            _activated = true;
            SwitchedOn.Invoke();
            _spriteRenderer.color = _colorOn;
        }
        else if (_activated && _canBeTurnedOff)
        {
            _activated = false;
            SwitchedOff.Invoke();
            _spriteRenderer.color = _colorOff;
        }
    }
}
