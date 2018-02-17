using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ateam
{
    public class Hotel : BaseBattleAISystem
    {
        readonly static float ATTACK_MIDDLE_LEN = 8;
        readonly static float ATTACK_SHORT_LEN = 2;
        readonly static float INVINCIBLE_LEN = 3;
        public float x;
        public float y;


        //---------------------------------------------------
        // InitializeAI
        //---------------------------------------------------
        override public void InitializeAI()
        {
        }

        //---------------------------------------------------
        // UpdateAI
        //---------------------------------------------------
        override public void UpdateAI()
        {

            //互いのチームのデータを取得
            List<CharacterModel.Data> playerList = GetTeamCharacterDataList(TEAM_TYPE.PLAYER);
            List<CharacterModel.Data> enemyList = GetTeamCharacterDataList(TEAM_TYPE.ENEMY);

            if (playerList[1].Hp != 0)
            {
                CharacterModel.Data getItem = playerList[1];
                float c1 = x - getItem.BlockPos.x;
                float d1 = y - getItem.BlockPos.y;
                if (c1 > 0)
                {
                    Move(getItem.ActorId, Common.MOVE_TYPE.RIGHT);

                }
                if (c1 < 0)
                {
                    Move(getItem.ActorId, Common.MOVE_TYPE.LEFT);

                }
                if (d1 > 0)
                {
                    Move(getItem.ActorId, Common.MOVE_TYPE.UP);

                }
                if (d1 < 0)
                {
                    Move(getItem.ActorId, Common.MOVE_TYPE.DOWN);





                }
            }

            //索敵機から順にロックオン
            //

            foreach (CharacterModel.Data playerData in playerList)
            {

                float minlen = 100;

                foreach (CharacterModel.Data enemyData in enemyList)
                {

                    if (enemyData.Hp <= 0)
                    {
                        continue;
                    }

                    float len = (enemyData.BlockPos - playerData.BlockPos).magnitude;
                    if (minlen > len)
                        minlen = len;
                }
                if (minlen < ATTACK_SHORT_LEN)
                    Action(playerData.ActorId, Define.Battle.ACTION_TYPE.ATTACK_SHORT);
                if (minlen < INVINCIBLE_LEN)
                    Action(playerData.ActorId, Define.Battle.ACTION_TYPE.INVINCIBLE);
                if (minlen < ATTACK_MIDDLE_LEN)
                    Action(playerData.ActorId, Define.Battle.ACTION_TYPE.ATTACK_MIDDLE);
                Action(playerData.ActorId, Define.Battle.ACTION_TYPE.ATTACK_LONG);
            }


            if (enemyList[2].Hp != 0)
            {

                foreach (CharacterModel.Data playerData in playerList)
                {
                    CharacterModel.Data enemyData = enemyList[2];


                    float c = enemyData.BlockPos.x - playerData.BlockPos.x;
                    float d = enemyData.BlockPos.y - playerData.BlockPos.y;


                    if (c > 0)
                    {
                        Move(playerData.ActorId, Common.MOVE_TYPE.RIGHT);

                    }
                    if (c < 0)
                    {
                        Move(playerData.ActorId, Common.MOVE_TYPE.LEFT);

                    }
                    if (d > 0)
                    {
                        Move(playerData.ActorId, Common.MOVE_TYPE.UP);

                    }
                    if (d < 0)
                    {
                        Move(playerData.ActorId, Common.MOVE_TYPE.DOWN);

                    }

                }
            }
            if (enemyList[2].Hp == 0)
            {
                foreach (CharacterModel.Data playerData in playerList)
                {
                    CharacterModel.Data enemyData = enemyList[1];


                    float c = enemyData.BlockPos.x - playerData.BlockPos.x;
                    float d = enemyData.BlockPos.y - playerData.BlockPos.y;

                    if (c > 0)
                    {
                        Move(playerData.ActorId, Common.MOVE_TYPE.RIGHT);
                    }
                    if (c < 0)
                    {
                        Move(playerData.ActorId, Common.MOVE_TYPE.LEFT);
                    }
                    if (d > 0)
                    {
                        Move(playerData.ActorId, Common.MOVE_TYPE.UP);
                    }
                    if (d < 0)
                    {
                        Move(playerData.ActorId, Common.MOVE_TYPE.DOWN);
                    }
                }
            }
                if (enemyList[2].Hp == 0)
                {
                    foreach (CharacterModel.Data playerData in playerList)
                    {
                        CharacterModel.Data enemyData = enemyList[0];


                        float c = enemyData.BlockPos.x - playerData.BlockPos.x;
                        float d = enemyData.BlockPos.y - playerData.BlockPos.y;


                        if (c > 0)
                        {
                            Move(playerData.ActorId, Common.MOVE_TYPE.RIGHT);

                        }
                        if (c < 0)
                        {
                            Move(playerData.ActorId, Common.MOVE_TYPE.LEFT);

                        }
                        if (d > 0)
                        {
                            Move(playerData.ActorId, Common.MOVE_TYPE.UP);

                        }
                        if (d < 0)
                        {
                            Move(playerData.ActorId, Common.MOVE_TYPE.DOWN);

                        }

                    }

                }

            }


        



        //---------------------------------------------------
        // ItemSpawnCallback
        //---------------------------------------------------
        override public void ItemSpawnCallback(ItemSpawnData itemData)
        {
           x = itemData.BlockPos.x;
           y = itemData.BlockPos.y;
        }
    }
}