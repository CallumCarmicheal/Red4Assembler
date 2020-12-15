/* Copyright (c) 2020 Callum Carmicheal (callumcarmicheal 'at' gmail 'dot' com)
 * 
 * License: zlib
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */


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
            System.IO.Directory.CreateDirectory("Red4Assembler/debug");
            System.IO.Directory.CreateDirectory("Red4Assembler/tests");

            var scriptFile = "final.redscripts";

            if (args.Length > 0 && string.IsNullOrWhiteSpace(args[0]) == false)
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
                    .ThenBy(f => f.SourceLine)
                    .ToArray();

            var dism = new FunctionDissembler();
            System.IO.File.WriteAllText("Red4Assembler/tests/GetActionAnimationSlideParams.non_ref.ws", dism.Dissemble(functionBodyScripts[0]));
            System.IO.File.WriteAllText("Red4Assembler/tests/GetActionAnimationSlideParams.ref_param.ws", dism.Dissemble(functionBodyScripts[1]));

            // Console.ReadLine();
        }
    }
}
