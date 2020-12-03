﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace PUZZLE
{
    class StopBlock
    {
        private int[,] colorTable;   // ブロックの色
        private ChainStart chainStart;//連鎖
        private bool[,] effectXTable;   // 横が揃ったエフェクト
        private bool[,] effectYTable;   // 縦が揃ったエフェクト
        private bool[,] effectXYTable;
        private ChainEffect chainEffect;//連鎖エフェクト
        int gamePoint;//得点
        private Chain chainMode;//連鎖モード
        private ChainFall chainFall;// 連鎖落下クラス
        private Score score;
        private int stackY;
        public StopBlock(Score score)
        {
            this.score = score;
            // 配列の生成
            colorTable = new int[Block.YMax, Block.XMax];
            effectXTable = new bool[Block.YMax, Block.XMax];    // 横が揃ったエフェクト
            effectYTable = new bool[Block.YMax, Block.XMax];    // 横が揃ったエフェクト
            effectXYTable = new bool[Block.YMax, Block.XMax];
            chainStart = new ChainStart(colorTable,effectXTable,effectYTable,effectXYTable);
            chainEffect = new ChainEffect(colorTable, effectXTable, effectYTable,effectXYTable);
            chainFall = new ChainFall(colorTable);  // 連鎖落下クラス
        }
        public void Initialize()
        {
            chainMode = Chain.Normal;
            gamePoint = 0;
            stackY = 99;
            // ブロック配列のクリア
            // （縦横で２重ループ）
            for (int y = 0; y < Block.YMax; y++)
            {
                for (int x = 0; x < Block.XMax; x++)
                {
                    // 配列の初期化
                    colorTable[y, x] = 0;
                    effectXTable[y, x] = false;
                    effectYTable[y, x] = false;
                    effectXYTable[y, x] = false;
                }
            }
            // 連鎖開始クラス
            chainStart.Initialize();
        }
        public void Update()
        {
            if (chainMode == Chain.Normal)
            {
                ChainStartCheck();
            }
            else if (chainMode == Chain.Effect)
            {
                ChainEffectCheak();
            }
            else if (chainMode == Chain.Fall)
            {
                ChainFallCheck();
            }
            StackCheck();
        }
        //連鎖開始、チェック
        public void ChainStartCheck()
        {
            gamePoint = chainStart.Update();
            if (gamePoint > 0)
            {
                chainMode = Chain.Effect;
                chainEffect.Initialize();
            }
            score.add(gamePoint);
        }
        public void Draw(Renderer renderer)
        {
            DrawBlock(renderer);//ブロックの表示
            chainEffect.Draw(renderer);
        }
        private void DrawBlock(Renderer renderer)
        {
            for (int y = 0; y < Block.YMax; y++)
            {
                for (int x = 0; x < Block.XMax; x++)
                {
                    if (colorTable[y,x]!= 0)
                    {
                        Vector2 position = new Vector2(Block.Size * x, Block.Size * y);
                        position += new Vector2(Block.StartX, Block.StartY);
                        Rectangle rect = new Rectangle(Block.Size*(colorTable[y,x]-1), 0, Block.Size, Block.Size);
                        if (effectXTable[y, x] == true || effectYTable[y, x] == true)
                        {
                        }
                        else
                        {
                            renderer.DrawTexture("block", position, rect);
                        }
                    }
                }
            }
        }
        public void SetBlock(Vector2 tablePosition,int color)
        {
            //指定された配列の位置にデータを入れる
            colorTable[(int)tablePosition.Y, (int)tablePosition.X] = color;
        }
        public int GetBlockColor(Vector2 tablePosition)
        {
            //範囲外なら
            if (tablePosition.X < 0 || tablePosition.X >= Block.XMax ||
                tablePosition.Y < 0 || tablePosition.Y >= Block.YMax)
            {
                // -1を返す
                return -1;
            }
            else
            {
                return colorTable[(int)tablePosition.Y, (int)tablePosition.X];
            }
        }
        private void ChainEffectCheak()
        {
            bool mode = chainEffect.Update();
            if (mode == true)
            {
                chainMode = Chain.Fall;
            }
        }
        public Chain GetChainMode()
        {
            // 連鎖モードを返す
            return chainMode;
        }
        private void ChainFallCheck()
        {
            // 連鎖落下の更新を行い、結果を獲得
            bool flag = chainFall.Update();

            // 落ちるブロックが無ければ
            if (flag == false)
            {
                chainMode = Chain.Normal;                 // モードを通常へ移行
            }
        }
        void StackCheck()
        {
            // 積み上がったYを大きい数で初期設定
            // 注意：高く積まれるほどYは減る
            stackY = 99;

            // （縦横で２重ループ）
            for (int y = 0; y < Block.YMax; y++)
            {
                for (int x = 0; x < Block.XMax; x++)
                {
                    // ブロックがあれば
                    if (colorTable[y, x] != 0)
                    {
                        // 今のYが小さければ代入
                        if (y<stackY)
                        {
                            stackY = y;
                        }
                    }
                }
            }
        }
        public int GetStackY()
        {
            return stackY;
        }
    }
}
