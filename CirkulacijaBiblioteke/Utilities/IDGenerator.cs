using System;

namespace CirkulacijaBiblioteke.Utilities;

public class IDGenerator : IIDGenerator
{
    public static int GetId()
    {
       return BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
    }

}