using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace RobotWarsTests;

public class Tests
{
    public RobotWars RobotWars { get; set; }
    
    [SetUp]
    public void Setup()
    {
        RobotWars = new RobotWars();
    }

    [Test]
    [TestCase("5 5", "1 2 N", "LMLMLMLMM", "1 3 North")]
    [TestCase("5 5", "3 3 E", "MMRMMRMRRM",  "5 1 East")]

    public void TestGame(string arenaSetupInstruction, string robotAddInstruction, string robotMovementInstruction, string robotPosition)
    {
        RobotWars.ProcessInput(arenaSetupInstruction);
        RobotWars.ProcessInput(robotAddInstruction);
        var processingResult = RobotWars.ProcessInput(robotMovementInstruction);
        
        Assert.That(processingResult.Message, Is.EqualTo(robotPosition));
    }

    [Test]
    [TestCase("5 5", 5,5, false)]
    [TestCase("a b", 5,5, true)]
    [TestCase(" ", 5,5, true)]
    public void TestArenaSetup(string input, int width, int height, bool inputProcessingError)
    {
        InputProcessor inputProcessor = new InputProcessor(input);
        inputProcessor.Validate();
        GridInitializationValidationResult result = (inputProcessor.ProcessInitializeGridInstruction() as GridInitializationValidationResult);

        if (inputProcessingError)
        {
            Assert.That(result.Success, Is.EqualTo(false));
            Assert.That(result.ErrorMessage, Is.Not.Null);
        }
        else
        {
            Assert.That(result.GridSize.MaxWidth, Is.EqualTo(width));
            Assert.That(result.GridSize.MaxHeight, Is.EqualTo(height));
        }
    }
    
    
    [Test]
    [TestCase("1 2 N", 1, 2, BearingType.North, false)]
    [TestCase("1 2 n", 1, 2, BearingType.North, false)]
    [TestCase("1 2 n", 1, 2, BearingType.North, false)]
    [TestCase("- 2 n", 1, 2, BearingType.North, true)]
    [TestCase("1  n", 1, 2, BearingType.North, true)]
    public void TestRobotAddInstruction(string input, int x, int y, BearingType bearingType, bool inputProcessingError)
    {
        InputProcessor inputProcessor = new InputProcessor(input);
        inputProcessor.Validate();
        RobotAddInstructionValidationResult result = (inputProcessor.ProcessAddInstruction() as RobotAddInstructionValidationResult);

        if (inputProcessingError)
        {
            Assert.That(result.Success, Is.EqualTo(false));
            Assert.That(result.ErrorMessage, Is.Not.Null);
        }
        else
        {
            Assert.That(result.Position.X, Is.EqualTo(x));
            Assert.That(result.Position.Y, Is.EqualTo(y));
            Assert.That(result.Position.BearingType, Is.EqualTo(bearingType));    
        }
    }

    [Test]
    [TestCase("LMLM", new [] {MovementInstructionType.RotateLeft, MovementInstructionType.MoveForward, MovementInstructionType.RotateLeft, MovementInstructionType.MoveForward}, false)]
    [TestCase("lmlm", new [] {MovementInstructionType.RotateLeft, MovementInstructionType.MoveForward, MovementInstructionType.RotateLeft, MovementInstructionType.MoveForward}, false)]
    [TestCase("5mm", new [] {MovementInstructionType.RotateLeft, MovementInstructionType.MoveForward, MovementInstructionType.RotateLeft, MovementInstructionType.MoveForward}, true)]
    [TestCase("lm ml", new [] {MovementInstructionType.RotateLeft, MovementInstructionType.MoveForward, MovementInstructionType.RotateLeft, MovementInstructionType.MoveForward}, true)]
    public void TestRobotMovementInstruction(string input, MovementInstructionType[] instructions, bool inputProcessingError)
    {
        InputProcessor inputProcessor = new InputProcessor(input);
        inputProcessor.Validate();
        MovementInstructionValidationResult result = (inputProcessor.ProcessMoveInstruction() as MovementInstructionValidationResult);
        
        if (inputProcessingError)
        {
            Assert.That(result.Success, Is.EqualTo(false));
            Assert.That(result.ErrorMessage, Is.Not.Null);
        }
        else
        {
            CollectionAssert.AreEqual(instructions, result.MovementInstructions);
        }
    }
}