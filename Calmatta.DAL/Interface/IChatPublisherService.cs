using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calmatta.DAL.Interface
{
    public interface IChatPublisherService
    {
        bool Publish(string message);
    }
}
