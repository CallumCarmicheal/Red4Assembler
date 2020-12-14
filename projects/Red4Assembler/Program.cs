using Gibbed.RED4.ScriptFormats;
using Gibbed.RED4.ScriptFormats.Definitions;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red4Assembler {
    class Program {
        static void Main(string[] args) {
            var scriptFile = "final.redscripts";

            if (args.Length >= 0 && string.IsNullOrWhiteSpace(args[0]) == false)
                scriptFile = args[0];

            const bool validate = true;

            CacheFile scriptCacheFile;
            var fileBytes = File.ReadAllBytes(scriptFile);
            using (var input = new MemoryStream(fileBytes, false)) 
                scriptCacheFile = CacheFile.Load(input, validate);

            var functionBodyScripts =
                scriptCacheFile.Definitions
                    .OfType<FunctionDefinition>()
                    .Where(t => t.Flags.HasFlag(FunctionFlags.HasBody))
                    .OrderBy(f => f.SourceFile?.Path)
                    .ThenBy(f => f.SourceLine);

            var dism = new FunctionDissembler();
            string script = dism.Dissemble(functionBodyScripts.First());
            File.WriteAllText("Red4Assembler.output_test.txt", script);
        }
    }
}
