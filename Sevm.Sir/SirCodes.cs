﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sevm.Sir {

    /// <summary>
    /// 代码集合
    /// </summary>
    public class SirCodes : List<SirCode> {

        /// <summary>
        /// 添加指令
        /// </summary>
        /// <param name="line"></param>
        /// <param name="instruction"></param>
        public void Add(int line, SirCodeInstructionTypes instruction) {
            this.Add(new SirCode() {
                Instruction = instruction,
                Exp1 = -1,
                Exp2 = -1,
                Exp3 = -1,
                SourceLine = line,
            });
        }

        /// <summary>
        /// 添加指令
        /// </summary>
        /// <param name="line"></param>
        /// <param name="instruction"></param>
        /// <param name="exp1"></param>
        public void Add(int line, SirCodeInstructionTypes instruction, int exp1) {
            this.Add(new SirCode() {
                Instruction = instruction,
                Exp1 = exp1,
                Exp2 = -1,
                Exp3 = -1,
                SourceLine = line,
            });
        }

        /// <summary>
        /// 添加指令
        /// </summary>
        /// <param name="line"></param>
        /// <param name="instruction"></param>
        /// <param name="exp1"></param>
        /// <param name="exp2"></param>
        public void Add(int line, SirCodeInstructionTypes instruction, int exp1, int exp2) {
            this.Add(new SirCode() {
                Instruction = instruction,
                Exp1 = exp1,
                Exp2 = exp2,
                Exp3 = -1,
                SourceLine = line,
            });
        }

        /// <summary>
        /// 添加指令
        /// </summary>
        /// <param name="line"></param>
        /// <param name="instruction"></param>
        /// <param name="exp1"></param>
        /// <param name="exp2"></param>
        /// <param name="exp3"></param>
        public void Add(int line, SirCodeInstructionTypes instruction, int exp1, int exp2, int exp3) {
            this.Add(new SirCode() {
                Instruction = instruction,
                Exp1 = exp1,
                Exp2 = exp2,
                Exp3 = exp3,
                SourceLine = line,
            });
        }

        /// <summary>
        /// 获取字符串表示形式
        /// </summary>
        /// <returns></returns>
        public new string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("Code\r\n");
            for (int i = 0; i < this.Count; i++) {
                sb.Append("    ");
                sb.Append(this[i].ToString());
                sb.Append("\r\n");
            }
            sb.Append("End Code\r\n");
            return sb.ToString();
        }

        /// <summary>
        /// 获取字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes() {
            List<byte> ls = new List<byte>();
            // 生成集合
            List<byte> lss = new List<byte>();
            for (int i = 0; i < this.Count; i++) {
                lss.AddRange(this[i].ToBytes());
            }
            // 生成长度
            ls.AddRange(Parser.GetIntegerBytes(lss.Count));
            ls.AddRange(lss);
            return ls.ToArray();
        }

    }

}
