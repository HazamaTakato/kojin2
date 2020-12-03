using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUZZLE
{
    class SceneManager
    {
        // メンバー変数の宣言
        private IScene currentScene = null; // 現在のシーン
        private Fade fade;

        // シーンを複数入れるディクショナリィ型のコレクション
        private Dictionary<Scene, IScene> scenes = new Dictionary<Scene, IScene>();


        /// <summary>
        /// コンストラクタ（生成時に自動的に呼び出される）
        /// </summary>
        public SceneManager()
        {
            fade = new Fade();
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            // 更新
            currentScene.Update();  // 現在のシーン
            fade.Update();
            // シ－ンが終了ならば
            if (currentScene.IsEnd() == true)
            {
                // 次のシーンを取り出す
                Scene next = currentScene.Next();

                // 次のシーンへ切り替える
                Change(next);
            }
        }

        /// <summary>
        /// 表示
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            //エンディングならゲームプレイの表示
            if (currentScene == scenes[Scene.Ending])
            {
                scenes[Scene.GamePlay].Draw(renderer);
            }
            currentScene.Draw(renderer);  // 現在のシーン
            // タイトルとゲームプレイでフェードイン
            if (currentScene == scenes[Scene.Title] || currentScene == scenes[Scene.GamePlay])
            {
                fade.Draw(renderer);// フェードインクラス
            }

        }
        /// <summary>
        /// シーンクラスの登録
        /// </summary>
        /// <param name="name"></param>
        /// <param name="scene"></param>
        public void Add(Scene name, IScene scene)
        {
            // 渡されたシーンをコレクションに登録する
            scenes[name] = scene;
        }


        /// <summary>
        /// 変更
        /// </summary>
        /// <param name="name"></param>
        public void Change(Scene name)
        {
            // 渡されたシーンを現在のシーンの登録する
            currentScene = scenes[name];

            // 現在のシーンの初期化
            currentScene.Initialize();
            fade.Initialize();
        }
    }
}
