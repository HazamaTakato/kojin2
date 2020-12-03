using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace PUZZLE
{
    class Renderer
    {
        // メンバー変数の宣言
        private ContentManager contentManager; //コンテンツ管理者
        private GraphicsDevice graphicsDevice; //グラフィックデバイス
        private SpriteBatch spriteBatch;       //スプライトの一括描画用オブジェクトの宣言

        //画像をディクショナリで一元管理
        //ディクショナリでキーを『アセット名』、値を『2Dテクスチャ』にして、
        //オブジェクトtexturesを宣言し、実体を生成
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="content">コンテンツ管理者</param>
        /// <param name="graphics">グラフィックデバイス</param>
        public Renderer(ContentManager content, GraphicsDevice graphics)
        {
            //（Game1クラスの大本のコンテンツ情報とひもづけ）
            contentManager = content;
            //コンテンツ（リソース）が入っているルートの場所を指定
            contentManager.RootDirectory = "Content";
            //（Game1クラスの大本のGraphicsDeviceとひもづけ）
            graphicsDevice = graphics;
            //現在のグラフィックデバイスでスプライト一括描画オブジェクトの実体を作成
            spriteBatch = new SpriteBatch(graphics);
        }
        /// <summary>
        /// テクスチャー（画像）読み込み機能
        /// </summary>
        /// <param name="name">画像のアセット名</param>
        public void LoadTexture(string name)
        {
            //画像管理用ディクショナリに登録する
            textures[name] = contentManager.Load<Texture2D>(name);
        }

        /// <summary>
        /// 解放処理
        /// </summary>
        public void Unload()
        {
            textures.Clear();           // ディクショナリの解放
            contentManager.Unload();    // コンテンツ管理者に登録されている情報を解放
        }

        /// <summary>
        /// 描画処理の開始
        /// </summary>
        public void Begin()
        {
            //スプライト一括処理の開始
            spriteBatch.Begin();
        }

        /// <summary>
        /// 描画処理の終了
        /// </summary>
        public void End()
        {
            //スプライト一括処理の終了
            spriteBatch.End();
        }

        /// <summary>
        /// 1枚の画像を描画
        /// </summary>
        /// <param name="name">描画する画像のアセット名</param>
        /// <param name="position">表示位置</param>
        /// <param name="alpha">透明値（0.0：透明　～ 1.0：不透明）：デフォルト値は1.0</param>
        public void DrawTexture(string name, Vector2 position, float alpha = 1.0f)
        {
            //スプライトバッチの描画機能を呼び出し、描画
            //第１引数：描画するtexture2Dを設定
            //（ディクショナリtexturesに登録されている、画像をアセット名を使い呼び出す）
            //第２引数：描画位置
            //第３引数：色を指定
            spriteBatch.Draw(textures[name], position, Color.White * alpha);
        }
        public void DrawTexture(string name, Vector2 position, Rectangle rect, float alpha = 1.0f)
        {
            //スプライトバッチの描画機能を呼び出し、描画
            //第１引数：描画するtexture2Dを設定
            //（ディクショナリtexturesに登録されている、画像をアセット名を使い呼び出す）
            //第２引数：描画位置
            //第３引数：色を指定
            spriteBatch.Draw(textures[name], position,rect, Color.White * alpha);
        }
        /// <summary>
        /// 数字の描画
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">表示位置</param>
        /// <param name="number">表示したい数字</param>
        public void DrawNumber(string name, Vector2 position, int number)
        {
            //引数の数字(number)を文字列にして1文字ずつ処理
            foreach (var n in number.ToString())
            {
                //spriteBatch.Draw(指定するテクスチャ, 位置, 表示する範囲（矩形）, 色を指定) 
                spriteBatch.Draw(
                    textures[name],
                    position,
                    new Rectangle((int.Parse(n.ToString())) * 32, 0, 32, 64),
                    //new Rectangle((n - '0') * 32, 0, 32, 64),
                    Color.White);

                //1文字描画したら、次の文字は32px分右にずらす
                position.X = position.X + 32;
            }
        }
        public void DrawNumber(string name, Vector2 position, int number, int digit)
        {
            // この処理は１文字の大きさが幅32、高さ64とする

            // マイナスの数は０とする
            if (number < 0)
            {
                number = 0;
            }

            // 桁数だけ右へ表示座標を移動する
            position.X += (digit - 1) * 32;

            // 桁数ループして、１の位を表示
            for (int i = 0; i < 5; i++)
            {
                // 10で割る余りで、表示する数値を出す。
                int n = number % 10;

                // 幅を掛けて座標を求め、１文字を絵から切り出して表示
                spriteBatch.Draw(textures[name], position,
                                 new Rectangle(n * 32, 0, 32, 64), Color.White);

                // 数値を10で割ることで次の桁へ移動する。
                number /= 10;

                // 表示座標のＸ座標を左へ移動する（Ｘ座標から横幅の32を引く）
                position.X = position.X - 32;
            }
        }
    }
}
