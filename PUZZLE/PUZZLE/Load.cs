using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUZZLE
{
    class Load:IScene
    {
        private bool endFlag;
        int timer;

        public Load()
        {

        }
        public void Initialize()
        {
            endFlag = false;   // シーン継続に設定
            timer = 0;          // タイマーの初期化
        }
        public void Update()
        {

        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("load", new Microsoft.Xna.Framework.Vector2(20, 20));

            timer++;
            if (timer > 10)
            {
                endFlag = true;
                renderer.LoadTexture("titlebg");          // タイトル
                renderer.LoadTexture("titletext");      // タイトル文字
                renderer.LoadTexture("starttext");      // ゲームスタート文字
                renderer.LoadTexture("ending");         // エンディング文字
                renderer.LoadTexture("wall");           // 黒い壁紙
                renderer.LoadTexture("gametext");   // ゲームテキスト
                renderer.LoadTexture("number");     // 数字
                renderer.LoadTexture("block");      // ブロック
                renderer.LoadTexture("cursor");     // カーソル
                renderer.LoadTexture("fade");       // フェード
                renderer.LoadTexture("info");       // 操作情報
            }
        }
        public bool IsEnd()
        {
            // 終了か？を返す
            return endFlag;
        }
        public Scene Next()
        {
            // 次のシーンを返す（タイトル）
            return Scene.Title;
        }

    }
}
