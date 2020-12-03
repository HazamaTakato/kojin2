using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace PUZZLE
{
    class Score
    {
        private int gameScore;          // ゲームスコア
        private int highScore;          // ハイスコア
        private int gamePoint;          // 揃えた得点

        public Score()
        {
            highScore = 0;
        }

        public void Initialize()
        {
            gameScore = 0;
            gamePoint = 0;
        }

        public void Draw(Renderer renderer)
        {
            // 「スコア」の表示
            renderer.DrawTexture("gametext", new Vector2(800, 20), new Rectangle(0, 0, 200, 64));
            // ゲームスコアの表示
            renderer.DrawNumber("number", new Vector2(800, 80), gameScore,5);
            // 「ハイスコア」の表示
            renderer.DrawTexture("gametext", new Vector2(800, 220), new Rectangle(0,64,310,64));
            // ハイスコアの表示
            renderer.DrawNumber("number", new Vector2(800, 280), highScore,5);

            // 揃った得点
            if (gamePoint > 0)
            {
                // 「得点」の表示
                renderer.DrawTexture("gametext", new Vector2(800, 420), new Rectangle(0, 165, 310, 64));

                // 得点の表示
                renderer.DrawNumber("number", new Vector2(800, 500), gamePoint);
            }
        }
        public void add(int gamePoint)
        {
            // 得点の登録
            this.gamePoint = gamePoint;

            // スコアの加算
            gameScore += gamePoint;

            // ハイスコアの更新
            if (gameScore < highScore)
            {
                highScore = gameScore;
            }
        }
    }
}
