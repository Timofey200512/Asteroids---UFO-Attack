using UnityEngine;

public class MenuAsteroidsSpawner : AsteroidsSpawner
{
    readonly float[] asteroidsScales = new float[] {.3f, .8f, 1.5f};
    protected override void MakeNumberOfAsteroids(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            float randomScale = asteroidsScales[Random.Range(0, 3)];
            MakeAnAsteroid(asteroid, 60, allCordinates[Random.Range(0, allCordinates.Count - 1)], new Vector2(randomScale, randomScale));
        }
    }
}
