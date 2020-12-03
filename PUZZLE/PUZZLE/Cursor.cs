using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PUZZLE
{
    class Cursor
    {
        private StopBlock stopBlock;
        private Vector2 position;
        private bool isPressRightKey;
        private bool isPressLeftKey;
        private bool isPressUpKey;
        private bool isPressDownKey;
        private int timer;
        Vector2 tablePosition;

        public Cursor(StopBlock stopBlock)
        {
            this.stopBlock = stopBlock;
            position = new Vector2();
            tablePosition = new Vector2();
        }
        public void Initialize()
        {
            position.X = (Block.XMax / 2) * Block.Size;
            position.Y = (Block.YMax - 1) * Block.Size;
       
            isPressRightKey = false;
            isPressLeftKey = false;
            isPressUpKey = false;
            isPressDownKey = false;
            timer = 0;
        }
        public void Update()
        {
            Move();
            timer++;
        }
        public void Draw(Renderer renderer)
        {
            if (timer % 40 < 20)
            {
                renderer.DrawTexture("cursor", position + new Vector2(Block.StartX, Block.StartY));
            }
            else
            {
                renderer.DrawTexture("cursor", position + new Vector2(Block.StartX, Block.StartY),0.5f);
            }
        }
        private void Move()
        {
            MoveRight();
            MoveLeft();
            MoveUp();
            MoveDown();

            Vector2 min = new Vector2(0, 0);
            Vector2 max = new Vector2(Block.Size * (Block.XMax - 1), Block.Size * (Block.YMax - 1));
            position = Vector2.Clamp(position, min, max);
            SetTablePosition();
        }
        private void MoveRight()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if(isPressRightKey == false)
                {
                    ChangeBlock(tablePosition, tablePosition + new Vector2(1, 0));
                    position.X = position.X + Block.Size;// ブロックのサイズだけ右へ移動
                    isPressRightKey = true;// 「押した」に設定
                }               
            }
            else
            {
                isPressRightKey = false;
            }
        }
        private void MoveLeft()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (isPressLeftKey == false)
                {
                    ChangeBlock(tablePosition, tablePosition + new Vector2(-1, 0));
                    position.X = position.X - Block.Size;
                    isPressLeftKey = true;
                }
            }
            else
            {
                isPressLeftKey = false;
            }
        }
        private void MoveUp()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (isPressUpKey == false)
                {
                    ChangeBlock(tablePosition, tablePosition + new Vector2(0, -1));
                    position.Y -= Block.Size;
                    isPressUpKey = true;
                }
            }
            else
            {
                isPressUpKey = false;
            }
        }
        private void MoveDown()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if(isPressDownKey == false)
                {
                    ChangeBlock(tablePosition, tablePosition + new Vector2(0, 1));
                    position.Y += Block.Size;
                    isPressDownKey = true;
                }
            }
            else
            {
                isPressDownKey = false;
            }
        }
        private void SetTablePosition()
        {
            tablePosition = position / Block.Size;
        }
        private void ChangeBlock(Vector2 position1,Vector2 position2)
        {
            if (position2.X < 0 || position2.X >= Block.XMax ||
                position2.Y < 0 || position2.Y >= Block.YMax)
            {
                return;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                int color1 = stopBlock.GetBlockColor(position1);
                int color2 = stopBlock.GetBlockColor(position2);

                if (color1 != 0 && color2 != 0)
                {
                    stopBlock.SetBlock(position1, color2);
                    stopBlock.SetBlock(position2, color1);
                }
            }
        }
    }
}
