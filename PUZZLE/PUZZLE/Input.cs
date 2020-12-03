using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PUZZLE
{
    static class Input
    {
        // フィールド
        // 移動量
        private static Vector2 velocity = Vector2.Zero;
        // 現在のキーボードの状態
        private static KeyboardState currentKey;
        // 1フレーム前のキーボードの状態
        private static KeyboardState previousKey;

        // 現在のマウスの状態
        private static MouseState currentMouse;
        // 1フレーム前のマウスの状態
        private static MouseState previousMouse;

        /// <summary>
        /// 更新
        /// </summary>
        public static void Update()
        {
            // キーボード
            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            // マウス
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            // 入力方向更新
            UpdateVelocity();
        }

        /// <summary>
        /// 入力方向取得
        /// </summary>
        /// <returns>velocity</returns>
        public static Vector2 Velocity()
        {
            return velocity;
        }

        /// <summary>
        /// velocity更新処理
        /// </summary>
        public static void UpdateVelocity()
        {
            // 毎ループ初期化
            velocity = Vector2.Zero;

            // 右キーが押されていたら
            if (currentKey.IsKeyDown(Keys.Right))
            {
                velocity.X += 50.0f;
            }
            // 左キーが押されていたら
            else if (currentKey.IsKeyDown(Keys.Left))
            {
                velocity.X -= 50.0f;
            }
            // 上キーが押されていたら
            else if (currentKey.IsKeyDown(Keys.Up))
            {
                velocity.Y -= 50.0f;
            }
            // 下キーが押されていたら
            else if (currentKey.IsKeyDown(Keys.Down))
            {
                velocity.Y += 50.0f;
            }

            // 正規化
            if (velocity.Length() != 0)
            {
                velocity.Normalize();
            }
        }

        /// <summary>
        /// キーが押された瞬間か？
        /// </summary>
        /// <param name="key">チェックしたいキー</param>
        /// <returns>現在キーが押されていて、1フレーム前に押されていなければtrue</returns>
        public static bool IsKeyDown(Keys key)
        {
            return currentKey.IsKeyDown(key) && !previousKey.IsKeyDown(key);
        }

        /// <summary>
        /// キーが押された瞬間か？
        /// </summary>
        /// <param name="key">チェックしたいキー</param>
        /// <returns>押された瞬間ならtrue</returns>
        public static bool GetKeyTrigger(Keys key)
        {
            return IsKeyDown(key);
        }

        /// <summary>
        /// キーが押されているか？
        /// </summary>
        /// <param name="key">調べたいキー</param>
        /// <returns>キーが押されていたらtrue</returns>
        public static bool GetKeyState(Keys key)
        {
            return currentKey.IsKeyDown(key);
        }

        /// <summary>
        /// マウスの左ボタンが押された瞬間か？
        /// </summary>
        /// <returns>現在押されていて、1フレーム前押されていなければtrue</returns>
        public static bool IsMouseLButtonDown()
        {
            return currentMouse.LeftButton == ButtonState.Pressed &&
                previousMouse.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// マウスの左ボタンが離された瞬間か？
        /// </summary>
        /// <returns>現在離されていて、1フレーム前に押されていたらtrue</returns>
        public static bool IsMouseLButtonUp()
        {
            return currentMouse.LeftButton == ButtonState.Released &&
                previousMouse.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// マウスの左ボタンが押されているか？
        /// </summary>
        /// <returns>左ボタンが押されていたらtrue</returns>
        public static bool IsMouseLButton()
        {
            return currentMouse.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// マウスの右ボタンが押された瞬間か？
        /// </summary>
        /// <returns>現在押されていて、1フレーム前押されていなければtrue</returns>
        public static bool IsMouseRButtonDown()
        {
            return currentMouse.RightButton == ButtonState.Pressed &&
                previousMouse.RightButton == ButtonState.Released;
        }

        /// <summary>
        /// マウスの右ボタンが離された瞬間か？
        /// </summary>
        /// <returns>現在離されていて、1フレーム前に押されていたらtrue</returns>
        public static bool IsMouseRButtonUp()
        {
            return currentMouse.RightButton == ButtonState.Released &&
                previousMouse.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// マウスの右ボタンが押されているか？
        /// </summary>
        /// <returns>右ボタンが押されていたらtrue</returns>
        public static bool IsMouseRButton()
        {
            return currentMouse.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// マウスの位置を返す
        /// </summary>
        public static Vector2 MousePosition
        {
            // プロパティでGetterを作成
            get
            {
                return new Vector2(currentMouse.X, currentMouse.Y);
            }
        }

        /// <summary>
        /// マウスのスクロールホイールの変化量
        /// </summary>
        /// <returns>1フレーム前と現在のホイール量の差分</returns>
        public static int GetMouseWheel()
        {
            return previousMouse.ScrollWheelValue -
                currentMouse.ScrollWheelValue;
        }
    }
}
