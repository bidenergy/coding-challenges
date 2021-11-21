﻿using System;

namespace RobotWars.Logic
{
    public record InstructionProcssingResult
    {
        public bool Successful { get; set; }
        public string SuccessMessage { get; set; }
        public string FailureMessage { get; set; }

        public static InstructionProcssingResult InvalidInput(string failureMessage)
        {
            return new InstructionProcssingResult
            {
                Successful = false,
                FailureMessage = failureMessage
            };
        }

        public static InstructionProcssingResult Success()
        {
            return new InstructionProcssingResult
            {
                Successful = true
            };
        }

        internal static InstructionProcssingResult Success(string successMessasge)
        {
            return new InstructionProcssingResult
            {
                Successful = true,
                SuccessMessage = successMessasge
            };
        }
    }
}
