using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MaskedSpirit.Enemies
{
    internal class EnemySpawner
    {
        public Action<Vector2> OnEnemyDeathDropXP;
        float mSpawnInterval = 5.0f;
        float mTimeSinceLastSpawn = 0f;
        List<Enemy> mEnemies;
        List<Enemy> mPreviousDeadEnemies;
        List<Enemy> mCurrentDeadEnemies;
        Random mRandom = new Random();
        float mMinSpawnInterval = 0.5f;
        float mSpawnIntervalDecreaseRate = 0.001f;
        public EnemySpawner()
        {
            mEnemies = new List<Enemy>();
            mPreviousDeadEnemies = new List<Enemy>();
            mCurrentDeadEnemies = new List<Enemy>();
        }

        public void Update(float pDeltaTime, Vector2 playerPosition)
        {
            mTimeSinceLastSpawn += pDeltaTime;
            mSpawnInterval = Math.Max(mMinSpawnInterval, mSpawnInterval - mSpawnIntervalDecreaseRate * pDeltaTime);
            if (mTimeSinceLastSpawn >= mSpawnInterval)
            {
                SpawnEnemy();
                mTimeSinceLastSpawn = 0f;
            }

            mCurrentDeadEnemies.Clear();

            foreach (var enemy in mEnemies)
            {
                enemy.Update(pDeltaTime, playerPosition);
                if (!enemy.isAlive)
                {
                    mCurrentDeadEnemies.Add(enemy);
                }
            }

            foreach (var enemy in mEnemies)
            {
                enemy.ApplySeparation(mEnemies, separationDistance: 32f, separationStrength: 2f);
            }

            var newlyDead = mCurrentDeadEnemies.Except(mPreviousDeadEnemies).ToList();

            foreach (var dead in newlyDead)
            {
                if (OnEnemyDeathDropXP != null)
                {
                    var rect = dead.getRectangle();
                    Vector2 dropPos = new Vector2(rect.X + rect.Width / 2f, rect.Y + rect.Height / 2f);
                    OnEnemyDeathDropXP(dropPos);
                }
            }

            mEnemies = mEnemies.Where(e => e.isAlive).ToList();

            mPreviousDeadEnemies.Clear();
            mPreviousDeadEnemies.AddRange(mCurrentDeadEnemies);
        }


        private void SpawnEnemy() 
        {
            int enemyType = mRandom.Next(0, 2); // 0 for CoatStand, 1 for CostumeHolder
            Rectangle spawnRectangle = new Rectangle(mRandom.Next(0, 800), mRandom.Next(0, 600), 50, 50); // Example spawn area
            Enemy newEnemy;
            if (enemyType == 0)
            {
                newEnemy = new CoatStandEnemy(spawnRectangle);
            }
            else
            {
                newEnemy = new CostumeHolderEnemy(spawnRectangle);
            }
            mEnemies.Add(newEnemy);
        }
        
        public List<Enemy> GetEnemies()
        {
            return mEnemies;
        }


    }
}
