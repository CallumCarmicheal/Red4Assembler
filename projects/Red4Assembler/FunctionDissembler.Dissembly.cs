using Gibbed.RED4.ScriptFormats.Definitions;
using Gibbed.RED4.ScriptFormats;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red4Assembler {
    partial class FunctionDissembler {
        private bool decompileOperation_StringTypes(ref BodyParseState state, Instruction instr, out string operation) {
            operation = "";

            switch (instr.Op) {
            case Opcode.LoadName:
                break;
            case Opcode.LoadEnumeral:
                break;
            case Opcode.LoadString:
                break;
            }

            return false;
        }

        /// <summary>
        /// This function does not get called, its just here to ensure I dont miss any op codes.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="instr"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        private bool decompileOperation_Unknown_TODO(ref BodyParseState state, Instruction instr, out string operation) {
            operation = "";

            switch (instr.Op) {
            case Opcode.LoadTweakDBId:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadResource:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadConstantTrue:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadConstantFalse:
                // TODO: this - 1998-01-05
                break;


            case Opcode.Switch:
                // TODO: this - 1998-01-05
                break;
            case Opcode.SwitchCase:
                // TODO: this - 1998-01-05
                break;
            case Opcode.SwitchDefault:
                // TODO: this - 1998-01-05
                break;

            case Opcode.Construct:
                // TODO: this - 1998-01-05
                break;
           
           


            case Opcode.CompareEqual:
                // TODO: this - 1998-01-05
                break;
            case Opcode.CompareNotEqual:
                // TODO: this - 1998-01-05
                break;
            }

            return false;
        }


        private bool decompileOperation_References(ref BodyParseState state, Instruction instr, out string operation) {
            operation = "";

            switch (instr.Op) {
            case Opcode.StoreRef: {
                    state.currentIdx++;
                    bool processedOpCode = false;

                    var value = decompileOperation(ref state, state.instructions[state.currentIdx], out processedOpCode); 
                    if (!processedOpCode) state.currentIdx++; // if the opcode is not implemented make sure we still skip it.

                    var assignment = decompileOperation(ref state, state.instructions[state.currentIdx], out processedOpCode);
                    if (!processedOpCode) state.currentIdx++; // if the opcode is not implemented make sure we still skip it.

                    operation = $"{value} = {assignment}";
                    return true;
                }

            case Opcode.RefLocal: {
                    // Get the var name through the arguments
                    var arguments = ((ValueTuple<Gibbed.RED4.ScriptFormats.Definitions.LocalDefinition>)instr.Argument).Item1;
                    operation = arguments.Name;
                    state.currentIdx++;
                    return true;
                }

            case Opcode.RefProperty: {
                    // TODO: this - 1998-01-05
                    break;
                }

            case Opcode.LoadParameter: {
                    // Get the var name through the arguments
                    var arguments = ((ValueTuple<Gibbed.RED4.ScriptFormats.Definitions.ParameterDefinition>)instr.Argument).Item1;
                    operation = $"{arguments.Name}";
                    state.currentIdx++;
                    return true;
                }

            case Opcode.LoadProperty: {
                    var arguments = ((ValueTuple<Gibbed.RED4.ScriptFormats.Definitions.PropertyDefinition>)instr.Argument).Item1;
                    string nextInstrDecl = decompileOperation(ref state, state.instructions[++state.currentIdx], out bool processedOpCode);

                    if (!processedOpCode) // if the opcode is not implemented make sure we still skip it.
                        state.currentIdx++;

                    operation = nextInstrDecl + "." + arguments.Name;
                    return true;
                }
            }

            return false;
        }

        private bool decompileOperation_Call(ref BodyParseState state, Instruction instr, out string operation) {
            operation = "";
            
            switch (instr.Op) {
            case Opcode.Call: {
                    (_, _, var fn) = ((short, ushort, FunctionDefinition))instr.Argument;

                    if (fn.Parameters == null || fn.Parameters.Length == 0) {
                        operation = $"{fn.Name}()";
                        state.currentIdx++;

                        if (state.instructions[state.currentIdx].Op == Opcode.EndCall)
                            state.currentIdx++;

                        return true;
                    }

                    // TODO: this - 1998-01-05
                    break;
                }
            case Opcode.AsObject: {
                    // I am expecting the following pattern
                    //    AsObject, LoadParamter, Call, ..., EndCall.
                    // This is a basic implementation and is expected to be overriden with a better solution.
                    //  this is a hard coded solution for the signature above.
                    var idx = state.currentIdx;
                    if (state.instructions[idx + 1].Op == Opcode.LoadParameter
                          && state.instructions[idx + 2].Op == Opcode.Call) {
                        bool processedOpCode = false;
                        state.currentIdx = idx + 1; // Get the parameter

                        var parameter = decompileOperation(ref state, state.instructions[idx + 1], out processedOpCode);
                        if (!processedOpCode) state.currentIdx++; // if the opcode is not implemented make sure we still skip it.

                        var callFn = decompileOperation(ref state, state.instructions[state.currentIdx], out processedOpCode);
                        if (!processedOpCode) state.currentIdx++; // if the opcode is not implemented make sure we still skip it.

                        operation = $"{parameter}.{callFn}";
                        return true;
                    }

                    break;
                }
            case Opcode.NativeCall:
                // TODO: this - 1998-01-05
                break;
            case Opcode.EndCall:
                // TODO: this - 1998-01-05
                break;
            case Opcode.Jump:
                // TODO: this - 1998-01-05
                break;
            case Opcode.JumpFalse:
                // TODO: this - 1998-01-05
                break;

            case Opcode.ReturnWithValue: {
                    string nextInstrDecl = decompileOperation(ref state, state.instructions[++state.currentIdx], out bool processedOpCode);

                    if (!processedOpCode) // if the opcode is not implemented make sure we still skip it.
                        state.currentIdx++;

                    operation = $"return {nextInstrDecl}";
                    state.currentIdx++;
                    return true;
                }
            }
            return false;
        }

        private bool decompileOperation_Unknown(ref BodyParseState state, Instruction instr, out string operation) {
            operation = "";

            switch ((byte)instr.Op) {
            case 0x51: {
                    int currentIdx = state.currentIdx;
                    string nextInstrDecl = decompileOperation(ref state, state.instructions[++state.currentIdx], out bool processedOpCode);
                    
                    operation = $"<$UNK_OP_x51u ({currentIdx})>(${nextInstrDecl})";
                    return true;
                }
            }

            return false;
        }

        private bool decompileOperation_Template(ref BodyParseState state, Instruction instr, out string operation) {
            operation = "";

            switch (instr.Op) {
            //
            }

            return false;
        }

        private bool decompileOperation_Constants(ref BodyParseState state, Instruction instr, out string operation) {
            operation = "";

            switch (instr.Op) {
            case Opcode.LoadConstantOne:
                operation = "1";
                state.currentIdx++;
                return true;
            case Opcode.LoadConstantZero:
                operation = "0";
                state.currentIdx++;
                return true;
            case Opcode.LoadConstantTrue:
                operation = "true";
                state.currentIdx++;
                return true;
            case Opcode.LoadConstantFalse:
                operation = "false";
                state.currentIdx++;
                return true;
            }
            return false;
        }

        private bool decompileOperation_BaseTypes(ref BodyParseState state, Instruction instr, out string operation) {
            operation = "";

            switch (instr.Op) {
            // Note I've only tested LoadFloat, I assume all the base type primitives are the same.
            case Opcode.LoadInt8:
            case Opcode.LoadInt16:
            case Opcode.LoadInt32:
            case Opcode.LoadInt64:
            case Opcode.LoadUint8:
            case Opcode.LoadUint16:
            case Opcode.LoadUint32:
            case Opcode.LoadDouble:
            case Opcode.LoadUint64:
            case Opcode.LoadString:
            case Opcode.LoadFloat: {
                    operation = instr.Argument.ToString();
                    state.currentIdx++;
                    return true;
                }

            // I assume this will be a string, I'll get around to implementing it when I get a function using it.
            case Opcode.LoadName:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadEnumeral:
                // TODO: this - 1998-01-05
                break;
            }

            return false;
        }
    }
}
