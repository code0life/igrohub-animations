using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Color GroundColor;
    public Color ObstaclesColor;
    public Color TargetsColor;
    public uint ObstaclesCount;
    
    Vector3 ObstacleScale;

    public GameObject ObstaclePrefab;
    public GameObject TargetPrefab;
    
    public int GridWidth;
    public float GridStep;
    
    void Start()
    {
        SetColor(gameObject, GroundColor);

        ObstacleScale = ObstaclePrefab.transform.localScale;

        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        for(uint i = 0; i < ObstaclesCount; ++i)
        {
            var position = new Vector3(i % GridWidth, 0, i / GridWidth);
            var obstacle_position = GridStep * position;
            SpawnObstacle(obstacle_position);

            var target_position = obstacle_position + Vector3.forward;
            SpawnTarget(target_position);
            
            yield return new WaitForSeconds(2.0f);
        }
    }

    GameObject SpawnTarget(Vector3 target_position)
    {
        var target = Instantiate(TargetPrefab);
        SetColor(target, TargetsColor);
        target.transform.position = target_position;
        return target;
    }

    GameObject SpawnObstacle(Vector3 position)
    {
        var obstacle = Instantiate(ObstaclePrefab);
        SetColor(obstacle, ObstaclesColor);

        Transform tfm = obstacle.transform;
        tfm.position = position;

        bool rotate = Random.value > 0.5f;
        if(!rotate)
            return obstacle;

        float rotation_x = Random.value > 0.5f ? 30.0f : -30.0f;
        tfm.RotateAround(Vector3.zero, Vector3.right, rotation_x);
        
        tfm.position = position - new Vector3(0.0f, 0.2f*ObstacleScale.y, 0.0f);

        return obstacle;
    }

    IEnumerator AnimateObstacleAppear(GameObject obstacle)
    {
        const float duration = 2.0f;
        float time_passed = 0.0f;

        float target_y = obstacle.transform.position.y;
        float start_y = target_y - 0.5f * ObstacleScale.y;

        Transform tfm = obstacle.transform;
        
        while(time_passed < duration)
        {
            float t = time_passed / duration;
            t = Easing.BackEaseOut(t);
            t = Mathf.Max(t, 0.0f);

            Vector3 scale = ObstacleScale;
            scale.x *= t;
            scale.z *= t;
            tfm.localScale = scale;

            Vector3 pos = tfm.position;
            pos.y = Mathf.Lerp(start_y, target_y, t);
            tfm.position = pos;

            time_passed += Time.deltaTime;
            yield return null;
        }
    }

    static void SetColor(GameObject o, Color color)
    {
        o.GetComponentInChildren<Renderer>().material.color = color;
    }
}
