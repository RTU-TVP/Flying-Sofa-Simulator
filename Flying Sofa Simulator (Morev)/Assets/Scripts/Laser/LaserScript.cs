using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    LineRenderer _beam;
    Transform _beamStartPos;
    [SerializeField] float _maxLength;
    [SerializeField] PlayerConfig _playerConfig;
    private void Awake()
    {
        _beam = GetComponent<LineRenderer>();
        _beamStartPos = transform;
    }
    public void BeamActivate()
    {
        _beam.enabled=true;
    }
    public void BeamDeactivate()
    {
        _beam.enabled = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(_beam.enabled) BeamDeactivate();
            else BeamActivate();
        }



        Ray ray = new Ray(_beamStartPos.position, _beamStartPos.forward);
        bool cast = Physics.Raycast(ray, out RaycastHit hit, _maxLength);
        Vector3 hitPosition = cast ? hit.point : _beamStartPos.position + _beamStartPos.forward * _maxLength;
        _beam.SetPosition(0, _beamStartPos.position);
        _beam.SetPosition(1, hitPosition);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("sofa"))
        {
            _playerConfig.SetAliveStatus(false);
        }
    }
}
