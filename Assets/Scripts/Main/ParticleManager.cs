using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }

    [SerializeField] private ParticleSystem particleSystem_MouseClick;
    [SerializeField] private ParticleSystem particleSystem_Click;
    [SerializeField] private ParticleSystem particleSystem_Enviroment;
    [SerializeField] private ParticleSystem particleSystem_Fragment;
    [SerializeField] private ParticleSystem particleSystem_Beforest;
    [SerializeField] private ParticleSystem particleSystem_Fruit;
    [SerializeField] private ParticleSystem particleSystem_Warp;
    [SerializeField] private ParticleSystem particleSystem_Gungunir;
    [SerializeField] private ParticleSystem particleSystem_Windom;
    [SerializeField] private ParticleSystem particleSystem_InstLvUp;
    [SerializeField] private ParticleSystem particleSystem_Awake;
    [SerializeField] private ParticleSystem particleSystem_MouseClickHaert;

    private int EnviromentCnt = 0;
    private int EnviromentCntMax = 100;

    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        Instance = this;
    }

    public void AllStop()
    {
        EnviromentCnt = 0;
        particleSystem_Click.Stop();
        particleSystem_Enviroment.Stop();
        particleSystem_Fragment.Stop();
        particleSystem_Beforest.Stop();
        particleSystem_Fruit.Stop();
        particleSystem_Warp.Stop();
        particleSystem_Gungunir.Stop();
        particleSystem_Windom.Stop();
        particleSystem_InstLvUp.Stop();
        particleSystem_Awake.Stop();
    }

    private void Update()
    {
        if (EnviromentCnt > 0)
        {
            particleSystem_Enviroment.maxParticles = EnviromentCnt;
            particleSystem_Enviroment.Emit(1);
        }
        //Debug.Log($"SetInstParticleCnt:{EnviromentCnt}");
    }

    public void PlayParticle_MouseClick(Vector3 pos)
    {
        EmitParticle(particleSystem_MouseClick, pos);
        EmitParticle(particleSystem_MouseClickHaert, pos);
    }

    public void PlayParticle_Click(int num = 1)
    {
        particleSystem_Click.Emit(num);
    }

    public void SetInstParticleCnt(int num = 1)
    {
        EnviromentCnt = Mathf.CeilToInt((float)num * 0.1f);
        if (EnviromentCnt > EnviromentCntMax)
        {
            EnviromentCnt = EnviromentCntMax;
        }
        //Debug.Log($"SetInstParticleCnt:{EnviromentCnt} {EnviromentCntMax} {num}");
    }

    public void PlayParticle_Fragment(Vector3 pos)
    {
        PlayParticle(particleSystem_Fragment, pos);
    }
    public void PlayParticle_Beforest(Vector3 pos)
    {
        PlayParticle(particleSystem_Beforest, pos);
    }
    public void PlayParticle_Fruit(Vector3 pos)
    {
        PlayParticle(particleSystem_Fruit, pos);
    }
    public void PlayParticle_Warp(Vector3 pos)
    {
        PlayParticle(particleSystem_Warp, pos);
    }
    public void PlayParticle_Awake(Vector3 pos)
    {
        PlayParticle(particleSystem_Awake, pos);
    }
    
    public void PlayParticle_Gungunir(Vector3 pos)
    {
        PlayParticle(particleSystem_Gungunir, pos);
    }
    public void PlayParticle_Wisdom(Vector3 pos)
    {
        PlayParticle(particleSystem_Windom, pos);
    }
    public void PlayParticle_InstLvUp(Vector3 pos)
    {
        PlayParticleAll(particleSystem_InstLvUp, pos);
    }

    public void PlayParticle_InstLvUp_Reset()
    {
        ResetParticle(particleSystem_InstLvUp);
    }


    private void ResetParticle(ParticleSystem _particleSystem)
    {
        ParticleSystem[] p = _particleSystem.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem _p in p)
        {
            _p.gameObject.SetActive(false);
            _p.gameObject.SetActive(true);
        }
    }
    private void PlayParticle(ParticleSystem _particleSystem, Vector3 pos)
    {
        _particleSystem.transform.position = pos;
        _particleSystem.Play();
    }
    private void PlayParticleAll(ParticleSystem _particleSystem, Vector3 pos)
    {
        _particleSystem.transform.position = pos;
        ParticleSystem[] p = _particleSystem.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem _p in p)
        {
            _p.Stop();
            _p.Play();
        }
    }
    private void EmitParticle(ParticleSystem _particleSystem, Vector3 pos)
    {
        _particleSystem.transform.position = pos;
        ParticleSystem[] p = _particleSystem.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem _p in p)
        {
            _p.Emit(1);
        }
    }


}
