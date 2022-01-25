using System;
using System.Collections.Generic;

public class Program
{
	public static void Main()
	{
		Console.WriteLine("Code For Robot Wars Challenge");
		
		// Test Input is taken in string	
		string testInput = "5 5\n1 2 N\nLMLMLMLMM\n3 3 E\nMMRMMRMRRM";
		
		// Splitting test input by new line and storing it in array
		string[] inputArray = testInput.Split('\n');
		
		int upperX = Convert.ToInt32(inputArray[0].Split(' ')[0]);
		int upperY = Convert.ToInt32(inputArray[0].Split(' ')[1]);
		
		List<String> output = new List<String>();
		
		
		// Logic to find the position of robots
		for(int i = 1; i < inputArray.Length; i=i+2) {
			
			// Robot's x,y coordinate and heading is stored in variables
			string position = inputArray[i];
			int currentPositionX = Convert.ToInt32(position.Split(' ')[0]);
			int currentPositionY = Convert.ToInt32(position.Split(' ')[1]);
			string currentHeading = position.Split(' ')[2];
			
			int nextPositionX, nextPositionY;
			string nextHeading;
			
			// nextPosition of robot is calculated based on current heading of the robot
			if(currentHeading.Equals("N")) {
				nextPositionX = currentPositionX;
				nextPositionY = currentPositionY + 1;
			} else if (currentHeading.Equals("E")) {
				nextPositionX = currentPositionX + 1;
				nextPositionY = currentPositionY;
			} else if (currentHeading.Equals("S")) {
				nextPositionX = currentPositionX;
				nextPositionY = currentPositionY - 1;
			} else if (currentHeading.Equals("W")) {
				nextPositionX = currentPositionX - 1;
				nextPositionY = currentPositionY;
			}
			
			// Robot's position is stored in a variable
			string instructions = inputArray[i+1];
		}
		
		// Method that returns heading of the robot based on current position and next position
		string getHeading(int currentPositionX, int currentPositionY, int nextPositionX, int nextPositionY) {
			string heading = "";
			
			if(currentPositionY == nextPositionY+1) heading = "N";
			else if(currentPositionX == nextPositionX+1) heading = "E";
			else if(currentPositionY == nextPositionY-1) heading = "S";
			else if(currentPositionX == nextPositionX-1) heading = "W";
			
			return heading;
		}	
		
	}
}