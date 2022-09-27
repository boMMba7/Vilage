using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MagicEffect : MonoBehaviour
{
    public enum EnemyType
    {
        MagicTorch,
        OrangeSpider
    }

    public GameObject _IceFreeze;
    public float _timeFrozen;
    public EnemyType _enemyType;

    private GameObject _ice;
    private bool _isFreezer;

   

    public void Freeze()
    {
        if (!_isFreezer)
        {
            _isFreezer = true;

            if (_enemyType == EnemyType.MagicTorch)
                FreezeTorch();
            if (_enemyType == EnemyType.OrangeSpider)
                FreezeSpider();

            Vector3 objSize = GetComponent<BoxCollider>().bounds.size;
            _ice = Instantiate(_IceFreeze, gameObject.transform.position, gameObject.transform.rotation);
            _ice.transform.localScale = objSize;
            Destroy(_ice, 20f);
        }
    }

    public void FreezeSpider()
    {
        GetComponent<EnemyControl>().Freeze();
        Invoke("UnFreezeSpider", _timeFrozen);
    }

    public void UnFreezeSpider()
    {
        GetComponent<EnemyControl>().UnFreeze();
        Destroy(_ice);
        _isFreezer = false;
    }

    private void FreezeTorch()
    {

        GetComponent<TorchControl>().Freeze();
        Invoke("UnfreezeTorch", _timeFrozen);

    }

    public void UnfreezeTorch()
    {
        GetComponent<TorchControl>().Unfreeze();
        Destroy(_ice);

        _isFreezer = false;
    }

    public void DestroyIce(float t)
    {
        Destroy(_ice, t);
    }
}
