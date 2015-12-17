using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McOverlapCore.ImageProcessing
{
    public enum ClipToHalfPlaneAction
    {
        DISCARD_LINE,
        UPDATE_START,
        UPDATE_END,
        DO_NOTHING
    }
}
