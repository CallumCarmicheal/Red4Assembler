using Gibbed.RED4.ScriptFormats.Definitions;
using Gibbed.RED4.ScriptFormats;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red4Assembler {
    partial class FunctionDissembler {
        private bool decompileOperation_StringTypes(BodyParseState state, Instruction instr, out string operation) {
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
        private bool decompileOperation_Unknown_TODO(BodyParseState state, Instruction instr, out string operation) {
            operation = "";

            switch(instr.Op) {
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
          
            case Opcode.LoadParameter:
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
            case Opcode.ReturnWithValue:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadProperty:
                // TODO: this - 1998-01-05
                break;
            case Opcode.AsObject:
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


        private bool decompileOperation_References(BodyParseState state, Instruction instr, out string operation) {
            operation = "";

            switch (instr.Op) {
            case Opcode.StoreRef:
                // TODO: this - 1998-01-05
                break;
            case Opcode.RefLocal:
                // TODO: this - 1998-01-05
                break;
            case Opcode.RefProperty:
                // TODO: this - 1998-01-05
                break;
            }

            return false;
        }


        private bool decompileOperation_Call(BodyParseState state, Instruction instr, out string operation) {
            operation = "";

            var info = OpcodeInfo.Get(instr.Op);

            switch (instr.Op) {
            case Opcode.Call:
                // TODO: this - 1998-01-05
                break;
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
            }
            return false;
        }

        private bool decompileOperation_Template(BodyParseState state, Instruction instr, out string operation) {
            operation = "";

            switch (instr.Op) {
           
            }

            return false;
        }

        private bool decompileOperation_Constants(BodyParseState state, Instruction instr, out string operation) {
            operation = "";

            switch (instr.Op) {
            case Opcode.LoadConstantOne:
                operation = "1";
                return true;
            case Opcode.LoadConstantZero:
                operation = "0";
                return true;
            case Opcode.LoadConstantTrue:
                operation = "true";
                return true;
            case Opcode.LoadConstantFalse:
                operation = "false";
                return true;
            }
            return false;
        }

        private bool decompileOperation_BaseTypes(BodyParseState state, Instruction instr, out string operation) {
            operation = "";

            switch (instr.Op) {
            case Opcode.LoadInt8:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadInt16:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadInt32:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadInt64:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadUint8:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadUint16:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadUint32:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadUint64:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadFloat:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadDouble:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadName:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadEnumeral:
                // TODO: this - 1998-01-05
                break;
            case Opcode.LoadString:
                // TODO: this - 1998-01-05
                break;
            }

            return false;
        }
    }
}
