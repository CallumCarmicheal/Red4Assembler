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

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red4Assembler {
    public partial class FunctionDissembler {
        internal string Dissemble(FunctionDefinition functionDefinition) {
            //
            StringBuilder sb = new StringBuilder();

            string functionSignature = generateFunctionSignature(functionDefinition);
            var functionBody = decompileBody(functionDefinition);

            string json = JsonConvert.SerializeObject(
                new {
                    functionSignature,
                    functionBody,
                    functionDefinition
                }
            , Formatting.Indented, new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            System.IO.File.WriteAllText("Red4Assembler/debug/Dissemble.json", json);
            System.IO.File.WriteAllText("Red4Assembler/debug/Dissemble.ws", string.Join("\n", functionBody));

            // Fancy print store in file
            {
                // Indent by 1 tab
                var lines = new List<string>(functionBody);
                
            }

            // 
            return sb.ToString();
        }

        private string generateFunctionSignature(FunctionDefinition fnDef) {
            string globalFunctionName = getFullDeclaredPath(fnDef);
            string returnType = !fnDef.Flags.HasFlag(FunctionFlags.HasReturnValue) ? "void" : getFullDeclaredPath(fnDef.ReturnType);

            List<string> parameters = new List<string>();
            foreach(var param in fnDef.Parameters) 
                parameters.Add($"{param.Name} : {getFullDeclaredPath(param.Type)}");

            StringBuilder signature = new StringBuilder();
            signature.Append(fnDef.Visibility.ToString().ToLower() + " function ");
            signature.Append(globalFunctionName + "( ");
            signature.Append(string.Join(", ", parameters));
            signature.Append(" ) : " + returnType);

            return signature.ToString();
        }

        private string getFullDeclaredPath(Definition def) {
            Stack<Definition> defChain = new Stack<Definition>();

            Definition currentDef = def;
            while (currentDef != null) {
                defChain.Push(currentDef);
                currentDef = currentDef.Parent;
            }

            StringBuilder sb = new StringBuilder();
            while (defChain.Count > 0) 
                sb.Append("::" + defChain.Pop().Name.Split(';')[0]);

            sb.Remove(0, 2); // Trim starting ::
            return sb.ToString();
        }
    
        private List<string> decompileBody(FunctionDefinition definition) {
            BodyParseState state = new BodyParseState() {
                instructions = definition.Body,
                fnDefinition = definition
            };

            List<string> Lines = new List<string>();

            Instruction[] instructions = definition.Body;

            bool parse = true;
            while (state.currentIdx < instructions.Length && parse) {
                var instr = instructions[state.currentIdx];

                if (instr.Op == Opcode.NoOperation) {
                    state.currentIdx++;
                    continue;
                }

                Lines.Add(decompileOperation(ref state, instr, out bool wasProcessed) + ";");

                if (!wasProcessed) {
                    state.currentIdx++;
                }
            }

            return Lines; // string.Join("\r\n", Lines);
        }

        private delegate bool DOperationDecompiler(ref BodyParseState state, Instruction instr, out string operation);
        private string decompileOperation(ref BodyParseState state, Instruction instr, out bool processed) {
            var decompilers = new DOperationDecompiler[] {
                this.decompileOperation_BaseTypes,
                this.decompileOperation_Call,
                this.decompileOperation_Constants,
                this.decompileOperation_References,
                this.decompileOperation_StringTypes,
            };


            if (processed = (instr.Op == Opcode.NoOperation))
                return "";

            foreach( var decom in decompilers ) {
                if (processed = decom(ref state, instr, out string operation)) {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("decompileOperation: " + $"${state.currentIdx:X4}, {(byte)instr.Op:X2} - {instr.Op.ToString()}");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    return operation;
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("decompileOperation: " + $"${state.currentIdx:X4}, {(byte)instr.Op:X2} - {instr.Op.ToString()}");
            Console.ForegroundColor = ConsoleColor.Gray;
            return $"<${state.currentIdx:X4}, UNPARSED, {(byte)instr.Op:X2} - {instr.Op.ToString()}>";
        }


        private class BodyParseState {
            public int currentIdx = 0;
            public Instruction[] instructions;
            public FunctionDefinition fnDefinition;
        }
    }
}
