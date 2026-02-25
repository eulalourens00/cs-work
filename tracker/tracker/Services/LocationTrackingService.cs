using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tracker.Services
{
    public partial class LocationTrackingService : ILocationTrackingService
    {
        public void StartTracking()
        {
            StartTrackingInternal();
        }

        partial void StartTrackingInternal();
    }
}
