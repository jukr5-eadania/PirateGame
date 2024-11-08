using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirateGame
{
    public abstract class AnimationProcess
    {
        public bool Finished { get; set; }
        public abstract void Update(float elapsed);
    }
}
