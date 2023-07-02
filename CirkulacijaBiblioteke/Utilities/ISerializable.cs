using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CirkulacijaBiblioteke.Utilities;

public interface ISerializable
{
    public string FileName();

    public IEnumerable<object>? GetList();
    public void Import(JToken token);
}