using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BirdPool : MonoBehaviour
{
    public GameObject bird;
    public List<GameObject> pool = new();
    const int defaultPoolSize = 20;
    int poolSize = 0;
    // 게임이 시작될 때 호출되는 함수입니다.
    void Start()
    {
        // 기본 수의 선인장 개체를 생성하고 풀에 넣습니다.
        for (var i = 0; i < defaultPoolSize; i++)
        {
            // 새로운 선인장 개체를 인스턴스화합니다.
            var obj = Instantiate(bird);
            // 개체를 비활성화합니다.
            obj.SetActive(false);
            // 개체를 풀에 추가합니다.
            pool.Add(obj);
        }
        // 풀 크기를 기본 크기로 설정합니다.
        poolSize = defaultPoolSize;
    }

    // 선인장 개체를 생성하는 함수입니다.
    public GameObject Spawn()
    {
        // 풀에서 비활성화된 선인장 개체를 찾습니다.
        var obj = pool.Find(x => x.activeSelf == false);
        // 비활성화된 선인장 개체가 없으면 새 개체를 생성합니다.
        if (obj == null)
        {
            // 새로운 선인장 개체를 생성합니다.
            for (var i = 0; i < poolSize; i++)
            {
                var obj2 = Instantiate(bird);
                obj2.SetActive(false);
                pool.Add(obj2);
            }
            // 풀 크기를 2배로 늘립니다.
            poolSize *= 2;
            // 새로운 선인장 개체를 반환합니다.
            return Spawn();
        }
        else
        {
            // 활성화된 선인장 개체를 반환합니다.
            obj.SetActive(true);
            return obj;
        }
    }

    // 선인장 개체를 제거하는 함수입니다.
    public void DeSpawn(GameObject obj)
    {
        // 개체를 비활성화합니다.
        obj.SetActive(false);
    }
}
