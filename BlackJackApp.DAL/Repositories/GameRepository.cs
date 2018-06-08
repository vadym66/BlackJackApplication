﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackApp.DAL.Dapper;
using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities;
using BlackJackApp.Entities.Entities;
using Dapper;
using Dapper.Contrib.Extensions;

namespace BlackJackApp.Services
{
    public class GameRepository<T> : IGameRepository<Game> where T : Game
    {
        public async Task<IEnumerable<Player>> GetGame(int gameId)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                var sql = @"SELECT Players.Name
                            FROM PlayerGames
                            LEFT JOIN Players ON PlayerGames.Player_Id = Players.Id
                            WHERE PlayerGames.Game_Id = @Id";

                return await connection.QueryAsync<Player>(sql, new { });
            }
        }

        public async Task<int> Add(Game game)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                var sql = @"INSERT INTO Games OUTPUT Inserted.ID DEFAULT VALUES";

                var id = await connection.QueryAsync<int>(sql, new { Game = game });
                
                return id.Single();
            }
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                var sql = "SELECT * FROM Games";

                return await connection.QueryAsync<Game>(sql);
            }
        }

        public async Task<Game> GetLast()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                var sql = "SELECT TOP 1 * FROM Games ORDER BY Id DESC";

                return await connection.QuerySingleAsync<Game>(sql);
            }
        }
    }
}
