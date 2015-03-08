namespace Peeplr.Web.DependencyResolution
{
    using Model.Commands;
    using Model.Queries;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;

    public class PeeplrRegistry : Registry
    {
        public PeeplrRegistry()
        {
            Scan( scan =>
            {
                scan.Assembly("Peeplr");
                scan.SingleImplementationsOfInterface();
            });
        }
    }
}