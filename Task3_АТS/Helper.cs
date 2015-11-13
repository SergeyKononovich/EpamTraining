using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3_АТS
{
    public delegate void StateChangedHandler<T>(object sender, T oldState, T newState);
}
