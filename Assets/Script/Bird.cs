using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    //bound 취득
    public Bounds GetBounds(GameObject obj)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        return spriteRenderer.bounds;
    }

    // 두 개의 객체가 AABB 방식으로 충돌하는지 확인합니다.
    bool CheckCollisionAABB(Bounds playerBounds, Bounds myBounds)
    {
        // AABB가 교차하는지 확인합니다.
        return Intersects(playerBounds, myBounds);
    }

    public bool Intersects(UnityEngine.Bounds playerBounds, UnityEngine.Bounds myBounds)
    {
        // 두 경계가 x축에서 겹치는지 확인합니다.
        if (playerBounds.min.x < myBounds.max.x && playerBounds.max.x > myBounds.min.x)
        {
            // 두 경계가 y축에서 겹치는지 확인합니다.
            if (playerBounds.min.y < myBounds.max.y && playerBounds.max.y > myBounds.min.y)
            {
                // 두 경계가 겹칩니다.
                return true;
            }
        }
        // 두 경계가 겹치지 않습니다.
        return false;
    }
    void Update()
    {
        if(!GameManager.inst.pause){
            transform.Translate(new Vector2(-(14f + GameManager.inst.stage) * Time.deltaTime, 0));
            if (transform.position.x <= -9.55f)
            {
                this.gameObject.SetActive(false);
            }
            if (CheckCollisionAABB(GetBounds(player), GetBounds(this.gameObject)))
            {
                if(GameManager.inst !=null && GameManager.inst.gameoverEvent !=null){
                    GameManager.inst.gameoverEvent.Invoke();
                }
            }
        }
    }
}
