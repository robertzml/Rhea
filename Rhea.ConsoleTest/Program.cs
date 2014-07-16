using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Synchronization synchronization = new Synchronization();
            //synchronization.SyncBuildingGroup();
            //synchronization.ShowBuildingMap();
            //synchronization.SyncBuilding();
            synchronization.SyncSubregion();
        }
    }
}
