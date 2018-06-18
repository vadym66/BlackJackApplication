using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var container = new ContainerBuilder();

            container.RegisterControllers(typeof(MvcApplication).Assembly);

            container.RegisterType<GameService>().As<IGameService>();
            container.RegisterType<HistoryService>().As<IHistoryService>();
            container.RegisterType < PlayerRepository<Player>
        }
    }
}
