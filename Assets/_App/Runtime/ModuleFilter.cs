using System.Collections.Generic;
using System.Linq;
using Gamebox.Core;

namespace Gamebox.App
{
    public static class ModuleFilter
    {
        public static IEnumerable<ModuleDefinition> Shown(IEnumerable<ModuleDefinition> definitions) =>
            definitions.Where(definition => definition.Enabled);
    }
}
