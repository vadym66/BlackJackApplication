using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using BlackJackApp.DAL.Interfaces;
using BlackJackApp.DAL.Repositories;
using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Services;
using BlackJackApp.Services.ServiceInterfaces;
using BlackJackApp.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlackJackApp.Controllers.Util
{
    public static class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

           
            builder.RegisterType<PlayerRepository<Player>>().As<IPlayerRepository<Player>>().PreserveExistingDefaults();
            builder.RegisterType<GameRepository<Game>>().As<IGameRepository<Game>>().PreserveExistingDefaults();
            builder.RegisterType<RoundRepository<Round>>().As<IRoundRepository<Round>>().PreserveExistingDefaults();
            builder.RegisterType<CardRepository<Card>>().As<ICardRepository<Card>>().PreserveExistingDefaults();
            builder.RegisterType<PlayersGameRepository<Player>>().As<IPlayersGameRepository<Player>>().PreserveExistingDefaults();

            builder.RegisterType<GameService>()
                .As<IGameService>()
                .PreserveExistingDefaults()
                .WithParameters(new List<Parameter>()
                                    { new NamedParameter("gameRepository", new GameRepository<Game>()),
                                      new NamedParameter("playerRepository", new PlayerRepository<Player>()),
                                      new NamedParameter("roundRepository", new RoundRepository<Round>()),
                                      new NamedParameter("cardRepository", new CardRepository<Card>()),
                                      new NamedParameter("playersGameRepository", new PlayersGameRepository<Player>())
                                    });

            builder.RegisterType<HistoryService>().As<IHistoryService>().PreserveExistingDefaults();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}