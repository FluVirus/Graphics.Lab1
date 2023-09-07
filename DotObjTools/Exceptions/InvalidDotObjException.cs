using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotObjTools.Exceptions;

public class InvalidDotObjException: ArgumentException
{
    public uint? Line { get; }

    public InvalidDotObjException(string message) : base(message)
    { 
    
    }

    public InvalidDotObjException() : base() 
    { 
    
    }

    public InvalidDotObjException(string message, uint line) : base($"[Line: {line}] " + message)
    {
        Line = line;
    }
}
