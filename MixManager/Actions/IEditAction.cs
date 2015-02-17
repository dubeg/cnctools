using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixManager.Actions
{
    public interface IEditAction
    {
        void Set();
        void Unset();
    }
}
