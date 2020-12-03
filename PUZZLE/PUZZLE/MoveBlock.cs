﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace PUZZLE
{
    class MoveBlock
    {
        Vector2 position;
        private bool isPressRightKey;
        private bool isPressLeftKey;
        bool aliveFlag;
        Random rand;
        int color;
        StopBlock stopBlock;
        Vector2 tablePosition;
        float fallspeed;
        private int timer;
        bool Return;
        public MoveBlock(StopBlock stopBlock)
        {
            this.stopBlock = stopBlock;
            position = new Vector2(0, 0);
            rand = new Random();
            tablePosition = new Vector2();                // 停止ブロック上の位置
        }
        public void Initialize()
        {
            isPressRightKey = false;
            isPressLeftKey = false;
            aliveFlag = false;
            fallspeed = 10;
            timer = 0;
            Return = false;
        }
        public void Update()
        {
            if (aliveFlag == true)
            {
                MoveDown();         // 落下移動
                AliveCheck();       // 存在チェック
            }
            // 存在しなければ
            else
            {
                timer++;
                if (timer>=60)
                {
                    ////通常モードならば
                    if (stopBlock.GetChainMode() == Chain.Normal)
                    {
                        SetBlock();// 上から発生
                    }
                }
            }
        }
        public void Draw(Renderer renderer)
        {
            if (aliveFlag == true)
            {
                Rectangle rect = new Rectangle(Block.Size*(color-1), 0, Block.Size, Block.Size);
                // ブロック表示
                renderer.DrawTexture("block", position + new Vector2(Block.StartX, Block.StartY), rect);
            }
        }
        private void MoveDown()
        {

                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    position.Y += fallspeed;   // 高速落下
                }
                else
                {
                    position.Y += fallspeed;    // 通常落下
                }

            fallspeed += 0.0000000000000000000005f;
        }
        //private void MoveRightLeft()
        //{
        //    //対応する停止ブロック上の位置を計算
        //    SetTablePosition();
        //    // 右へ移動
        //    // 右キー、パッド右方向を押したら
        //    if (Keyboard.GetState().IsKeyDown(Keys.Right))
        //    {
        //        // 前回が押してなければ
        //        if (isPressRightKey == false)
        //        {
        //            //右側が空いてあれば
        //            if (stopBlock.GetBlockColor(tablePosition + new Vector2(1, 0)) == 0 &&
        //                stopBlock.GetBlockColor(tablePosition + new Vector2(1, 1)) == 0)
        //            {

        //                position.X += Block.Size;   // ブロックのサイズだけ右へ移動
        //                isPressRightKey = true;// 「押した」に設定
        //            }    
        //        }
        //    }
        //    else　// 押してなければ
        //    {
        //        isPressRightKey = false;      // 「押してない」に設定
        //    }


        //    // 左へ移動
        //    // 左キー、パッド左方向を押したら
        //    if (Keyboard.GetState().IsKeyDown(Keys.Left))
        //    {
        //        // 前回が押してなければ
        //        if (isPressLeftKey == false)
        //        {
        //            // 左側が空いてあれば
        //            if (stopBlock.GetBlockColor(tablePosition+new Vector2(-1,0)) == 0 &&
        //                stopBlock.GetBlockColor(tablePosition+new Vector2(-1,1)) == 0)
        //            {

        //                position.X -= Block.Size;       // ブロックのサイズだけ左へ移動
        //                isPressLeftKey = true;// 「押した」に設定
        //            }
        //        }
        //    }
        //    else　// 押してなければ
        //    {
        //        isPressLeftKey = false;// 「押してない」に設定
        //    }
        //}
        private void AliveCheck()
        {
            //対応する停止ブロック上の位置を計算
            SetTablePosition();
            // 画面の下に到着
            if (position.Y >= Block.Size * (Block.YMax - 1)||stopBlock.GetBlockColor(tablePosition+new Vector2(0,1))!=0)
            {
                // 存在しない
                aliveFlag = false;
                //停止ブロックの発生
                stopBlock.SetBlock(tablePosition, color);
            }
        }
        private void SetBlock()
        {
            // 存在する
            aliveFlag = true;

            // 座標の設定
            position.X = rand.Next(Block.XMax) * Block.Size;
            position.Y = 0;

            //色設定
            color = rand.Next(5) + 1;
        }
        private void SetTablePosition()
        {
            //表示座標をブロックサイズで割れば
            //対応する配列の位置が算出できる
            tablePosition = position / Block.Size;
        }
    }
}
