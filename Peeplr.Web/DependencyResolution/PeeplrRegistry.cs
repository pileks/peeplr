using Peeplr.Data;

namespace Peeplr.Web.DependencyResolution
{
    using Model.Commands;
    using Model.Queries;
    using ct = Model.Commands.Test;
    using qt = Model.Queries.Test;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;

    public class PeeplrRegistry : Registry
    {
        public PeeplrRegistry()
        {
            Scan( scan =>
            {
                //scan.Assembly("Peeplr");
                //scan.SingleImplementationsOfInterface();
            });

            For<IContactCommands>().Use<ct::ContactCommands>();
            For<IEmailCommands>().Use<ct::EmailCommands>();
            For<INumberCommands>().Use<ct::NumberCommands>();
            For<ITagCommands>().Use<ct::TagCommands>();

            For<IContactQueries>().Use<qt::ContactQueries>();
            For<IEmailQueries>().Use<qt::EmailQueries>();
            For<INumberQueries>().Use<qt::NumberQueries>();
            For<ITagQueries>().Use<qt::TagQueries>();

            For<PeeplrDataContext>().Singleton();
        }
    }
}